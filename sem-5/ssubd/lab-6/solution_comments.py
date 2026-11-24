# Импортируем встроенный драйвер sqlite3 для низкоуровневого взаимодействия с файловой СУБД SQLite
# и модуль json для сериализации табличных выборок в стандартный формат обмена данными JSON.
import sqlite3
import json

# Устанавливаем соединение с базой данных предметной области «IT-компания».
# SQLite — встраиваемая бессерверная СУБД: если файл 'it_company.sqlite' отсутствует на диске,
# он будет создан автоматически в рабочей директории.
conn = sqlite3.connect("it_company.sqlite")

# ВАЖНАЯ ОСОБЕННОСТЬ SQLITE: по умолчанию проверка ссылочной целостности (FOREIGN KEY) отключена
# ради обратной совместимости со старыми версиями. Включаем её явно для текущей сессии.
conn.execute("PRAGMA foreign_keys = ON;")


# Задание 1: Формирование базы данных предметной области, состоящей из 5 связанных таблиц:
# 1) departments (отделы), 2) employees (сотрудники), 3) projects (проекты),
# 4) tasks (задачи), 5) work_logs (учет рабочего времени).
def create_all_tables():
    # Создаем объект курсора (cursor) — это интерфейс-посредник, через который отправляются
    # SQL-инструкции в движок СУБД и считываются результирующие наборы строк.
    cursor = conn.cursor()

    # Таблица 1: Справочник отделов (создается первой, так как на неё ссылается таблица employees).
    # Используем конструкцию IF NOT EXISTS, чтобы повторный запуск скрипта не вызывал исключение
    # sqlite3.OperationalError: table employees already exists.
    # INTEGER PRIMARY KEY AUTOINCREMENT гарантирует уникальность первичного ключа и автоинкремент.
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS departments (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            office TEXT NOT NULL
        );
    """)

    # Таблица 2:
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS employees (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            fio TEXT NOT NULL,
            position TEXT NOT NULL,
            salary REAL NOT NULL,
            department_id INTEGER,
            FOREIGN KEY (department_id) REFERENCES departments(id)
        );
    """)

    # Таблица 3: Проекты компании.
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS projects (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            budget REAL NOT NULL,
            status TEXT NOT NULL
        );
    """)

    # Таблица 4: Задачи по проектам (связь «один-ко-многим» с таблицами projects и employees).
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS tasks (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            priority TEXT NOT NULL,
            project_id INTEGER,
            employee_id INTEGER,
            FOREIGN KEY (project_id) REFERENCES projects(id),
            FOREIGN KEY (employee_id) REFERENCES employees(id)
        );
    """)

    # Таблица 5: Журнал трудозатрат сотрудников по задачам.
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS work_logs (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            employee_id INTEGER,
            task_id INTEGER,
            hours REAL NOT NULL,
            work_date TEXT NOT NULL,
            FOREIGN KEY (employee_id) REFERENCES employees(id),
            FOREIGN KEY (task_id) REFERENCES tasks(id)
        );
    """)

    # Любые операции изменения структуры (DDL) или данных (DML) в Python DB-API 2.0 открывают
    # транзакцию. Метод commit() фиксирует изменения на диске (обеспечивает свойство Durability из ACID).
    conn.commit()


# Задание 3: Функция выгрузки всех записей одной таблицы в формате JSON.
def get_all():
    cursor = conn.cursor()
    # Операция SELECT только читает данные и не меняет состояние БД, поэтому commit() здесь не нужен.
    cursor.execute("SELECT * FROM employees")

    results = []
    # Метод fetchall() извлекает все строки результата из буфера курсора в виде списка кортежей:
    # [(1, 'Иванов...', 'Backend...', 150000.0, 1), ...].
    rows = cursor.fetchall()

    # Преобразуем каждый кортеж в ассоциативный массив (словарь dict) с именованными ключами,
    # чтобы на выходе получился структурированный JSON-массив объектов.
    for row in rows:
        results.append(
            {
                "id": row[0],
                "fio": row[1],
                "position": row[2],
                "salary": row[3],
                "department_id": row[4],
            }
        )

    # indent=4 добавляет отступы для читаемости;
    # ensure_ascii=False запрещает экранирование кириллицы в коды вида \u0418, сохраняя русский текст.
    return json.dumps(results, indent=4, ensure_ascii=False)


# Задание 4: Функция параметризованного поиска по таблице.
def get_by_fio(fio):
    cursor = conn.cursor()

    # МЕТОДИЧЕСКОЕ ЗАМЕЧАНИЕ: В методичке показана подстановка через оператор '%s', однако в реальной
    # разработке так делать категорически нельзя из-за уязвимости SQL-инъекций (SQL Injection).
    # Правильный стандарт DB-API — передача параметров через плейсхолдер '?' и кортеж (fio,).
    # Запятая после fio обязательна, чтобы Python распознал выражение именно как кортеж из 1 элемента!
    cursor.execute("SELECT * FROM employees WHERE fio = ?", (fio,))

    results = []
    rows = cursor.fetchall()
    for row in rows:
        results.append(
            {
                "id": row[0],
                "fio": row[1],
                "position": row[2],
                "salary": row[3],
                "department_id": row[4],
            }
        )
    return json.dumps(results, indent=4, ensure_ascii=False)


# Задание 5: Функция создания новой записи в таблице (операция INSERT / Create в CRUD).
def create_record(fio, position, salary, department_id):
    cursor = conn.cursor()
    # Значение поля id не передаем, так как СУБД сгенерирует его автоматически (AUTOINCREMENT).
    cursor.execute(
        "INSERT INTO employees (fio, position, salary, department_id) VALUES (?, ?, ?, ?)",
        (fio, position, salary, department_id),
    )
    # Обязательно фиксируем транзакцию, иначе новая запись останется только в оперативной памяти.
    conn.commit()


# Задание 6: Функция редактирования существующей записи (операция UPDATE в CRUD).
def update_record(emp_id, new_position, new_salary):
    cursor = conn.cursor()
    # Внимание на условие WHERE id = ?: если его забыть, команда UPDATE перезапишет должность
    # и оклад абсолютно всем сотрудникам в таблице!
    cursor.execute(
        "UPDATE employees SET position = ?, salary = ? WHERE id = ?",
        (new_position, new_salary, emp_id),
    )
    conn.commit()


# Задание 7: Функция удаления записи по первичному ключу (операция DELETE в CRUD).
def delete_record(emp_id):
    cursor = conn.cursor()
    # Удаление по первичному ключу id гарантирует точечное удаление ровно одной целевой записи.
    cursor.execute("DELETE FROM employees WHERE id = ?", (emp_id,))
    conn.commit()


# Вспомогательная функция первичного наполнения всех 5 таблиц тестовыми данными (идемпотентная).
def seed_initial_data():
    cursor = conn.cursor()
    # Проверяем, пуста ли таблица отделов, чтобы при повторных запусках не дублировать стартовые записи.
    cursor.execute("SELECT COUNT(*) FROM departments")
    if cursor.fetchone()[0] == 0:
        # executemany позволяет выполнить один параметризованный запрос сразу для списка кортежей.
        cursor.executemany(
            "INSERT INTO departments (title, office) VALUES (?, ?)",
            [
                ("Разработка ПО", "Корпус А, каб. 301"),
                ("Аналитика и БД", "Корпус Б, каб. 204"),
            ],
        )
        create_record("Иванов Иван Иванович", "Backend-разработчик", 150000.0, 1)
        create_record("Петров Петр Сергеевич", "Администратор БД", 165000.0, 2)
        create_record("Сидорова Анна Алексеевна", "Системный аналитик", 140000.0, 2)

        cursor.execute(
            "INSERT INTO projects (name, budget, status) VALUES (?, ?, ?)",
            ("Корпоративный портал", 1200000.0, "В работе"),
        )
        cursor.execute(
            "INSERT INTO tasks (title, priority, project_id, employee_id) VALUES (?, ?, ?, ?)",
            ("Проектирование схемы БД", "Высокий", 1, 2),
        )
        cursor.execute(
            "INSERT INTO work_logs (employee_id, task_id, hours, work_date) VALUES (?, ?, ?, ?)",
            (2, 1, 6.5, "2026-11-24"),
        )
        conn.commit()


# Точка входа в программу: последовательная демонстрация всех функций из задания лабораторной работы.
if __name__ == "__main__":
    create_all_tables()
    seed_initial_data()

    print("--- Все сотрудники (get_all) ---")
    print(get_all())

    print("\n--- Поиск по ФИО (get_by_fio) ---")
    print(get_by_fio("Петров Петр Сергеевич"))

    # Демонстрация добавления (Create) -> получит id=4
    create_record("Козлов Дмитрий Олегович", "Junior Python Developer", 85000.0, 1)
    print("\n--- После добавления новой записи ---")
    print(get_all())

    # Демонстрация редактирования (Update) записи с id=4
    update_record(4, "Middle Python Developer", 130000.0)
    print("\n--- После редактирования записи с id=4 ---")
    print(get_by_fio("Козлов Дмитрий Олегович"))

    # Демонстрация удаления (Delete) записи с id=4
    delete_record(4)
    print("\n--- После удаления записи с id=4 ---")
    print(get_all())

    # Корректно закрываем соединение с БД в конце работы, чтобы снять блокировку файла
    # и освободить системные дескрипторы.
    conn.close()
