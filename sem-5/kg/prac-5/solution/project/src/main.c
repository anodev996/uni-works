#include <windows.h>
#include <GL/gl.h>
#include <GL/freeglut.h>

#include "callbacks.h"
#include "constants.h"

static void initOpenGL(void) {
    glClearColor(COLOR_BG[0], COLOR_BG[1], COLOR_BG[2], 1.0f);

    glEnable(GL_BLEND);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
    glEnable(GL_LINE_SMOOTH);
    glEnable(GL_POINT_SMOOTH);
}

int main(int argc, char** argv) {
    glutInit(&argc, argv);

    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);

    glutInitWindowSize(650, 650);
    glutInitWindowPosition(150, 100);

    glutCreateWindow("Sem5KgPrac5");

    initOpenGL();

    glutDisplayFunc(display);
    glutReshapeFunc(reshape);

    glutMainLoop();

    return 0;
}