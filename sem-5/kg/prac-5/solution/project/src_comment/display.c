#include <GL/freeglut.h>

#include "display.h"
#include "constants.h"

// ============================================================================
// ОСНОВНАЯ ФУНКЦИЯ ОТРИСОВКИ СЦЕНЫ (DISPLAY)
// Содержит реализацию всех 4 обязательных типов примитивов:
// 1. GL_QUADS
// 2. GL_TRIANGLES
// 3. GL_LINES
// 4. GL_POINTS
// ============================================================================
void display() {
    // Очистка буфера цвета перед формированием нового кадра
    glClear(GL_COLOR_BUFFER_BIT);

    // ------------------------------------------------------------------------
    // ПРИМИТИВ 1: GL_QUADS (Внешнее голубое кольцо)
    // Кольцо аппроксимируется набором четырехугольников между R_INNER и R_OUTER
    // ------------------------------------------------------------------------
    glColor3f(COLOR_OUTER[0], COLOR_OUTER[1], COLOR_OUTER[2]);
    glBegin(GL_QUADS);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        // Углы текущего сектора в радианах
        float alpha1 = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        float alpha2 = 2.0f * PI * (float)(i + 1) / CIRCLE_SEGMENTS;

        // 4 вершины сегмента кольца: внутренний радиус -> внешний радиус
        glVertex2f(R_INNER * cosf(alpha1), R_INNER * sinf(alpha1));
        glVertex2f(R_OUTER * cosf(alpha1), R_OUTER * sinf(alpha1));
        glVertex2f(R_OUTER * cosf(alpha2), R_OUTER * sinf(alpha2));
        glVertex2f(R_INNER * cosf(alpha2), R_INNER * sinf(alpha2));
    }
    glEnd();

    // ------------------------------------------------------------------------
    // ПРИМИТИВ 2: GL_TRIANGLES
    // а) Внутренний розовый диск (веер треугольников от центра (0,0))
    // б) Желтые маркировочные треугольники на осях и окружности
    // ------------------------------------------------------------------------
    // а) Центральный круг
    glColor3f(COLOR_INNER[0], COLOR_INNER[1], COLOR_INNER[2]);
    glBegin(GL_TRIANGLES);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha1 = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        float alpha2 = 2.0f * PI * (float)(i + 1) / CIRCLE_SEGMENTS;

        glVertex2f(0.0f, 0.0f); // Центральная общая вершина
        glVertex2f(R_INNER * cosf(alpha1), R_INNER * sinf(alpha1));
        glVertex2f(R_INNER * cosf(alpha2), R_INNER * sinf(alpha2));
    }
    glEnd();

    // б) Желтые треугольники на внешней окружности (4 направления)
    glColor3f(COLOR_YELLOW[0], COLOR_YELLOW[1], COLOR_YELLOW[2]);
    glBegin(GL_TRIANGLES);
    // Треугольник на оси +Y (вершина вверх)
    glVertex2f(0.0f, R_OUTER + TRI_H);
    glVertex2f(-TRI_W / 2.0f, R_OUTER - 0.03f);
    glVertex2f(TRI_W / 2.0f, R_OUTER - 0.03f);

    // Треугольник на оси -Y (вершина вниз)
    glVertex2f(0.0f, -R_OUTER - TRI_H);
    glVertex2f(-TRI_W / 2.0f, -R_OUTER);
    glVertex2f(TRI_W / 2.0f, -R_OUTER);

    // Треугольник на оси +X (вершина вправо)
    glVertex2f(R_OUTER + TRI_H, 0.0f);
    glVertex2f(R_OUTER, -TRI_W / 2.0f);
    glVertex2f(R_OUTER, TRI_W / 2.0f);

    // Треугольник на оси -X (вершина влево)
    glVertex2f(-R_OUTER - TRI_H, 0.0f);
    glVertex2f(-R_OUTER, -TRI_W / 2.0f);
    glVertex2f(-R_OUTER, TRI_W / 2.0f);
    glEnd();

    // Вспомогательные стрелки вдоль оси Y (как на схеме задания)
    float midY = (R_INNER + R_OUTER) / 2.0f;
    // Верхняя стрелка (вверх)
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);
    glBegin(GL_TRIANGLES);
    glVertex2f(0.0f, midY + TRI_H * 0.5f);
    glVertex2f(-TRI_W * 0.3f, midY - TRI_H * 0.5f);
    glVertex2f(TRI_W * 0.3f, midY - TRI_H * 0.5f);
    glEnd();


    // Контуры желтых треугольников (для четкости, как на исходной схеме)
    glLineWidth(1.5f);
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);
    glBegin(GL_LINE_LOOP);
    glVertex2f(0.0f, R_OUTER + TRI_H);
    glVertex2f(-TRI_W / 2.0f, R_OUTER - 0.03f);
    glVertex2f(TRI_W / 2.0f, R_OUTER - 0.03f);
    glEnd();
    glBegin(GL_LINE_LOOP);
    glVertex2f(0.0f, -R_OUTER - TRI_H);
    glVertex2f(-TRI_W / 2.0f, -R_OUTER);
    glVertex2f(TRI_W / 2.0f, -R_OUTER);
    glEnd();
    glBegin(GL_LINE_LOOP);
    glVertex2f(R_OUTER + TRI_H, 0.0f);
    glVertex2f(R_OUTER, -TRI_W / 2.0f);
    glVertex2f(R_OUTER, TRI_W / 2.0f);
    glEnd();
    glBegin(GL_LINE_LOOP);
    glVertex2f(-R_OUTER - TRI_H, 0.0f);
    glVertex2f(-R_OUTER, -TRI_W / 2.0f);
    glVertex2f(-R_OUTER, TRI_W / 2.0f);
    glEnd();

    // ------------------------------------------------------------------------
    // ПРИМИТИВ 3: GL_LINES (Координатные оси и контуры окружностей)
    // ------------------------------------------------------------------------
    glLineWidth(LINE_WIDTH); // Установка толщины линий по ТЗ
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);

    glBegin(GL_LINES);
    // Ось X
    glVertex2f(-AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH, 0.0f);
    // Стрелка оси X
    glVertex2f(AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH - ARROW_HEAD_LEN, ARROW_HEAD_W);
    glVertex2f(AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH - ARROW_HEAD_LEN, -ARROW_HEAD_W);

    // Ось Y
    glVertex2f(0.0f, -AXIS_LENGTH);
    glVertex2f(0.0f, AXIS_LENGTH);
    // Стрелка оси Y
    glVertex2f(0.0f, AXIS_LENGTH);
    glVertex2f(-ARROW_HEAD_W, AXIS_LENGTH - ARROW_HEAD_LEN);
    glVertex2f(0.0f, AXIS_LENGTH);
    glVertex2f(ARROW_HEAD_W, AXIS_LENGTH - ARROW_HEAD_LEN);
    glEnd();

    // Внешняя контурная окружность
    glBegin(GL_LINE_LOOP);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        glVertex2f(R_OUTER * cosf(alpha), R_OUTER * sinf(alpha));
    }
    glEnd();

    // Внутренняя разделительная окружность
    glBegin(GL_LINE_LOOP);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        glVertex2f(R_INNER * cosf(alpha), R_INNER * sinf(alpha));
    }
    glEnd();

    // Буквы осей "X" и "Y" (отрисованы отрезками GL_LINES)
    glBegin(GL_LINES);
    // Символ 'X' около положительного конца оси X
    glVertex2f(AXIS_LENGTH + 0.03f, 0.03f);
    glVertex2f(AXIS_LENGTH + 0.07f, -0.03f);
    glVertex2f(AXIS_LENGTH + 0.03f, -0.03f);
    glVertex2f(AXIS_LENGTH + 0.07f, 0.03f);

    // Символ 'Y' около положительного конца оси Y
    glVertex2f(-0.02f, AXIS_LENGTH + 0.07f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.02f, AXIS_LENGTH + 0.07f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.02f);
    glEnd();

    // ------------------------------------------------------------------------
    // ПРИМИТИВ 4: GL_POINTS (Ключевые узловые точки на осях)
    // ------------------------------------------------------------------------
    glPointSize(POINT_SIZE); // Установка диаметра точек по ТЗ
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);

    glBegin(GL_POINTS);
    // Точка в начале координат (0, 0)
    glVertex2f(0.0f, 0.0f);

    // 4 точки пересечения осей с внутренней окружностью
    glVertex2f(R_INNER, 0.0f);
    glVertex2f(-R_INNER, 0.0f);
    glVertex2f(0.0f, R_INNER);
    glVertex2f(0.0f, -R_INNER);

    // Дополнительная точка на отрицательной полуоси Y (как на схеме)
    glVertex2f(0.0f, -(R_INNER + R_OUTER) / 2.0f);
    glEnd();

    // Принудительный сброс команд на видеокарту для немедленного вывода
    glFlush();
}
