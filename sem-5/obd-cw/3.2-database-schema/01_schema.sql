-- ==========================================
-- ЧАСТЬ 1: СПРАВОЧНИКИ-КЛАССИФИКАТОРЫ
-- ==========================================

CREATE TABLE модель_авто (
    ид_модели SERIAL PRIMARY KEY,
    марка VARCHAR(100) NOT NULL,
    модель VARCHAR(100) NOT NULL
);

CREATE TABLE тип_кузова (
    ид_типа_кузова SERIAL PRIMARY KEY,
    наименование_типа_кузова VARCHAR(100) NOT NULL
);

CREATE TABLE тип_привода (
    ид_типа_привода SERIAL PRIMARY KEY,
    наименование_типа_привода VARCHAR(100) NOT NULL
);

CREATE TABLE тип_кпп (
    ид_типа_кпп SERIAL PRIMARY KEY,
    наименование_типа_кпп VARCHAR(100) NOT NULL
);

CREATE TABLE цвет (
    ид_цвета SERIAL PRIMARY KEY,
    наименование_цвета VARCHAR(100) NOT NULL,
    хекс_код VARCHAR(7)
);

CREATE TABLE страна_производства (
    ид_страны SERIAL PRIMARY KEY,
    название_страны VARCHAR(100) NOT NULL
);

CREATE TABLE статус_автомобиля (
    ид_статуса SERIAL PRIMARY KEY,
    наименование_статуса VARCHAR(100) NOT NULL
);

CREATE TABLE должность (
    ид_должности SERIAL PRIMARY KEY,
    наименование_должности VARCHAR(100) NOT NULL
);

CREATE TABLE вид_запчасти (
    ид_вида_запчасти SERIAL PRIMARY KEY,
    наименование_вида_запчасти VARCHAR(100) NOT NULL
);

CREATE TABLE тип_услуги (
    ид_типа_услуги SERIAL PRIMARY KEY,
    наименование_услуги VARCHAR(255) NOT NULL,
    базовая_стоимость NUMERIC(12, 2) NOT NULL
);

-- Классификатор 2-го уровня (ссылается на вид_запчасти)
CREATE TABLE наименование_запчасти (
    ид_наименования SERIAL PRIMARY KEY,
    ид_вида_запчасти INTEGER NOT NULL REFERENCES вид_запчасти(ид_вида_запчасти),
    наименование_запчасти VARCHAR(255) NOT NULL,
    артикул VARCHAR(100) UNIQUE NOT NULL
);


-- ==========================================
-- ЧАСТЬ 2: ПОЛЬЗОВАТЕЛИ И СОТРУДНИКИ
-- ==========================================

CREATE TABLE клиент (
    ид_клиента SERIAL PRIMARY KEY,
    фамилия VARCHAR(100) NOT NULL,
    имя VARCHAR(100) NOT NULL,
    отчество VARCHAR(100), -- Может быть NULL
    электронная_почта VARCHAR(255) UNIQUE NOT NULL,
    номер_телефона VARCHAR(20) NOT NULL,
    хеш_пароля VARCHAR(255) NOT NULL
);

CREATE TABLE сотрудник (
    ид_сотрудника SERIAL PRIMARY KEY,
    ид_должности INTEGER NOT NULL REFERENCES должность(ид_должности),
    фамилия VARCHAR(100) NOT NULL,
    имя VARCHAR(100) NOT NULL,
    отчество VARCHAR(100), -- Может быть NULL
    номер_телефона VARCHAR(20) NOT NULL
);


-- ==========================================
-- ЧАСТЬ 3: АВТОМОБИЛИ
-- ==========================================

CREATE TABLE автомобиль (
    ид_автомобиля SERIAL PRIMARY KEY,
    ид_модели INTEGER NOT NULL REFERENCES модель_авто(ид_модели),
    ид_типа_кузова INTEGER NOT NULL REFERENCES тип_кузова(ид_типа_кузова),
    ид_типа_привода INTEGER NOT NULL REFERENCES тип_привода(ид_типа_привода),
    ид_типа_кпп INTEGER NOT NULL REFERENCES тип_кпп(ид_типа_кпп),
    ид_цвета INTEGER NOT NULL REFERENCES цвет(ид_цвета),
    ид_страны INTEGER NOT NULL REFERENCES страна_производства(ид_страны),
    ид_статуса INTEGER NOT NULL REFERENCES статус_автомобиля(ид_статуса),
    vin_номер VARCHAR(17) UNIQUE NOT NULL,
    год_выпуска INTEGER NOT NULL,
    пробег INTEGER DEFAULT 0 NOT NULL,
    цена NUMERIC(12, 2) NOT NULL,
    фотография BYTEA -- Бинарный тип для хранения изображений
);


-- ==========================================
-- ЧАСТЬ 4: БИЗНЕС-ПРОЦЕССЫ И ДОКУМЕНТЫ
-- ==========================================

CREATE TABLE заявка_на_тест_драйв (
    ид_заявки SERIAL PRIMARY KEY,
    ид_автомобиля INTEGER NOT NULL REFERENCES автомобиль(ид_автомобиля),
    ид_клиента INTEGER NOT NULL REFERENCES клиент(ид_клиента),
    дата_тест_драйва DATE NOT NULL,
    время_тест_драйва TIME NOT NULL,
    комментарий TEXT -- Может быть NULL
);

CREATE TABLE платеж_бронирования (
    ид_платежа SERIAL PRIMARY KEY,
    ид_автомобиля INTEGER NOT NULL REFERENCES автомобиль(ид_автомобиля),
    сумма_платежа NUMERIC(12, 2) NOT NULL,
    дата_время_платежа TIMESTAMP NOT NULL,
    статус_платежа VARCHAR(50) NOT NULL
);

CREATE TABLE договор_купли_продажи (
    ид_договора SERIAL PRIMARY KEY,
    ид_автомобиля INTEGER NOT NULL REFERENCES автомобиль(ид_автомобиля),
    ид_клиента INTEGER NOT NULL REFERENCES клиент(ид_клиента),
    ид_сотрудника INTEGER NOT NULL REFERENCES сотрудник(ид_сотрудника),
    дата_время_сделки TIMESTAMP NOT NULL,
    итоговая_сумма NUMERIC(12, 2) NOT NULL,
    тип_расчета VARCHAR(50) NOT NULL
);

CREATE TABLE заказ_наряд (
    ид_заказа SERIAL PRIMARY KEY,
    ид_клиента INTEGER NOT NULL REFERENCES клиент(ид_клиента),
    ид_сотрудника INTEGER NOT NULL REFERENCES сотрудник(ид_сотрудника),
    vin_автомобиля_клиента VARCHAR(17) NOT NULL,
    дата_заезда DATE NOT NULL,
    планируемая_дата_выдачи DATE, -- Может быть NULL
    статус_готовности VARCHAR(50) NOT NULL
);

-- ==========================================
-- ЧАСТЬ 5: ДЕТАЛИЗАЦИЯ РЕМОНТА (РАЗРЕШЕНИЕ M:N)
-- ==========================================

CREATE TABLE детализация_ремонта (
    ид_детализации SERIAL PRIMARY KEY,
    ид_заказа INTEGER NOT NULL REFERENCES заказ_наряд(ид_заказа) ON DELETE CASCADE,
    ид_типа_услуги INTEGER REFERENCES тип_услуги(ид_типа_услуги),     -- NULLABLE
    ид_наименования INTEGER REFERENCES наименование_запчасти(ид_наименования), -- NULLABLE
    количество_запчастей INTEGER DEFAULT 1 NOT NULL,
    стоимость NUMERIC(12, 2) NOT NULL
);
