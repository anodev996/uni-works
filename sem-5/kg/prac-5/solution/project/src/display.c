#include "callbacks.h"

#include <windows.h>
#include <GL/gl.h>
#include <math.h>

#include "constants.h"

void display(void) {
    glClear(GL_COLOR_BUFFER_BIT);

    glColor3f(COLOR_OUTER[0], COLOR_OUTER[1], COLOR_OUTER[2]);
    glBegin(GL_QUADS);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha1 = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        float alpha2 = 2.0f * PI * (float)(i + 1) / CIRCLE_SEGMENTS;

        glVertex2f(R_INNER * cosf(alpha1), R_INNER * sinf(alpha1));
        glVertex2f(R_OUTER * cosf(alpha1), R_OUTER * sinf(alpha1));
        glVertex2f(R_OUTER * cosf(alpha2), R_OUTER * sinf(alpha2));
        glVertex2f(R_INNER * cosf(alpha2), R_INNER * sinf(alpha2));
    }
    glEnd();

    glColor3f(COLOR_INNER[0], COLOR_INNER[1], COLOR_INNER[2]);
    glBegin(GL_TRIANGLES);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha1 = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        float alpha2 = 2.0f * PI * (float)(i + 1) / CIRCLE_SEGMENTS;

        glVertex2f(0.0f, 0.0f);
        glVertex2f(R_INNER * cosf(alpha1), R_INNER * sinf(alpha1));
        glVertex2f(R_INNER * cosf(alpha2), R_INNER * sinf(alpha2));
    }
    glEnd();

    glColor3f(COLOR_YELLOW[0], COLOR_YELLOW[1], COLOR_YELLOW[2]);
    glBegin(GL_TRIANGLES);
    glVertex2f(0.0f, R_OUTER + TRI_H);
    glVertex2f(-TRI_W / 2.0f, R_OUTER - 0.03f);
    glVertex2f(TRI_W / 2.0f, R_OUTER - 0.03f);

    glVertex2f(0.0f, -R_OUTER - TRI_H);
    glVertex2f(-TRI_W / 2.0f, -R_OUTER);
    glVertex2f(TRI_W / 2.0f, -R_OUTER);

    glVertex2f(R_OUTER + TRI_H, 0.0f);
    glVertex2f(R_OUTER, -TRI_W / 2.0f);
    glVertex2f(R_OUTER, TRI_W / 2.0f);

    glVertex2f(-R_OUTER - TRI_H, 0.0f);
    glVertex2f(-R_OUTER, -TRI_W / 2.0f);
    glVertex2f(-R_OUTER, TRI_W / 2.0f);
    glEnd();

    float midY = (R_INNER + R_OUTER) / 2.0f;
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);
    glBegin(GL_TRIANGLES);
    glVertex2f(0.0f, midY + TRI_H * 0.5f);
    glVertex2f(-TRI_W * 0.3f, midY - TRI_H * 0.5f);
    glVertex2f(TRI_W * 0.3f, midY - TRI_H * 0.5f);
    glEnd();

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

    glLineWidth(LINE_WIDTH);
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);

    glBegin(GL_LINES);
    glVertex2f(-AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH - ARROW_HEAD_LEN, ARROW_HEAD_W);
    glVertex2f(AXIS_LENGTH, 0.0f);
    glVertex2f(AXIS_LENGTH - ARROW_HEAD_LEN, -ARROW_HEAD_W);

    glVertex2f(0.0f, -AXIS_LENGTH);
    glVertex2f(0.0f, AXIS_LENGTH);
    glVertex2f(0.0f, AXIS_LENGTH);
    glVertex2f(-ARROW_HEAD_W, AXIS_LENGTH - ARROW_HEAD_LEN);
    glVertex2f(0.0f, AXIS_LENGTH);
    glVertex2f(ARROW_HEAD_W, AXIS_LENGTH - ARROW_HEAD_LEN);
    glEnd();

    glBegin(GL_LINE_LOOP);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        glVertex2f(R_OUTER * cosf(alpha), R_OUTER * sinf(alpha));
    }
    glEnd();

    glBegin(GL_LINE_LOOP);
    for (int i = 0; i < CIRCLE_SEGMENTS; ++i) {
        float alpha = 2.0f * PI * (float)i / CIRCLE_SEGMENTS;
        glVertex2f(R_INNER * cosf(alpha), R_INNER * sinf(alpha));
    }
    glEnd();

    glBegin(GL_LINES);
    glVertex2f(AXIS_LENGTH + 0.03f, 0.03f);
    glVertex2f(AXIS_LENGTH + 0.07f, -0.03f);
    glVertex2f(AXIS_LENGTH + 0.03f, -0.03f);
    glVertex2f(AXIS_LENGTH + 0.07f, 0.03f);

    glVertex2f(-0.02f, AXIS_LENGTH + 0.07f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.02f, AXIS_LENGTH + 0.07f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.045f);
    glVertex2f(0.0f, AXIS_LENGTH + 0.02f);
    glEnd();

    glPointSize(POINT_SIZE);
    glColor3f(COLOR_BLACK[0], COLOR_BLACK[1], COLOR_BLACK[2]);

    glBegin(GL_POINTS);
    glVertex2f(0.0f, 0.0f);

    glVertex2f(R_INNER, 0.0f);
    glVertex2f(-R_INNER, 0.0f);
    glVertex2f(0.0f, R_INNER);
    glVertex2f(0.0f, -R_INNER);

    glVertex2f(0.0f, -(R_INNER + R_OUTER) / 2.0f);
    glEnd();

    glFlush();
}