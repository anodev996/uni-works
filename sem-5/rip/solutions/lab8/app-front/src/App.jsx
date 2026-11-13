import { useState } from "react";
import TodoList from "./components/TodoList";
import Chat from "./components/Chat";
import AuthForm from "./components/AuthForm";
import "./App.css";

export default function App() {
  const [activeTab, setActiveTab] = useState("todo");
  const [isDarkMode, setIsDarkMode] = useState(false);

  const [token, setToken] = useState(localStorage.getItem("token") || null);
  const [username, setUsername] = useState(
    localStorage.getItem("username") || "",
  );

  const toggleTheme = () => setIsDarkMode(!isDarkMode);

  const handleAuthSuccess = (newToken, newUsername) => {
    localStorage.setItem("token", newToken);
    localStorage.setItem("username", newUsername);
    setToken(newToken);
    setUsername(newUsername);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    setToken(null);
    setUsername("");
  };

  return (
    <div className={`app-layout ${isDarkMode ? "dark-theme" : ""}`}>
      {token && (
        <div className="sidebar">
          <h1>Выбор приложения</h1>

          <button
            className={`nav-btn ${activeTab === "todo" ? "active" : ""}`}
            onClick={() => setActiveTab("todo")}
          >
            &gt; Заметки
          </button>

          <button
            className={`nav-btn ${activeTab === "chat" ? "active" : ""}`}
            onClick={() => setActiveTab("chat")}
          >
            &gt; Чат
          </button>

          <div className="spacer"></div>

          <button
            className="theme-btn"
            onClick={toggleTheme}
            style={{ marginBottom: "8px" }}
          >
            Тема | {isDarkMode ? "Тёмная" : "Светлая"}
          </button>

          <button
            className="theme-btn"
            onClick={handleLogout}
            style={{ backgroundColor: "#ff00008e", color: "#fff" }}
          >
            Выйти
          </button>
        </div>
      )}

      <div className="main-content">
        {!token ? (
          <AuthForm onAuthSuccess={handleAuthSuccess} />
        ) : (
          <>
            {activeTab === "todo" && (
              <TodoList onLogout={handleLogout} token={token} />
            )}
            {activeTab === "chat" && <Chat username={username} />}
          </>
        )}
      </div>
    </div>
  );
}
