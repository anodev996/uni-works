import { useState, useEffect, useRef } from "react";
import SockJS from "sockjs-client";
import { Stomp } from "@stomp/stompjs";

export default function Chat({ username }) {
  const [messages, setMessages] = useState([]);
  const [currentMessage, setCurrentMessage] = useState("");
  const stompClientRef = useRef(null);

  useEffect(() => {
    const socket = new SockJS("http://localhost:8080/ws");
    const client = Stomp.over(socket);
    client.debug = () => {};

    client.connect(
      {},
      () => {
        client.subscribe("/topic/public", (payload) => {
          const message = JSON.parse(payload.body);
          setMessages((prev) => [...prev, message]);
        });
      },
      (error) => {
        console.error("Ошибка сокетов:", error);
      },
    );

    stompClientRef.current = client;

    return () => {
      if (stompClientRef.current) {
        stompClientRef.current.disconnect();
      }
    };
  }, []);

  const sendMessage = (e) => {
    e.preventDefault();
    if (currentMessage.trim() && stompClientRef.current) {
      const chatMessage = {
        sender: username,
        content: currentMessage,
      };

      stompClientRef.current.send(
        "/app/chat.sendMessage",
        {},
        JSON.stringify(chatMessage),
      );

      setCurrentMessage("");
    }
  };

  return (
    <div
      className="content-card"
      style={{
        display: "flex",
        flexDirection: "column",
        height: "600px",
        width: "100%",
        maxWidth: "600px",
      }}
    >
      <div className="header">
        <h2>Глобальный чат</h2>
        <span style={{ fontSize: "14px", color: "var(--text-muted)" }}>
          Вы: <b>{username}</b>
        </span>
      </div>

      <div className="chat-messages">
        {messages.length === 0 ? (
          <p className="empty-msg">Сообщений пока нет. Напишите первым!</p>
        ) : (
          messages.map((msg, index) => (
            <div key={index} className="chat-message">
              <b>{msg.sender}:</b> {msg.content}
            </div>
          ))
        )}
      </div>

      <form
        className="task-form"
        onSubmit={sendMessage}
        style={{ marginBottom: 0 }}
      >
        <input
          className="task-input"
          type="text"
          placeholder="Введите сообщение..."
          value={currentMessage}
          onChange={(e) => setCurrentMessage(e.target.value)}
        />
        <button className="add-btn" type="submit">
          Отправить
        </button>
      </form>
    </div>
  );
}
