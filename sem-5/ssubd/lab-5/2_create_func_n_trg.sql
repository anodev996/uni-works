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