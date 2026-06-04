#!/usr/bin/env python3
from typing import Any, Dict, Optional

import requests

BASE_URL: str = "http://127.0.0.1:8000"
TIMEOUT_SEC: int = 10


def main() -> None:
    # This vars contain user session
    token: Optional[str] = None
    current_test_id: Optional[str] = None
    current_question_id: Optional[str] = None

    while True:
        print("\n" + "-" * 40)
        print("API Client")
        print("-" * 40)
        print(f"Токен:      {token or 'Не авторизован'}")
        print(f"ID теста:   {current_test_id or 'Не выбран'}")
        print(f"ID вопроса: {current_question_id or 'Не выбран'}")
        print("-" * 40)
        print("1 Регистрация")
        print("2 Авторизация (получить токен)")
        print("3 Создать тест")
        print("4 Скрыть / раскрыть тест")
        print("5 Добавить вопрос в тест")
        print("6 Ответить на вопрос")
        print("7 Просмотреть ответы по тесту")
        print("0 Выход")
        print("-" * 40)

        choice: str = input("Выберите опцию: ").strip()

        try:
            if choice == "1":
                username: str = input("Введите логин: ")
                password: str = input("Введите пароль: ")
                password_confirm: str = input("Повторите пароль: ")

                reg_data: Dict[str, str] = {
                    "username": username,
                    "password": password,
                    "password_confirm": password_confirm,
                }
                res = requests.post(
                    f"{BASE_URL}/register", json=reg_data, timeout=TIMEOUT_SEC
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

            elif choice == "2":
                username = input("Введите логин: ")
                password = input("Введите пароль: ")

                res = requests.post(
                    f"{BASE_URL}/login",
                    params={"username": username, "password": password},
                    timeout=TIMEOUT_SEC,
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

                if res.status_code == 200:
                    token = res.json().get("token")
                    print("Токен успешно сохранен в клиенте.")

            elif choice == "3":
                title: str = input("Название теста: ")
                desc: str = input("Описание теста: ")

                res = requests.post(
                    f"{BASE_URL}/tests",
                    params={"token": token},
                    json={"title": title, "description": desc},
                    timeout=TIMEOUT_SEC,
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

                if res.status_code == 200:
                    current_test_id = res.json().get("id")
                    print("Тест создан и выбран как текущий.")

            elif choice == "4":
                t_id: str = (
                    input(f"Введите ID теста [{current_test_id}]: ").strip()
                    or current_test_id
                    or ""
                )
                if not t_id:
                    print("Ошибка: ID теста не указан!")
                    continue

                res = requests.patch(
                    f"{BASE_URL}/tests/{t_id}/visibility",
                    params={"token": token},
                    timeout=TIMEOUT_SEC,
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

            elif choice == "5":
                t_id = (
                    input(f"Введите ID теста [{current_test_id}]: ").strip()
                    or current_test_id
                    or ""
                )
                if not t_id:
                    print("Ошибка: ID теста не указан!")
                    continue

                text: str = input("Текст вопроса: ")
                res = requests.post(
                    f"{BASE_URL}/tests/{t_id}/questions",
                    params={"token": token},
                    json={"text": text},
                    timeout=TIMEOUT_SEC,
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

                if res.status_code == 200:
                    current_question_id = res.json().get("id")
                    print("Вопрос добавлен и выбран как текущий.")

            elif choice == "6":
                t_id = (
                    input(f"Введите ID теста [{current_test_id}]: ").strip()
                    or current_test_id
                    or ""
                )
                q_id: str = (
                    input(
                        f"Введите ID вопроса [{current_question_id}]: "
                    ).strip()
                    or current_question_id
                    or ""
                )

                if not t_id or not q_id:
                    print("Ошибка: Не указан ID теста или вопроса!")
                    continue

                ans_input: str = input("Ваш ответ (1 - Да, 0 - Нет): ").strip()
                answer_bool: bool = ans_input == "1"

                res = requests.post(
                    f"{BASE_URL}/tests/{t_id}/questions/{q_id}/answer",
                    params={"token": token},
                    json={"answer": answer_bool},
                    timeout=TIMEOUT_SEC,
                )
                print(
                    f"\n[Статус {res.status_code}] Ответ сервера: {res.json()}"
                )

            elif choice == "7":
                t_id = (
                    input(f"Введите ID теста [{current_test_id}]: ").strip()
                    or current_test_id
                    or ""
                )
                if not t_id:
                    print("Ошибка: ID теста не указан!")
                    continue

                res = requests.get(
                    f"{BASE_URL}/tests/{t_id}/answers",
                    params={"token": token},
                    timeout=TIMEOUT_SEC,
                )
                print(f"\n[Статус {res.status_code}] Ответ сервера:")

                if res.status_code == 200:
                    answers: list[Dict[str, Any]] = res.json()
                    if not answers:
                        print("  Ответов пока нет.")
                    else:
                        for ans in answers:
                            ans_text: str = "Да" if ans["answer"] else "Нет"
                            question_snippet: str = str(ans["question_id"])[:8]
                            print(
                                f"{ans['user']} | "
                                f"{question_snippet}... | "
                                f"Ответ: {ans_text}"
                            )

            else:
                print("Некорректный ввод. Попробуйте еще раз.")

        # Catch timeout and connection error exceptions
        except requests.exceptions.Timeout:
            print(f"\nОшибка: Сервер не ответил за {TIMEOUT_SEC} секунд.")
        except requests.exceptions.ConnectionError:
            print("\nОшибка: Не удалось подключиться к серверу. Он запущен?")


if __name__ == "__main__":
    main()
