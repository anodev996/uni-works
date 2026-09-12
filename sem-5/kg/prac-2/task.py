#!/bin/env python3
import tkinter as tk
import numpy as np


class KochSnowflakeApp:
    def __init__(self, root):
        self.root = root
        self.root.title("Снежинка Коха")
        self.root.geometry("800x700")
        self.root.resizable(False, False)

        self.canvas = tk.Canvas(root, width=780, height=620, bg="#E8D8C3")
        self.canvas.pack()

        self.button = tk.Button(
            root, text="Рисовать", command=self.draw_snowflake
        )
        self.button.pack(pady=10)

    def koch_curve(self, p1, p2, depth):
        if depth == 0:
            return [p1, p2]

        x1, y1 = p1
        x2, y2 = p2
        dx = x2 - x1
        dy = y2 - y1

        p_a = (x1 + dx / 3, y1 + dy / 3)  # 1/3
        p_b = (x1 + 2 * dx / 3, y1 + 2 * dy / 3)  # 2/3

        angle = np.pi / 3
        px = p_a[0] + (dx / 3) * np.cos(angle) - (dy / 3) * np.sin(angle)
        py = p_a[1] + (dx / 3) * np.sin(angle) + (dy / 3) * np.cos(angle)
        p_c = (px, py)

        points = []
        points.extend(self.koch_curve(p1, p_a, depth - 1)[:-1])
        points.extend(self.koch_curve(p_a, p_c, depth - 1)[:-1])
        points.extend(self.koch_curve(p_c, p_b, depth - 1)[:-1])
        points.extend(self.koch_curve(p_b, p2, depth - 1))
        return points

    def koch_snowflake(self, side_length=400, depth=4):
        h = side_length * np.sqrt(3) / 2
        cx, cy = 390, 320

        p1 = (cx - side_length / 2, cy + h / 3)
        p2 = (cx + side_length / 2, cy + h / 3)
        p3 = (cx, cy - 2 * h / 3)

        points = []
        points.extend(self.koch_curve(p1, p2, depth)[:-1])
        points.extend(self.koch_curve(p2, p3, depth)[:-1])
        points.extend(self.koch_curve(p3, p1, depth))
        return points

    def draw_snowflake(self):
        self.canvas.delete("all")
        points = self.koch_snowflake(side_length=400, depth=5)

        for i in range(len(points) - 1):
            x1, y1 = points[i]
            x2, y2 = points[i + 1]
            self.canvas.create_line(x1, y1, x2, y2, fill="black", width=1)

        x1, y1 = points[-1]
        x2, y2 = points[0]
        self.canvas.create_line(x1, y1, x2, y2, fill="black", width=1)


if __name__ == "__main__":
    root = tk.Tk()
    app = KochSnowflakeApp(root)
    root.mainloop()
