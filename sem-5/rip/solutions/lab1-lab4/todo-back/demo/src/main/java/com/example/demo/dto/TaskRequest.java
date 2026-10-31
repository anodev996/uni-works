package com.example.demo.dto;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;

public class TaskRequest {

    // Поле не должно быть null и не должно состоять только из пробелов
    @NotBlank(message = "Название задачи не может быть пустым")
    @Size(min = 1, max = 100, message = "Название должно содержать от 2 до 100 символов")
    private String title;

    private boolean completed;

    // Геттеры и сеттеры
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    public boolean isCompleted() { return completed; }
    public void setCompleted(boolean completed) { this.completed = completed; }
}