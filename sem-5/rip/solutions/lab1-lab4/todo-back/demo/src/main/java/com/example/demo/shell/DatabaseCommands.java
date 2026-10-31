package com.example.demo.shell;

import com.example.demo.repository.TaskRepository;
import org.springframework.shell.standard.ShellComponent;
import org.springframework.shell.standard.ShellMethod;

@ShellComponent
public class DatabaseCommands {

    private final TaskRepository repository;

    public DatabaseCommands(TaskRepository repository) {
        this.repository = repository;
    }

    // key — текст команды в консоли, value — описание для справки
    @ShellMethod(key = "clear-db", value = "Fully clears `tasks` table")
    public String clearDatabase() {
        long count = repository.count();
        repository.deleteAll();
        return "Success! The `tasks` tasble is fully cleared. Tasks deleted: " + count;
    }
}