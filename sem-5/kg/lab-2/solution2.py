#!/usr/bin/env python3

import tkinter as tk
from tkinter import messagebox
from PIL import Image, ImageTk
import numpy as np

WIDTH = 900
HEIGHT = 520
KMAX = 100
RMIN = 2.0

X0, XN = -2.0, 0.8
Y0, YN = -1.0, 1.0


class MandelbrotApp:

    def __init__(self, root):
        self.root = root
        self.root.title("Фрактал Мандельброта — Вариант 2")
        self.root.geometry("920x580")
        self.root.resizable(False, False)

        self.image_tk = None

        self._build_ui()

    def _build_ui(self):
        frame_top = tk.Frame(self.root)
        frame_top.pack(fill="x", padx=10, pady=8)

        tk.Button(
            frame_top, text="Построить", command=self.build_fractal, width=15
        ).pack(side="left")

        info = f"Границы:  x = [{X0}; {XN}],  y = [{Y0}; {YN}]"
        tk.Label(frame_top, text=info, foreground="black").pack(
            side="left", padx=(15, 0)
        )

        self.canvas = tk.Canvas(
            self.root,
            bg="white",
            width=WIDTH,
            height=HEIGHT,
            highlightthickness=0,
        )
        self.canvas.pack(padx=10, pady=(0, 8))

    def _compute_mandelbrot(self, width, height, x0, xn, y0, yn, kmax, rmin):
        xs = np.linspace(x0, xn, width)
        ys = np.linspace(y0, yn, height)

        CX, CY = np.meshgrid(xs, ys)
        ZX = np.zeros_like(CX, dtype=np.float64)
        ZY = np.zeros_like(CY, dtype=np.float64)
        M = np.full(CX.shape, kmax, dtype=np.int32)

        r2 = rmin * rmin

        for k in range(1, kmax + 1):
            ZX_new = ZX * ZX - ZY * ZY + CX
            ZY_new = 2.0 * ZX * ZY + CY
            ZX, ZY = ZX_new, ZY_new

            mask = (ZX * ZX + ZY * ZY > r2) & (M == kmax)
            M[mask] = k

            if not np.any(M == kmax):
                break

        return M

    def _colorize(M, kmax):
        h, w = M.shape
        img = np.full((h, w, 3), 255, dtype=np.uint8)

        inside = M == kmax
        img[inside] = (0, 0, 255)

        outside = ~inside
        t = np.zeros_like(M, dtype=np.float32)
        t[outside] = M[outside] / kmax

        gamma = 0.7
        t = np.power(t, gamma)

        r = (255 * (1 - t)).astype(np.uint8)
        g = (255 * (1 - t)).astype(np.uint8)
        b = np.full_like(r, 255, dtype=np.uint8)

        img[outside, 0] = r[outside]
        img[outside, 1] = g[outside]
        img[outside, 2] = b[outside]

        return img

    def build_fractal(self):
        self.root.update_idletasks()

        M = self._compute_mandelbrot(WIDTH, HEIGHT, X0, XN, Y0, YN, KMAX, RMIN)
        rgb = self._colorize(self, K, KMAX)

        img = Image.fromarray(rgb, mode="RGB")
        self.image_tk = ImageTk.PhotoImage(img)

        self.canvas.delete("all")
        self.canvas.create_image(0, 0, anchor="nw", image=self.image_tk)


if __name__ == "__main__":
    root = tk.Tk()
    app = MandelbrotApp(root)
    root.mainloop()
