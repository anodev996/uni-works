#pragma once

// Системные библиотеки
#include <Pt.h>
#include <stdint.h>
#include <sys/dispatch.h>
#include <sys/neutrino.h>

// Подключаем общий протокол с сервером
#include "protocol.h"

// --- Типы данных ---
typedef enum {
    STATE_UNREGISTERED = 0, // До регистрации
    STATE_REGISTERED,       // Зарегистрирован, ждет звонка
    STATE_RINGING,          // Входящий звонок
    STATE_IN_CALL           // Идет разговор
} app_state_t;

// --- Магия PhAB для глобальных переменных ---
// Если макрос DEFINE_GLOBALS определен, переменные инициализируются.
// В противном случае они помечаются как extern.
#ifdef DEFINE_GLOBALS
#define GLOBAL
#define INIT(x) = (x)
#else
#define GLOBAL extern
#define INIT(x)
#endif

// --- Глобальные переменные клиента ---
GLOBAL app_state_t current_state INIT(STATE_UNREGISTERED);
GLOBAL int my_phone INIT(0);
GLOBAL int bss_coid INIT(-1);
GLOBAL int target_phone INIT(0);
GLOBAL name_attach_t *attach INIT(NULL);

// --- Прототипы функций интерфейса (gui_helpers) ---
void console_print(const char *msg);
void set_btn(PtWidget_t *btn, int is_enabled, const char *text);
void update_fsm(app_state_t new_state);
