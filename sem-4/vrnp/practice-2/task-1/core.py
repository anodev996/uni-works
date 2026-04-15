#!/usr/bin/env python3


def check_balance(text: str) -> bool:
    """Doc."""
    matching = {")": "(", "]": "[", "}": "{"}
    opening = set(matching.values())

    stack = []

    for char in text:
        if char in opening:
            stack.append(char)
        elif char in matching:
            if not stack:
                return False

            last_open = stack.pop()

            if last_open != matching[char]:
                return False

    return len(stack) == 0


def get_examples() -> dict:
    """Doc."""
    return {
        "1": {"text": "function test() { return (a + b); }"},
        "2": {"text": "print('hello'))"},
    }
