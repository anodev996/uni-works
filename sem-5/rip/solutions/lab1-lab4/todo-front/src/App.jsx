import { useEffect, useState } from "react";
import TaskForm from "./components/TaskForm";
import TaskItem from "./components/TaskItem";
import "./App.css"; // Обязательно импортируем наш файл со стилями

const API_URL = "http://localhost:8080/api/tasks";

export default function App() {
  const [tasks, setTasks] = useState([]);
  const [isDarkMode, setIsDarkMode] = useState(false); // false = белая тема

  // Добавляем функцию переключения (меняет false на true и обратно)
  const toggleTheme = () => setIsDarkMode(!isDarkMode);

  // Запускаем функцию загрузки один раз при монтировании компонента (пустой массив [] в конце)
  useEffect(() => {
    fetchTasks();
  }, []);

  // GET
  // `fetch(URL, setup obj)` -- GET по умолчанию
  const fetchTasks = async () => {
    try {
      const response = await fetch(API_URL); // GET-запрос по умолчанию
      const data = await response.json(); // Десериализация JSON в массив объектов
      setTasks(data); // Отдаем данные React'у для отрисовки
    } catch (error) {
      console.error("Ошибка при загрузке данных:", error);
    }
  };

  // POST
  const addTask = async (title) => {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ title: title, completed: false }),
    });

    const newTask = await response.json(); // Бэкенд возвращает созданную задачу с новым ID
    setTasks([...tasks, newTask]); // Добавляем её в интерфейс
  };

  // PUT, not PATCH because of idempotent
  const toggleTask = async (id) => {
    // Ищем задачу, на которую кликнули
    const taskToUpdate = tasks.find((task) => task.id === id);

    const response = await fetch(`${API_URL}/${id}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      // Отправляем старый текст, но инвертированный статус completed
      body: JSON.stringify({
        title: taskToUpdate.title,
        completed: !taskToUpdate.completed,
      }),
    });

    const updatedTask = await response.json();
    // Обновляем массив в памяти браузера ответом от сервера
    setTasks(tasks.map((task) => (task.id === id ? updatedTask : task)));
  };

  // DELETE
  const deleteTask = async (id) => {
    // Для удаления тело не нужно, только URL с ID
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });

    // Убираем удаленную задачу из интерфейса
    setTasks(tasks.filter((task) => task.id !== id));
  };

  return (
    // Главная магия: вешаем класс dark-theme, если включена темная тема
    <div className={`app-wrapper ${isDarkMode ? "dark-theme" : ""}`}>
      <div className="content-card">
        <div className="header">
          <h2>Мой To-Do Список</h2>
          <button className="theme-btn" onClick={toggleTheme}>
            Тема | {isDarkMode ? "🌙" : "☀️"}
          </button>
        </div>

        {/* Пропса isDarkMode больше нет! */}
        <TaskForm onAddTask={addTask} />

        <ul className="task-list">
          {tasks.length === 0 ? (
            <p className="empty-msg">Задач пока нет</p>
          ) : (
            tasks.map((task) => (
              <TaskItem
                key={task.id}
                task={task}
                onToggleTask={toggleTask}
                onDeleteTask={deleteTask}
              />
            ))
          )}
        </ul>
      </div>
    </div>
  );
}
