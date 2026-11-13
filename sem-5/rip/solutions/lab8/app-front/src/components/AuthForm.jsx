import { useState } from "react";

const AUTH_URL = "http://localhost:8080/api/auth";

export default function AuthForm({ onAuthSuccess }) {
  const [isLogin, setIsLogin] = useState(true);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (!username.trim() || !password.trim()) return;

    const endpoint = isLogin ? "/login" : "/register";

    try {
      const response = await fetch(`${AUTH_URL}${endpoint}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username: username.trim(), password }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        setError(errorText || "Ошибка авторизации");
        return;
      }

      const data = await response.json();
      onAuthSuccess(data.token, data.username);
    } catch (err) {
      console.error("Auth error:", err);
      setError(
        err instanceof TypeError
          ? "Внутренняя ошибка приложения"
          : "Не удалось подключиться к серверу",
      );
    }
  };

  return (
    <div
      className="content-card"
      style={{ width: "100%", maxWidth: "380px", marginTop: "60px" }}
    >
      <div className="header">
        <h2>{isLogin ? "Вход в систему" : "Регистрация"}</h2>
      </div>

      {error && (
        <p
          style={{
            color: "var(--danger-color)",
            marginBottom: "15px",
            fontSize: "14px",
          }}
        >
          {error}
        </p>
      )}

      <form
        onSubmit={handleSubmit}
        style={{ display: "flex", flexDirection: "column", gap: "12px" }}
      >
        <input
          className="task-input"
          type="text"
          placeholder="Имя пользователя"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          className="task-input"
          type="password"
          placeholder="Пароль"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button
          className="add-btn"
          type="submit"
          style={{ marginTop: "8px", padding: "10px" }}
        >
          {isLogin ? "Войти" : "Зарегистрироваться"}
        </button>
      </form>

      <div style={{ marginTop: "16px", textAlign: "center" }}>
        <button
          type="button"
          onClick={() => {
            setIsLogin(!isLogin);
            setError("");
          }}
          style={{
            background: "transparent",
            border: "none",
            color: "var(--text-muted)",
            cursor: "pointer",
            textDecoration: "underline",
          }}
        >
          {isLogin ? "Нет аккаунта? Создать" : "Уже есть аккаунт? Войти"}
        </button>
      </div>
    </div>
  );
}
