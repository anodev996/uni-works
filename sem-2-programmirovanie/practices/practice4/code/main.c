//#define _CRT_SECURE_NO_WARNINGS
#define _USE_MATH_DEFINES
#include <stdio.h> 
#include <stdlib.h> 
#include <locale.h>
#include <math.h>

// Функция уравнения
double f(double x, unsigned int select)
{
    switch (select)
    {
    case 1:
        return pow(x, 4) - 0.48 * pow(x, 3) - 5.792 * pow(x, 2) + 0.486 * x + 4.792;
        break;

    case 2:
        return 0.1*sin(x) + pow(x,3) - 1;
        break;

    case 3:
        return 0.1*exp(x) - pow(sin(x),2) + 0.5;
        break;
    }
}

// Производная функции
double df(double x, unsigned int select)
{
    switch (select)
    {
    case 1:
        return 4 * pow(x, 3) - 1.44 * pow(x, 2) - 11.584 * x + 0.486;
        break;

    case 2:
        return 0.1*cos(x) + 3*pow(x,2);
        break;

    case 3:
        return 0.1*exp(x) - 2*sin(x)*cos(x);
        break;
    }
}
// Вторая производная от функции
double d2f(double x, unsigned int select) 
{
    switch (select)
    {
    case 1:
        return 12 * pow(x, 2) - 2.88 * x - 11.584;
        break;

    case 2:
        return 0.1*(-1)*sin(x) + 3*2*x;
        break;

    case 3:
        return 0.1*exp(x) - 2*(cos(x)*cos(x) + (-1)*sin(x)*sin(x));
        break;
    }
}
//Возвращает знак
double Sign(double d)
{
    if (d >= 0)
        return 1;
    return -1;
}

// Приближение
double GetApproximation(double start, double end, unsigned int select)
{
    double x = 0;
    if (Sign(df(start, select)) == Sign(d2f(start, select)))
    {
        printf("Знак первой и второй производной в точке %2.3lf совпадает\n", start);
        x = start;
    }
    else if (Sign(df(end, select)) == Sign(d2f(end, select)))
    {
        printf("Знак первой и второй производной в точке %2.3lf совпадает\n", end);
        x = end;
    }
    else
    {
        x = start;
    }
    printf("Выбрано приближение в точке %2.3lf\n", x);
    return x;
}

// Метод Ньютона для нахождения корня
double newton_method(double approximation,double epsilon, unsigned int select) 
{
    //Приближение
    double x = approximation;

    while(1)
    {
        double fx = f(x, select);
        double dfx = df(x, select);

        //Если |f(x_next)| < epsilon
        if (fabs(dfx) < epsilon) { 
            return x;
        }

        // x_next = x - f(x)/f(x)'
        double x_next = x - fx / dfx;
        
        //Если |x_next - x| < epsilon
        if (fabs(x_next - x) < epsilon) { // Проверка точности
            return x_next;
        }

        x = x_next;
    }
}

//Оценка погрешности //TODO ПРОВЕРИТЬ ЭТО 
double errorValue(double xn, double x0, unsigned int select)
{
    return f(xn, select) / fabs(x0 - xn);
}

int main()
{
    setlocale(LC_ALL, "RU");

    // Начальные приближения для разных корней
    double a, b;

    unsigned int select=0;
    printf("Выберите тип уравнения {1,2,3}:\n");
    scanf("%u", &select);
    if (1 > select || select > 3) exit(1);

    printf("Введите значение {a} интервала [a,b]:\n");
    scanf("%lf", &a);
    printf("Введите значение {b} интервала [a,b]:\n");
    scanf("%lf", &b);

    //Противоеблан
    if (a > b)
    {
        double save = b;
        b = a;
        a = save;
    }

    double epsilon = 1e-6;
    printf("Введите значения точности вычисления:\n");
    printf("%.2e\n", epsilon);
    //scanf("%lf", &epsilon);

    double aproximation = GetApproximation(a, b, select);
    printf("Поиск корней уравнения...\n");

    double root = newton_method(aproximation, epsilon, select);

    printf("Ответ: %2.3lf\n", root);
    printf("Погрешность: %12.2e\n", errorValue(root, aproximation, select));

    return 0;
}
