#include <windows.h>
#include <GL/gl.h>
#include <GL/freeglut.h>

#include "callbacks.h"

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