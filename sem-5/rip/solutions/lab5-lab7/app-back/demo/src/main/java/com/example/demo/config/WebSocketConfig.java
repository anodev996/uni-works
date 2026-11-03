package com.example.demo.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.messaging.simp.config.MessageBrokerRegistry;
import org.springframework.web.socket.config.annotation.EnableWebSocketMessageBroker;
import org.springframework.web.socket.config.annotation.StompEndpointRegistry;
import org.springframework.web.socket.config.annotation.WebSocketMessageBrokerConfigurer;

@Configuration
@EnableWebSocketMessageBroker // Включает поддержку WebSockets и STOMP
public class WebSocketConfig implements WebSocketMessageBrokerConfigurer {

    @Override
    public void registerStompEndpoints(StompEndpointRegistry registry) {
        // Эндпоинт "/ws", к которому будет подключаться React
        registry.addEndpoint("/ws")
                .setAllowedOrigins("http://localhost:5173") // Разрешаем CORS для фронтенда
                .withSockJS(); // Резервный вариант (Fallback), если браузер не поддерживает WebSockets
    }

    @Override
    public void configureMessageBroker(MessageBrokerRegistry registry) {
        // Префикс "/topic" для каналов, на которые подписывается клиент (получение сообщений)
        registry.enableSimpleBroker("/topic");
        
        // Префикс "/app" для адресов, на которые клиент отправляет сообщения (отправка на сервер)
        registry.setApplicationDestinationPrefixes("/app");
    }
}