import os
import django

os.environ.setdefault("DJANGO_SETTINGS_MODULE", "config.settings")
django.setup()

from shop.models import Product, Customer, Order

# CREATE
new_cust = Customer.objects.create(
    first_name="Сергей",
    last_name="Васильев",
    email="serg.v@example.com",
    phone="+79990001122",
    discount_rate=15,
)

# READ
active_orders = Order.objects.select_related("customer", "product").filter(
    status__in=["NEW", "PROCESSING"]
)
for o in active_orders:
    print(
        f"{o.order_number} | {o.customer.last_name} | {o.product.title} | {o.total_price} руб."
    )

# UPDATE
Product.objects.filter(sku="ROG-4080-16G").update(price=129990.00)

# DELETE
Customer.objects.filter(email="serg.v@example.com").delete()
