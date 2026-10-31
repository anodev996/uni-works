package com.example.demo.model;

import jakarta.persistence.*;

@Entity
@Table(name = "tasks") // Имя таблицы в БД
public class Task {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id; // Первичный ключ, генерируется автоматически

    @Column(nullable = false)
    private String title; // Название задачи, не может быть пустым

    private boolean completed = false; // Статус выполнения

    // Пустой конструктор обязателен для работы Hibernate
    public Task() {}

    public Task(String title) {
        this.title = title;
    }

    // Геттеры и сеттеры
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }
    public String getTitle() { return title; }
    public void setTitle(String title) { this.title = title; }
    public boolean isCompleted() { return completed; }
    public void setCompleted(boolean completed) { this.completed = completed; }
}