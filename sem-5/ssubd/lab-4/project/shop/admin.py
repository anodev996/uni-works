from django.contrib import admin

from django.contrib import admin
from .models import Category, Supplier, Product, Customer, Order


@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    list_display = ("id", "name", "slug")
    search_fields = ("name", "description")
    prepopulated_fields = {"slug": ("name",)}


@admin.register(Supplier)
class SupplierAdmin(admin.ModelAdmin):
    list_display = ("id", "company_name", "contact_email", "phone", "country")
    search_fields = ("company_name", "contact_email")
    list_filter = ("country",)


@admin.register(Product)
class ProductAdmin(admin.ModelAdmin):
    list_display = (
        "id",
        "title",
        "sku",
        "category",
        "supplier",
        "price",
        "stock_quantity",
        "is_available",
    )
    list_filter = ("category", "is_available", "supplier")
    search_fields = ("title", "sku")
    list_editable = ("price", "stock_quantity", "is_available")


@admin.register(Customer)
class CustomerAdmin(admin.ModelAdmin):
    list_display = ("id", "last_name", "first_name", "email", "phone", "discount_rate")
    search_fields = ("last_name", "email", "phone")
    list_filter = ("discount_rate",)


@admin.register(Order)
class OrderAdmin(admin.ModelAdmin):
    list_display = (
        "id",
        "order_number",
        "customer",
        "product",
        "quantity",
        "total_price",
        "status",
        "created_at",
    )
    list_filter = ("status", "created_at")
    search_fields = ("order_number", "customer__last_name", "product__title")
    readonly_fields = ("total_price", "created_at")
