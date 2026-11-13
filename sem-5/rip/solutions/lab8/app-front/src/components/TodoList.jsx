import { useEffect, useState } from "react";
import TaskForm from "./TaskForm";
import TaskItem from "./TaskItem";

const API_URL = "http://localhost:8080/api/tasks";

export default function TodoList({ token, onLogout }) {
  const [tasks, setTasks] = useState([]);

  const getAuthHeaders = () => ({
    "Content-Type": "application/json",
    Authorization: `Bearer ${token}`,
  });

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await fetch(API_URL, {
        headers: getAuthHeaders(),
      });
      if (response.status === 401 || response.status === 403) {
        onLogout();
        return;
      }
      const data = await response.json();
      setTasks(data);
    } catch (error) {
      console.error("Ошибка загрузки:", error);
    }
  };

  const addTask = async (title) => {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: getAuthHeaders(),
      body: JSON.stringify({ title, completed: false }),
    });
    if (response.ok) {
      const newTask = await response.json();
      setTasks([...tasks, newTask]);
    }
  };

  const toggleTask = async (id) => {
    const taskToUpdate = tasks.find((task) => task.id === id);
    const response = await fetch(`${API_URL}/${id}`, {
      method: "PUT",
      headers: getAuthHeaders(),
      body: JSON.stringify({
        title: taskToUpdate.title,
        completed: !taskToUpdate.completed,
      }),
    });
    if (response.ok) {
      const updatedTask = await response.json();
      setTasks(tasks.map((task) => (task.id === id ? updatedTask : task)));
    }
  };

  const deleteTask = async (id) => {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
      headers: getAuthHeaders(),
    });
    if (response.ok) {
      setTasks(tasks.filter((task) => task.id !== id));
    }
  };

  return (
    <div className="content-card" style={{ width: "100%", maxWidth: "450px" }}>
      <div className="header">
        <h2>Глобальный To-Do Список</h2>
      </div>
      <TaskForm onAddTask={addTask} />
      <ul className="task-list">
        {tasks.length === 0 ? (
          <p className="empty-msg">Задач пока нет</p>
        ) : (
          tasks.map((task) => (
            <TaskItem
              key={task.id}
              onDeleteTask={deleteTask}
              onToggleTask={toggleTask}
              task={task}
            />
          ))
        )}
      </ul>
    </div>
  );
}
