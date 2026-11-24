import sqlite3
import json

conn = sqlite3.connect("it_company.sqlite")
conn.execute("PRAGMA foreign_keys = ON;")


def create_all_tables():
    cursor = conn.cursor()
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS departments (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            office TEXT NOT NULL
        );
    """)
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
    cursor.execute("""
        CREATE TABLE IF NOT EXISTS projects (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            budget REAL NOT NULL,
            status TEXT NOT NULL
        );
    """)
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
    conn.commit()


def get_all():
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM employees")
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


def get_by_fio(fio):
    cursor = conn.cursor()
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


def create_record(fio, position, salary, department_id):
    cursor = conn.cursor()
    cursor.execute(
        "INSERT INTO employees (fio, position, salary, department_id) VALUES (?, ?, ?, ?)",
        (fio, position, salary, department_id),
    )
    conn.commit()


def update_record(emp_id, new_position, new_salary):
    cursor = conn.cursor()
    cursor.execute(
        "UPDATE employees SET position = ?, salary = ? WHERE id = ?",
        (new_position, new_salary, emp_id),
    )
    conn.commit()


def delete_record(emp_id):
    cursor = conn.cursor()
    cursor.execute("DELETE FROM employees WHERE id = ?", (emp_id,))
    conn.commit()


def seed_initial_data():
    cursor = conn.cursor()
    cursor.execute("SELECT COUNT(*) FROM departments")
    if cursor.fetchone()[0] == 0:
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


if __name__ == "__main__":
    create_all_tables()
    seed_initial_data()

    print("--- Все сотрудники (get_all) ---")
    print(get_all())

    print("\n--- Поиск по ФИО (get_by_fio) ---")
    print(get_by_fio("Петров Петр Сергеевич"))

    create_record("Козлов Дмитрий Олегович", "Junior Python Developer", 85000.0, 1)
    print("\n--- После добавления новой записи ---")
    print(get_all())

    update_record(4, "Middle Python Developer", 130000.0)
    print("\n--- После редактирования записи с id=4 ---")
    print(get_by_fio("Козлов Дмитрий Олегович"))

    delete_record(4)
    print("\n--- После удаления записи с id=4 ---")
    print(get_all())

    conn.close()
