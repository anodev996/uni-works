package com.example.demo.service;

import com.example.demo.model.Task;
import com.example.demo.repository.TaskRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class TaskService {
    
    private final TaskRepository repository;

    // Внедрение зависимости через конструктор (лучшая практика)
    public TaskService(TaskRepository repository) {
        this.repository = repository;
    }

    // READ (Все задачи)
    public List<Task> getAllTasks() {
        return repository.findAll();
    }

    // READ (Одна задача)
    public Task getTaskById(Long id) {
        return repository.findById(id)
                .orElseThrow(() -> new RuntimeException("Задача с id " + id + " не найдена"));
    }

    // CREATE
    public Task createTask(Task task) {
        return repository.save(task);
    }

    // UPDATE
    public Task updateTask(Long id, Task taskDetails) {
        Task task = getTaskById(id);
        task.setTitle(taskDetails.getTitle());
        task.setCompleted(taskDetails.isCompleted());
        return repository.save(task); // save() работает и на создание, и на обновление
    }

    // DELETE
    public void deleteTask(Long id) {
        repository.deleteById(id);
    }
}