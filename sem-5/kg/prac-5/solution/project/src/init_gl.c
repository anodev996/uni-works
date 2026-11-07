#include "callbacks.h"

#include <windows.h>
#include <GL/gl.h>

#include "constants.h"

void initOpenGL(void) {
    glClearColor(COLOR_BG[0], COLOR_BG[1], COLOR_BG[2], 1.0f);

    glEnable(GL_BLEND);
    glEnable(GL_LINE_SMOOTH);
    glEnable(GL_POINT_SMOOTH);
}