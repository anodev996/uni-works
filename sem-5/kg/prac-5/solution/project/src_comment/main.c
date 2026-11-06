// opengl.dll и glu.dll уже крутятся в системных путях.
#include <GL/freeglut.h> // Основная библиотека FreeGLUT для создания окна и обработки событий
#include <math.h>        // Математическая библиотека для вычисления sin, cos и константы PI

//#include "../include/constants.h"
#include "constants.h"
#include "display.h"
#include "reshape.h"
 
// ============================================================================
// ФУНКЦИЯ ИНИЦИАЛИЗАЦИИ OPENGL
// ============================================================================
void initOpenGL() {
    // Установка цвета очистки экрана (фоновый цвет)
    glClearColor(COLOR_BG[0], COLOR_BG[1], COLOR_BG[2], 1.0f);

    // Включение сглаживания линий и точек для более аккуратного рендеринга
    glEnable(GL_BLEND);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
    glEnable(GL_LINE_SMOOTH);
    glEnable(GL_POINT_SMOOTH);
}

// ============================================================================
// ТОЧКА ВХОДА (MAIN)
// ============================================================================
int main(int argc, char** argv) {
    // Инициализация библиотеки GLUT
    glutInit(&argc, argv);

    // Режим отображения: одинарный буфер (статичная сцена) и цветовая модель RGBA
    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

    // Размеры и начальное положение окна
    glutInitWindowSize(650, 650);
    glutInitWindowPosition(150, 100);

    // Создание окна с заголовком работы
    glutCreateWindow("sem5-kg-prac5");

    // Вызов настроек параметров OpenGL
    initOpenGL();

    // Регистрация функций обратного вызова (колбэков)
    glutDisplayFunc(display);
    glutReshapeFunc(reshape);

    // Запуск бесконечного цикла обработки сообщений GLUT
    glutMainLoop();

    return 0;
}
