#include "callbacks.h"

#include <windows.h>
#include <GL/gl.h>
#include <GL/glu.h>

#include "constants.h"

void reshape(int width, int height) {
    if (height == 0) height = 1;

    glViewport(0, 0, width, height);

    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();

    float aspect = (float)width / (float)height;

    if (width >= height) {
        gluOrtho2D(-SCENE_RANGE * aspect, SCENE_RANGE * aspect, -SCENE_RANGE, SCENE_RANGE);
    }
    else {
        gluOrtho2D(-SCENE_RANGE, SCENE_RANGE, -SCENE_RANGE / aspect, SCENE_RANGE / aspect);
    }

    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}