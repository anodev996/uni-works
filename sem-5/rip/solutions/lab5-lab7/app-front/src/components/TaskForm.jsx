import { useState } from "react";

// Больше не принимаем isDarkMode
export default function TaskForm({ onAddTask }) {
  const [title, setTitle] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!title.trim()) return;

    onAddTask(title.trim());
    setTitle("");
  };

  return (
    <form className="task-form" onSubmit={handleSubmit}>
      <input
        className="task-input"
        type="text"
        placeholder="Что нужно сделать?"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
      />
      <button className="add-btn" type="submit">
        Добавить
      </button>
    </form>
  );
}
