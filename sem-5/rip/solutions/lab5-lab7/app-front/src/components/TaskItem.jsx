export default function TaskItem({ task, onToggleTask, onDeleteTask }) {
  return (
    <li className="task-item">
      <label className="task-label">
        <input
          type="checkbox"
          checked={task.completed}
          onChange={() => onToggleTask(task.id)}
        />
        {/* Добавляем класс 'completed', если задача выполнена */}
        <span className={`task-text ${task.completed ? "completed" : ""}`}>
          {task.title}
        </span>
      </label>
      <button className="delete-btn" onClick={() => onDeleteTask(task.id)}>
        ✕
      </button>
    </li>
  );
}
