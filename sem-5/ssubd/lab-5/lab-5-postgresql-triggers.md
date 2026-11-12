# lab-5-postgresql-triggers

### 1. Анализ задания (по методичке)

Цель работы — изучить процедурный язык **PL/pgSQL** в PostgreSQL и научиться писать хранимые функции и триггеры.

  

Согласно разделу 4 методички, к сдаче предъявляются три требования:

  

1. Написать триггер, применимый к собственной базе данных.
    
      
    
2. **Не копировать пример с логгированием** из раздела 3 (запись текстовых логов в таблицу `logs` при `INSERT/UPDATE/DELETE` преподаватель не примет).
    
      
    
3. При защите объяснить логику работы триггера и продемонстрировать его выполнение.
    
      
    

### 2. Простая схема БД и логика триггера

Чтобы не перегружать базу, возьмём предметную область **«Мини-склад / учёт заказов»** из двух связанных таблиц:

  

- `products` — товары на складе (`id`, название, цена, остаток `stock`).
    
      
    
- `orders` — заказы (`id`, `product_id`, количество `quantity`, итоговая сумма `total_price`, дата).
    
      
    

**Что делает триггер (`trg_process_order`):** Срабатывает **до вставки** новой записи в таблицу заказов (`BEFORE INSERT FOR EACH ROW`) и выполняет реальную бизнес-логику вместо простого логгирования:

  

1. Проверяет, существует ли товар и хватает ли его на складе (`stock >= NEW.quantity`).
    
      
    
2. Если товара не хватает — прерывает операцию через `RAISE EXCEPTION` и не даёт оформить некорректный заказ.
    
      
    
3. Если товара хватает — **автоматически рассчитывает итоговую стоимость заказа** (`NEW.total_price = price * quantity`) и **списывает купленное количество со склада** в таблице `products`.
    
      
    

### 3. SQL-код для DBeaver

> **Совет для DBeaver:** выполняй каждый шаг отдельно через выделение нужного фрагмента и нажатие **`Ctrl + Enter`** (или запуск скрипта через **`Alt + X`**).
> 
>   

#### Шаг 1. Создание таблиц и тестовых данных

**Вариант 1: С комментариями**

  

SQL

```
-- Удаляем таблицы, если они уже существовали (для чистого перезапуска)
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;

-- Создаём таблицу товаров
CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price NUMERIC(10, 2) NOT NULL CHECK (price >= 0),
    stock INTEGER NOT NULL CHECK (stock >= 0)
);

-- Создаём таблицу заказов (total_price будет заполнять триггер)
CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    product_id INTEGER NOT NULL REFERENCES products(id),
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    total_price NUMERIC(10, 2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Наполняем склад начальными товарами
INSERT INTO products (name, price, stock) VALUES
    ('Ноутбук', 75000.00, 5),
    ('Беспроводная мышь', 1500.00, 10),
    ('Механическая клавиатура', 4500.00, 3);
```

**Вариант 2: Без комментариев**

  

SQL

```
DROP TABLE IF EXISTS orders;
DROP TABLE IF EXISTS products;

CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    price NUMERIC(10, 2) NOT NULL CHECK (price >= 0),
    stock INTEGER NOT NULL CHECK (stock >= 0)
);

CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    product_id INTEGER NOT NULL REFERENCES products(id),
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    total_price NUMERIC(10, 2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO products (name, price, stock) VALUES
    ('Ноутбук', 75000.00, 5),
    ('Беспроводная мышь', 1500.00, 10),
    ('Механическая клавиатура', 4500.00, 3);
```

#### Шаг 2. Создание триггерной функции и триггера

**Вариант 1: С комментариями**

  

SQL

