"Doc."

from typing import Any, Dict, List
from uuid import uuid4

from fastapi import Depends, FastAPI, HTTPException, Query
from pydantic import BaseModel

app = FastAPI(
    title="Test Platform API",
    description="API для создания и прохождения тестов",
)

# In memory database
users_db: Dict[str, Dict[str, str]] = {}
tests_db: Dict[str, Dict[str, Any]] = {}
questions_db: Dict[str, Dict[str, str]] = {}
answers_db: Dict[str, List[Dict[str, Any]]] = {}


# Data validation
class UserRegister(BaseModel):
    username: str
    password: str
    password_confirm: str


class TestCreate(BaseModel):
    title: str
    description: str


class QuestionCreate(BaseModel):
    text: str


class AnswerSubmit(BaseModel):
    answer: bool  # True - Да, False - Нет


def get_current_user(
    token: str = Query(
        ...,
        description="Токен авторизации (для теста пишите 'token-<username>')",
    )
) -> str:
    """Проверка токена и возврат имени текущего пользователя."""
    if not token.startswith("token-"):
        raise HTTPException(status_code=401, detail="Неверный формат токена")

    username: str = token.replace("token-", "")
    if username not in users_db:
        raise HTTPException(status_code=401, detail="Пользователь не найден")

    return username


@app.post("/register", summary="Регистрация")
def register(user: UserRegister) -> Dict[str, str]:
    """По юзернейму и паролю + повторить пароль."""
    if user.password != user.password_confirm:
        raise HTTPException(status_code=400, detail="Пароли не совпадают")
    if user.username in users_db:
        raise HTTPException(
            status_code=400,
            detail="Пользователь с таким именем уже существует",
        )

    users_db[user.username] = {
        "username": user.username,
        "password": user.password,
    }
    return {"message": "Пользователь успешно зарегистрирован"}


@app.post("/login", summary="Авторизация")
def login(
    username: str = Query(...), password: str = Query(...)
) -> Dict[str, str]:
    """По юзернейму и паролю (query параметры)."""
    user = users_db.get(username)
    if not user or user["password"] != password:
        raise HTTPException(
            status_code=401, detail="Неверный логин или пароль"
        )

    return {"message": "Успешный вход", "token": f"token-{username}"}


@app.post("/tests", summary="Создание теста")
def create_test(
    test: TestCreate, current_user: str = Depends(get_current_user)
) -> Dict[str, Any]:
    """Название и описание теста."""
    test_id: str = str(uuid4())
    tests_db[test_id] = {
        "id": test_id,
        "title": test.title,
        "description": test.description,
        "owner": current_user,
        "is_hidden": False,
    }
    return tests_db[test_id]


@app.patch("/tests/{test_id}/visibility", summary="Скрытие/раскрытие теста")
def toggle_visibility(
    test_id: str, current_user: str = Depends(get_current_user)
) -> Dict[str, Any]:
    """Скрытие или раскрытие теста (только владелец)."""
    test = tests_db.get(test_id)
    if not test:
        raise HTTPException(status_code=404, detail="Тест не найден")

    if test["owner"] != current_user:
        raise HTTPException(
            status_code=403,
            detail="Только владелец может менять видимость теста",
        )

    test["is_hidden"] = not test["is_hidden"]
    status: str = "скрыт" if test["is_hidden"] else "раскрыт"
    return {
        "message": f"Статус теста изменен. Теперь он {status}.",
        "is_hidden": test["is_hidden"],
    }


@app.post("/tests/{test_id}/questions", summary="Загрузка вопросов")
def add_question(
    test_id: str,
    question: QuestionCreate,
    current_user: str = Depends(get_current_user),
) -> Dict[str, str]:
    """Добавление текста вопроса."""
    test = tests_db.get(test_id)
    if not test:
        raise HTTPException(status_code=404, detail="Тест не найден")
    if test["owner"] != current_user:
        raise HTTPException(
            status_code=403, detail="Только владелец может добавлять вопросы"
        )

    q_id: str = str(uuid4())
    questions_db[q_id] = {
        "id": q_id,
        "test_id": test_id,
        "text": question.text,
    }
    return questions_db[q_id]


@app.post(
    "/tests/{test_id}/questions/{question_id}/answer",
    summary="Прохождение теста",
)
def submit_answer(
    test_id: str,
    question_id: str,
    answer: AnswerSubmit,
    current_user: str = Depends(get_current_user),
) -> Dict[str, str]:
    """Отправка да/нет по конкретному вопросу."""
    if (
        question_id not in questions_db
        or questions_db[question_id]["test_id"] != test_id
    ):
        raise HTTPException(
            status_code=404, detail="Вопрос не найден в данном тесте"
        )

    if test_id not in answers_db:
        answers_db[test_id] = []

    ans_record: Dict[str, Any] = {
        "question_id": question_id,
        "user": current_user,
        "answer": answer.answer,
    }
    answers_db[test_id].append(ans_record)
    return {"message": "Ответ успешно сохранен"}


@app.get("/tests/{test_id}/answers", summary="Просмотр ответов")
def get_answers(
    test_id: str, current_user: str = Depends(get_current_user)
) -> List[Dict[str, Any]]:
    """Просмотр ответов по всему тесту (только владелец)."""
    test = tests_db.get(test_id)
    if not test:
        raise HTTPException(status_code=404, detail="Тест не найден")

    if test["owner"] != current_user:
        raise HTTPException(
            status_code=403,
            detail="Только владелец может просматривать ответы",
        )

    return answers_db.get(test_id, [])
