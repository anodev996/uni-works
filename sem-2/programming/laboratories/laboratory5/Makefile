#Makefile to build the whole project
TARGET_EXE := lab5_main.exe
TARGET_EXE_2 := lab5_main2.exe

BUILD_DIR := ./build

CC := gcc
COMPILE_FLAGS := -Wall -Wextra -std=c99
INCLUDE_FLAGS := -I ./lib/lab5/include
INCLUDE_FILE := ./lib/lab5/include/lab5.h

.PHONY: all prepare_dir build_1 build_2 clean

all: prepare_dir $(BUILD_DIR)/$(TARGET_EXE) $(BUILD_DIR)/$(TARGET_EXE_2)
build_1: prepare_dir $(BUILD_DIR)/$(TARGET_EXE)
build_2: prepare_dir $(BUILD_DIR)/$(TARGET_EXE_2)

$(BUILD_DIR)/$(TARGET_EXE): $(BUILD_DIR)/lab5_main.o $(BUILD_DIR)/liblab5.a
	$(CC) $^ -o $(BUILD_DIR)/$(TARGET_EXE)

$(BUILD_DIR)/$(TARGET_EXE_2): $(BUILD_DIR)/lab5_main2.o $(BUILD_DIR)/liblab5.a
	$(CC) $^ -o $(BUILD_DIR)/$(TARGET_EXE_2)

$(BUILD_DIR)/lab5_main.o: ./src/lab5_main.c $(INCLUDE_FILE)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

$(BUILD_DIR)/lab5_main2.o: ./src/lab5_main2.c $(INCLUDE_FILE)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

$(BUILD_DIR)/liblab5.a: $(BUILD_DIR)/helpful_functions.o $(BUILD_DIR)/lab5_functions.o 
	ar -rcs $@ $^

$(BUILD_DIR)/helpful_functions.o: ./lib/lab5/src/helpful_functions.c $(INCLUDE_FILE)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

$(BUILD_DIR)/lab5_functions.o: ./lib/lab5/src/lab5_functions.c $(INCLUDE_FILE)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

prepare_dir: $(BUILD_DIR)

$(BUILD_DIR):
	mkdir -p $(BUILD_DIR)

clean:
	rm -fr $(BUILD_DIR)