package com.example.demo.config;

import com.example.demo.repository.TaskRepository;
import com.example.demo.repository.UserRepository;
import org.springframework.shell.standard.ShellComponent;
import org.springframework.shell.standard.ShellMethod;

@ShellComponent
public class DatabaseCommands {

    private final TaskRepository taskRepository;
    private final UserRepository userRepository;

    public DatabaseCommands(TaskRepository taskRepository, UserRepository userRepository) {
        this.taskRepository = taskRepository;
        this.userRepository = userRepository;
    }

    @ShellMethod(key = "clear-db", value = "Fully clears all DB tables")
    public String clearDatabase() {
        long count = taskRepository.count() + userRepository.count();
        taskRepository.deleteAll();
        userRepository.deleteAll();

        return "Success! The DB tables are fully cleared. Entries deleted: " + count;
    }
}