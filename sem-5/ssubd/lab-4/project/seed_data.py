import os
import django

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "config.settings")
django.setup()

from shop.models import Category, Supplier, Product, Customer, Order


def run_seed():
    Order.objects.all().delete()
    Product.objects.all().delete()
    Customer.objects.all().delete()
    Supplier.objects.all().delete()
    Category.objects.all().delete()

    c_gpu = Category.objects.create(
        name="Видеокарты", slug="gpu", description="Графические ускорители"
    )
    c_cpu = Category.objects.create(
        name="Процессоры", slug="cpu", description="Центральные процессоры"
    )
    c_ram = Category.objects.create(
        name="Оперативная память", slug="ram", description="Модули DDR4 и DDR5"
    )

    s_asus = Supplier.objects.create(
        company_name="ASUS Global",
        contact_email="b2b@asus.com",
        phone="+886-2-2894-3447",
        country="Тайвань",
    )
    s_intel = Supplier.objects.create(
        company_name="Intel Corp",
        contact_email="sales@intel.com",
        phone="+1-408-765-8080",
        country="США",
    )

    p1 = Product.objects.create(
        category=c_gpu,
        supplier=s_asus,
        title="ASUS ROG Strix RTX 4080",
        sku="ROG-4080-16G",
        price=134990.00,
        stock_quantity=12,
    )
    p2 = Product.objects.create(
        category=c_cpu,
        supplier=s_intel,
        title="Intel Core i7-14700K",
        sku="BX8071514700K",
        price=42990.00,
        stock_quantity=25,
    )
    p3 = Product.objects.create(
        category=c_ram,
        supplier=s_asus,
        title="Kingston Fury Beast 32GB",
        sku="KF556C36BBEK2-32",
        price=11990.00,
        stock_quantity=40,
    )

    cust1 = Customer.objects.create(
        first_name="Иван",
        last_name="Петров",
        email="ivan.petrov@mail.ru",
        phone="+79991112233",
        discount_rate=5,
    )
    cust2 = Customer.objects.create(
        first_name="Анна",
        last_name="Смирнова",
        email="anna.sm@yandex.ru",
        phone="+79994445566",
        discount_rate=10,
    )

    Order.objects.create(
        customer=cust1,
        product=p1,
        order_number="ORD-2026-001",
        quantity=1,
        status="PROCESSING",
    )
    Order.objects.create(
        customer=cust2,
        product=p2,
        order_number="ORD-2026-002",
        quantity=2,
        status="NEW",
    )
    Order.objects.create(
        customer=cust1,
        product=p3,
        order_number="ORD-2026-003",
        quantity=4,
        status="DELIVERED",
    )


if __name__ == "__main__":
    run_seed()
