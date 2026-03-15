#Badulin Ilya, group 424-1.
#First year, second semester.
#Laboratory class 7.
TARGET := main.exe
#TARGET_2 := 

SRC_FOLDER := ./src
LIB_SRC_FOLDER := ./lib/lab7/src##
LIB_INCLUDE_FOLDER := ./lib/lab7/include##

SRC_NAME := main
#SRC_NAME_2 := 
LIB_SRC_NAME := core
LIB_SRC_NAME_2 := utility
LIB_INCLUDE_NAME := lab7.h##
LIB_NAME := liblab7.a##

BUILD_DIR := ./build
OBJ_DIR := ./build/obj

CC := gcc
COMPILE_FLAGS := -Wall -Wextra -std=c99 -ggdb
INCLUDE_FLAGS := -I $(LIB_INCLUDE_FOLDER)

.PHONY: all prepare_dir clean

#all: prepare_dir $(BUILD_DIR)/$(TARGET) $(BUILD_DIR)/$(TARGET_2)
all: prepare_dir $(BUILD_DIR)/$(TARGET)

$(BUILD_DIR)/$(TARGET): $(OBJ_DIR)/$(SRC_NAME).o $(OBJ_DIR)/$(LIB_NAME)
	$(CC) $^ -o $@

#$(BUILD_DIR)/$(TARGET_2):$(OBJ_DIR)/$(SRC_NAME_2).o $(OBJ_DIR)/$(LIB_NAME)
#	$(CC) $^ -o $@

$(OBJ_DIR)/$(SRC_NAME).o: $(SRC_FOLDER)/$(SRC_NAME).c $(LIB_INCLUDE_FOLDER)/$(LIB_INCLUDE_NAME)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

#$(OBJ_DIR)/$(SRC_NAME_2).o: $(SRC_FOLDER)/$(SRC_NAME_2).c $(LIB_INCLUDE_FOLDER)/$(LIB_INCLUDE_NAME)
#	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

$(OBJ_DIR)/$(LIB_NAME): $(OBJ_DIR)/$(LIB_SRC_NAME).o $(OBJ_DIR)/$(LIB_SRC_NAME_2).o
	ar -rcs $@ $^

$(OBJ_DIR)/$(LIB_SRC_NAME).o: $(LIB_SRC_FOLDER)/$(LIB_SRC_NAME).c $(LIB_INCLUDE_FOLDER)/$(LIB_INCLUDE_NAME)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

$(OBJ_DIR)/$(LIB_SRC_NAME_2).o: $(LIB_SRC_FOLDER)/$(LIB_SRC_NAME_2).c $(LIB_INCLUDE_FOLDER)/$(LIB_INCLUDE_NAME)
	$(CC) -c $(COMPILE_FLAGS) $(INCLUDE_FLAGS) $< -o $@

prepare_dir: $(BUILD_DIR) $(OBJ_DIR)

$(BUILD_DIR):
	mkdir -p $(BUILD_DIR)

$(OBJ_DIR):
	mkdir -p $(OBJ_DIR)

clean:
	rm -fr $(BUILD_DIR)