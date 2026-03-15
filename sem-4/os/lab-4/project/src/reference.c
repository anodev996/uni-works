#include <pthread.h>
#include <stdlib.h>

int num_lines_per_cpu;
int num_cpus;

int main(int argc, char **argv)
{
    int cpu;
    pthread_t *thread_ids;
    //... Выполнить инициализации

    // Получить число процессоров
    num_cpus = _syspage_ptr->num_cpu;
    thread_ids = malloc(sizeof(pthread_t) * num_cpus);
    num_lines_per_cpu = num_x_lines / num_cpus;

    for (cpu = 0; cpu < num_cpus; cpu++) {
        pthread_create(&thread_ids[cpu], NULL, do_one_batch, (void *)cpu);
    }

    // Синхронизировать с завершением всех потоков
    for (cpu = 0; cpu < num_cpus; cpu++) {
        pthread_join(thread_ids[cpu], NULL);
    }

    //... Вывести результат
}

void *do_one_batch(void *c)
{
    int cpu = (int)c;
    int x1;
    for (x1 = 0; x1 < num_lines_per_cpu; x1++) {
        do_one_line(x1 + cpu * num_lines_per_cpu);
    }
}
