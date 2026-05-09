namespace project;

// Represents a logical move between two rods.
public readonly record struct Move(int FromRod, int ToRod);