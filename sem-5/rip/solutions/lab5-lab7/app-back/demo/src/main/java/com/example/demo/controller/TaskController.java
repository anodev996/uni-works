package com.example.demo.controller;

import com.example.demo.dto.TaskRequest;
import com.example.demo.model.Task;
import com.example.demo.service.TaskService;
import jakarta.validation.Valid;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/tasks") // Базовый URL для всех эндпоинтов в этом классе
public class TaskController {

    private final TaskService service;

    public TaskController(TaskService service) {
        this.service = service;
    }

    @GetMapping
    public List<Task> getAllTasks() {
        return service.getAllTasks();
    }

    @GetMapping("/{id}")
    public Task getTaskById(@PathVariable Long id) {
        return service.getTaskById(id);
    }

    @PostMapping
    public Task createTask(@Valid @RequestBody TaskRequest request) {
        // Преобразуем проверенный DTO в сущность базы данных
        Task task = new Task(request.getTitle());
        task.setCompleted(request.isCompleted());
        return service.createTask(task);
    }

    @PutMapping("/{id}")
    public Task updateTask(@PathVariable Long id, @Valid @RequestBody TaskRequest request) {
        Task taskDetails = new Task(request.getTitle());
        taskDetails.setCompleted(request.isCompleted());
        return service.updateTask(id, taskDetails);
    }

    @DeleteMapping("/{id}")
    public void deleteTask(@PathVariable Long id) {
        service.deleteTask(id);
    }
}