```
-- Создаём триггерную функцию (без аргументов, возвращает тип trigger)
CREATE OR REPLACE FUNCTION process_order_fn()
RETURNS TRIGGER AS $$
DECLARE
    -- Локальные переменные для хранения цены и текущего остатка товара
    v_price NUMERIC(10, 2);
    v_stock INTEGER;
BEGIN
    -- Получаем цену и остаток заказанного товара из таблицы products
    SELECT price, stock INTO v_price, v_stock
    FROM products
    WHERE id = NEW.product_id;

    -- Если товар с указанным ID не найден, отменяем вставку
    IF NOT FOUND THEN
        RAISE EXCEPTION 'Товар с ID % не найден', NEW.product_id;
    END IF;

    -- Проверяем, достаточно ли товара на складе
    IF v_stock < NEW.quantity THEN
        RAISE EXCEPTION 'Недостаточно товара на складе. Доступно: %, запрошено: %', v_stock, NEW.quantity;
    END IF;

    -- Автоматически вычисляем итоговую стоимость заказа в новой строке
    NEW.total_price := v_price * NEW.quantity;

    -- Уменьшаем остаток товара на складе
    UPDATE products
    SET stock = stock - NEW.quantity
    WHERE id = NEW.product_id;

    -- Возвращаем изменённую строку NEW для завершения операции INSERT
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Привязываем функцию к таблице orders как построчный BEFORE-триггер
CREATE TRIGGER trg_process_order
BEFORE INSERT ON orders
FOR EACH ROW
EXECUTE FUNCTION process_order_fn();
```

**Вариант 2: Без комментариев**

  

SQL

```
CREATE OR REPLACE FUNCTION process_order_fn()
RETURNS TRIGGER AS $$
DECLARE
    v_price NUMERIC(10, 2);
    v_stock INTEGER;
BEGIN
    SELECT price, stock INTO v_price, v_stock
    FROM products
    WHERE id = NEW.product_id;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Товар с ID % не найден', NEW.product_id;
    END IF;

    IF v_stock < NEW.quantity THEN
        RAISE EXCEPTION 'Недостаточно товара на складе. Доступно: %, запрошено: %', v_stock, NEW.quantity;
    END IF;

    NEW.total_price := v_price * NEW.quantity;

    UPDATE products
    SET stock = stock - NEW.quantity
    WHERE id = NEW.product_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_process_order
BEFORE INSERT ON orders
FOR EACH ROW
EXECUTE FUNCTION process_order_fn();
```

#### Шаг 3. Проверка работоспособности (тесты)

> При запуске третьего `INSERT` DBeaver выдаст всплывающее окно с ошибкой `Недостаточно товара на складе...` — это ожидаемое поведение, которое подтверждает защиту от некорректных данных.
> 
>   

**Вариант 1: С комментариями**

  

SQL

```
-- Тест 1: Успешный заказ (покупаем 2 ноутбука по 75000.00; total_price не передаём)
INSERT INTO orders (product_id, quantity) VALUES (1, 2);

-- Проверяем: в orders появилась запись с total_price = 150000.00, а у ноутбука stock стал 3
SELECT * FROM orders;
SELECT * FROM products;

-- Тест 2: Попытка заказать больше, чем есть на складе (клавиатур всего 3, просим 10)
-- Запрос завершится ошибкой (RAISE EXCEPTION) и откатится
INSERT INTO orders (product_id, quantity) VALUES (3, 10);

-- Проверяем, что ошибочный заказ не добавился, а остатки не изменились
SELECT * FROM orders;
SELECT * FROM products;
```

**Вариант 2: Без комментариев**

  

SQL

```
INSERT INTO orders (product_id, quantity) VALUES (1, 2);

SELECT * FROM orders;
SELECT * FROM products;

INSERT INTO orders (product_id, quantity) VALUES (3, 10);

SELECT * FROM orders;
SELECT * FROM products;
```

### 4. Шпаргалка для объяснения преподавателю на защите

Если преподаватель спросит, как это работает внутри PostgreSQL:

  

- **Почему `BEFORE INSERT`, а не `AFTER INSERT`?** Потому что мы модифицируем вставляемую строку до её записи на диск — записываем рассчитанную сумму в поле `NEW.total_price`. В триггере `AFTER` менять `NEW` уже поздно.
    
      
    
- **Что такое `NEW`?** Специальная системная переменная типа `RECORD`, которая автоматически создаётся в строчных триггерах (`FOR EACH ROW`) и хранит данные новой добавляемой строки.
    
      
    
- **Зачем в конце `RETURN NEW;`?** В `BEFORE`-триггере возврат `NEW` разрешает PostgreSQL продолжить вставку уже изменённой строки. Если вернуть `NULL`, вставка отменится тихо, а `RAISE EXCEPTION` отменяет вставку с понятным сообщением об ошибке.
    
      
    
- **Что будет с транзакцией при ошибке?** Триггер выполняется внутри транзакции внешнего запроса `INSERT`. При срабатывании `RAISE EXCEPTION` вся транзакция откатывается автоматически, поэтому расхождений между заказами и остатками не возникнет.