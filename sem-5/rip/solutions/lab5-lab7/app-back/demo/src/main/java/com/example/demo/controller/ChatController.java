package com.example.demo.controller;

import com.example.demo.dto.ChatMessage;
import org.springframework.messaging.handler.annotation.MessageMapping;
import org.springframework.messaging.handler.annotation.SendTo;
import org.springframework.stereotype.Controller;

@Controller
public class ChatController {

    // Метод сработает, когда React отправит сообщение на адрес "/app/chat.sendMessage"
    @MessageMapping("/chat.sendMessage")
    
    // Метод автоматически перешлет результат всем, кто подписан на "/topic/public"
    @SendTo("/topic/public")
    public ChatMessage sendMessage(ChatMessage message) {
        // В базовом варианте мы просто принимаем сообщение и сразу отправляем его всем участникам
        return message; 
    }
}