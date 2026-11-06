#include <GL/freeglut.h>

#include "reshape.h"

// ============================================================================
// ФУНКЦИЯ ОБРАБОТКИ ИЗМЕНЕНИЯ РАЗМЕРОВ ОКНА (RESHAPE)
// Обеспечивает сохранение пропорций (aspect ratio) сцены без искажений
// ============================================================================
void reshape(int width, int height) {
    // Предотвращение деления на ноль при сворачивании окна
    if (height == 0) height = 1;

    // Область вывода OpenGL растягивается на все окно
    glViewport(0, 0, width, height);

    // Переключаемся на матрицу проекции для настройки системы координат
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();

    // Вычисляем соотношение сторон окна
    float aspect = (float)width / (float)height;
    const float sceneRange = 1.15f; // Видимая граница сцены от центра

    // Корректируем ортогональную проекцию, чтобы круг не превращался в эллипс
    if (width >= height) {
        gluOrtho2D(-sceneRange * aspect, sceneRange * aspect, -sceneRange, sceneRange);
    }
    else {
        gluOrtho2D(-sceneRange, sceneRange, -sceneRange / aspect, sceneRange / aspect);
    }

    // Возвращаем режим матрицы в видовую (для рисования объектов)
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}
