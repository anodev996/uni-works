# django-orm-lab4

## 1. Разбор концепции: миграции базы данных

### Миграции в целом

Миграции базы данных — это система контроля версий для схемы данных. Подобно тому как Git отслеживает построчные изменения в исходном коде, механизм миграций фиксирует эволюцию структуры базы данных: добавление таблиц, переименование колонок, смену типов данных, установку ограничений целостности (`NOT NULL`, `UNIQUE`) и внешних ключей (`FOREIGN KEY`).

До появления миграций разработчикам приходилось вручную писать SQL DDL-скрипты (`ALTER TABLE...`), синхронизировать их между членами команды и вручную накатывать на тестовые и продуктовые сервера. Это приводило к рассинхронизации схемы и потере данных. Миграции автоматизируют этот процесс, делая изменения схемы **декларативными**, **воспроизводимыми** и **обратимыми** (поддерживающими откат `rollback`).

### Миграции в примере лабораторной работы и в Django

В экосистеме Django ORM миграционный процесс разбит на два ключевых этапа:

1. **Генерация миграций (`python manage.py makemigrations`)**:

- Движок Django сравнивает текущее состояние классов в файлах `models.py` с предыдущими снимками схемы, хранящимися в пакете `migrations/` каждого приложения.

- На основе найденных различий создается новый файл миграции на языке Python (в методичке это `api/migrations/0001_initial.py`), содержащий структурированные команды уровня фреймворка: `migrations.CreateModel`, `migrations.AddField` и т.д.

- _Примечание по методичке:_ на снимке экрана виден ворнинг `(fields.W122) 'max_length' is ignored when used with IntegerField`. Преподаватель или автор методички указал `max_length=2` для `IntegerField` в модели `SemesterModel`. В SQL целочисленные типы (`INTEGER`) определяются разрядностью в байтах, а не количеством символов, поэтому Django проигнорировал этот аргумент и выдал предупреждение.

2. **Применение миграций (`python manage.py migrate`)**:

- Django подключается к целевой СУБД и транслирует абстрактные операции из файлов миграций в специфичный для конкретной базы диалект SQL (`CREATE TABLE`, `ADD CONSTRAINT`).

- В базе создается служебная таблица `django_migrations`. В нее записываются имена всех успешно примененных файлов с отметкой времени. Это гарантирует, что ни одна миграция не выполнится повторно.

- В методичке видно, что команда `migrate` сначала накатила встроенные системные таблицы Django (`contenttypes`, `auth`, `admin`, `sessions`), а затем пользовательскую миграцию `api.0001_initial`.

---

## 2. Предметная область: «Маркетплейс цифровой техники и комплектующих»

В соответствии с заданием выбрана новая предметная область, отличная от университетской успеваемости. Разрабатывается система управления заказами интернет-магазина.

Спроектировано 5 связанных моделей:

1. **`Category`** — категории номенклатуры (каталог).
2. **`Supplier`** — дистрибьюторы и поставщики оборудования.
3. **`Product`** — товары с привязкой к категории и поставщику (внешние ключи).
4. **`Customer`** — профили покупателей с системой скидок.
5. **`Order`** — заказы клиентов с расчетом итоговой стоимости, учетом количества и статусной моделью.

---

## 3. Блоки кода лабораторной работы

### Блок 1: Описание моделей ORM (`shop/models.py`)

#### Вариант 1 (без комментариев)

```python
from django.db import models
from django.core.validators import MinValueValidator, MaxValueValidator


class Category(models.Model):
    name = models.CharField(max_length=120, unique=True)
    slug = models.SlugField(max_length=140, unique=True)
    description = models.TextField(blank=True, default='')

    class Meta:
        verbose_name = 'Категория'
        verbose_name_plural = 'Категории'
        ordering = ['name']

    def __str__(self):
        return self.name


class Supplier(models.Model):
    company_name = models.CharField(max_length=200, unique=True)
    contact_email = models.EmailField(unique=True)
    phone = models.CharField(max_length=20)
    country = models.CharField(max_length=100, default='Россия')

    class Meta:
        verbose_name = 'Поставщик'
        verbose_name_plural = 'Поставщики'
        ordering = ['company_name']

    def __str__(self):
        return f'{self.company_name} ({self.country})'


class Product(models.Model):
    category = models.ForeignKey(
        Category,
        on_delete=models.PROTECT,
        related_name='products'
    )
    supplier = models.ForeignKey(
        Supplier,
        on_delete=models.CASCADE,
        related_name='products'
    )
    title = models.CharField(max_length=255)
    sku = models.CharField(max_length=50, unique=True)
    price = models.DecimalField(
        max_digits=10,
        decimal_places=2,
        validators=[MinValueValidator(0.01)]
    )
    stock_quantity = models.PositiveIntegerField(default=0)
    is_available = models.BooleanField(default=True)

    class Meta:
        verbose_name = 'Товар'
        verbose_name_plural = 'Товары'
        ordering = ['-price']

    def __str__(self):
        return f'{self.title} [SKU: {self.sku}]'


class Customer(models.Model):
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    email = models.EmailField(unique=True)
    phone = models.CharField(max_length=20)
    discount_rate = models.IntegerField(
        default=0,
        validators=[MinValueValidator(0), MaxValueValidator(50)]
    )

    class Meta:
        verbose_name = 'Покупатель'
        verbose_name_plural = 'Покупатели'
        ordering = ['last_name', 'first_name']

    def __str__(self):
        return f'{self.last_name} {self.first_name} ({self.email})'


class Order(models.Model):
    STATUS_CHOICES = [
        ('NEW', 'Новый'),
        ('PROCESSING', 'В обработке'),
        ('SHIPPED', 'Отправлен'),
        ('DELIVERED', 'Доставлен'),
        ('CANCELLED', 'Отменен'),
    ]

    customer = models.ForeignKey(
        Customer,
        on_delete=models.CASCADE,
        related_name='orders'
    )
    product = models.ForeignKey(
        Product,
        on_delete=models.PROTECT,
        related_name='orders'
    )
    order_number = models.CharField(max_length=36, unique=True)
    quantity = models.PositiveIntegerField(
        default=1,
        validators=[MinValueValidator(1)]
    )
    total_price = models.DecimalField(
        max_digits=12,
        decimal_places=2,
        editable=False
    )
    status = models.CharField(
        max_length=20,
        choices=STATUS_CHOICES,
        default='NEW'
    )
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        verbose_name = 'Заказ'
        verbose_name_plural = 'Заказы'
        ordering = ['-created_at']

    def save(self, *args, **kwargs):
        base_sum = self.product.price * self.quantity
        discount = base_sum * (self.customer.discount_rate / 100)
        self.total_price = base_sum - discount
        super().save(*args, **kwargs)

    def __str__(self):
        return f'Заказ {self.order_number} от {self.customer.last_name}'

```

#### Вариант 2 (с обширными комментариями)

```python
# Импортируем базовый модуль models, предоставляющий метаклассы и типы полей для ORM-сопоставления
from django.db import models
# Валидаторы используются для обеспечения бизнес-ограничений целостности данных на уровне приложения
from django.core.validators import MinValueValidator, MaxValueValidator


# ==============================================================================
# МОДЕЛЬ 1: Категория товаров (Category)
# ==============================================================================
class Category(models.Model):
    # CharField транслируется в VARCHAR(120). unique=True создает уникальный индекс в БД
    name = models.CharField(max_length=120, unique=True)
    # SlugField оптимизирован для хранения URL-совместимых идентификаторов (латиница, цифры, дефисы)
    slug = models.SlugField(max_length=140, unique=True)
    # TextField транслируется в тип TEXT без жесткого ограничения длины; blank=True разрешает пустое поле в формах
    description = models.TextField(blank=True, default='')

    class Meta:
        # Человекочитаемые имена для интерфейса Django Admin в единственном и множественном числе
        verbose_name = 'Категория'
        verbose_name_plural = 'Категории'
        # Сортировка по умолчанию по названию категории в алфавитном порядке
        ordering = ['name']

    def __str__(self):
        # Строковое представление экземпляра объекта, отображаемое в админке и списках
        return self.name


# ==============================================================================
# МОДЕЛЬ 2: Поставщик оборудования (Supplier)
# ==============================================================================
class Supplier(models.Model):
    company_name = models.CharField(max_length=200, unique=True)
    # EmailField проверяет корректность формата адреса электронной почты регулярным выражением
    contact_email = models.EmailField(unique=True)
    phone = models.CharField(max_length=20)
    country = models.CharField(max_length=100, default='Россия')

    class Meta:
        verbose_name = 'Поставщик'
        verbose_name_plural = 'Поставщики'
        ordering = ['company_name']

    def __str__(self):
        return f'{self.company_name} ({self.country})'


# ==============================================================================
# МОДЕЛЬ 3: Товар (Product)
# ==============================================================================
class Product(models.Model):
    # Внешний ключ на категорию. on_delete=models.PROTECT запретит удаление категории, если в ней есть товары
    category = models.ForeignKey(
        Category,
        on_delete=models.PROTECT,
        # related_name задает имя обратной связи от Category к Product (например, category.products.all())
        related_name='products'
    )
    # Связь с поставщиком. on_delete=models.CASCADE удалит товары поставщика при удалении самого поставщика
    supplier = models.ForeignKey(
        Supplier,
        on_delete=models.CASCADE,
        related_name='products'
    )
    title = models.CharField(max_length=255)
    # SKU (артикул) должен быть строго уникальным
    sku = models.CharField(max_length=50, unique=True)
    # Для финансовых значений обязательно используется DecimalField во избежание ошибок округления чисел с плавающей точкой
    price = models.DecimalField(
        max_digits=10,       # Максимальное количество цифр всего (до и после запятой)
        decimal_places=2,   # Количество знаков после запятой
        validators=[MinValueValidator(0.01)]  # Запрет нулевой и отрицательной цены
    )
    # PositiveIntegerField принимает только целые неотрицательные числа
    stock_quantity = models.PositiveIntegerField(default=0)
    is_available = models.BooleanField(default=True)

    class Meta:
        verbose_name = 'Товар'
        verbose_name_plural = 'Товары'
        # Сортировка: знак «минус» означает убывание (от самых дорогих к дешевым)
        ordering = ['-price']

    def __str__(self):
        return f'{self.title} [SKU: {self.sku}]'


# ==============================================================================
# МОДЕЛЬ 4: Покупатель (Customer)
# ==============================================================================
class Customer(models.Model):
    first_name = models.CharField(max_length=100)
    last_name = models.CharField(max_length=100)
    email = models.EmailField(unique=True)
    phone = models.CharField(max_length=20)
    # Персональный процент скидки: от 0% до 50%
    discount_rate = models.IntegerField(
        default=0,
        validators=[MinValueValidator(0), MaxValueValidator(50)]
    )

    class Meta:
        verbose_name = 'Покупатель'
        verbose_name_plural = 'Покупатели'
        ordering = ['last_name', 'first_name']

    def __str__(self):
        return f'{self.last_name} {self.first_name} ({self.email})'


# ==============================================================================
# МОДЕЛЬ 5: Заказ (Order)
# ==============================================================================
class Order(models.Model):
    # Кортеж вариантов для статуса заказа (первый элемент — значение в БД, второй — текст для UI)
    STATUS_CHOICES = [
        ('NEW', 'Новый'),
        ('PROCESSING', 'В обработке'),
        ('SHIPPED', 'Отправлен'),
        ('DELIVERED', 'Доставлен'),
        ('CANCELLED', 'Отменен'),
    ]

    customer = models.ForeignKey(
        Customer,
        on_delete=models.CASCADE,
        related_name='orders'
    )
    product = models.ForeignKey(
        Product,
        on_delete=models.PROTECT,
        related_name='orders'
    )
    order_number = models.CharField(max_length=36, unique=True)
    quantity = models.PositiveIntegerField(
        default=1,
        validators=[MinValueValidator(1)]
    )
    # Поле рассчитывается автоматически в методе save(), поэтому editable=False скрывает его от ручного ввода в формах
    total_price = models.DecimalField(
        max_digits=12,
        decimal_places=2,
        editable=False
    )
    status = models.CharField(
        max_length=20,
        choices=STATUS_CHOICES,
        default='NEW'
    )
    # auto_now_add=True автоматически фиксирует дату и время момента создания записи в БД
    created_at = models.DateTimeField(auto_now_add=True)

    class Meta:
        verbose_name = 'Заказ'
        verbose_name_plural = 'Заказы'
        ordering = ['-created_at']

    def save(self, *args, **kwargs):
        # Бизнес-логика: автоматический пересчет итоговой стоимости с учетом персональной скидки клиента
        base_sum = self.product.price * self.quantity
        discount = base_sum * (self.customer.discount_rate / 100)
        self.total_price = base_sum - discount
        # Вызов оригинального метода сохранения родительского класса models.Model
        super().save(*args, **kwargs)

    def __str__(self):
        return f'Заказ {self.order_number} от {self.customer.last_name}'

```

---

### Блок 2: Настройка CRUD через Django Admin (`shop/admin.py`)

По условию задачи реализация операций CRUD (Create, Read, Update, Delete) выполняется через интерфейс административной панели Django.

#### Вариант 1 (без комментариев)

```python
from django.contrib import admin
from .models import Category, Supplier, Product, Customer, Order


@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    list_display = ('id', 'name', 'slug')
    search_fields = ('name', 'description')
    prepopulated_fields = {'slug': ('name',)}


@admin.register(Supplier)
class SupplierAdmin(admin.ModelAdmin):
    list_display = ('id', 'company_name', 'contact_email', 'phone', 'country')
    search_fields = ('company_name', 'contact_email')
    list_filter = ('country',)


@admin.register(Product)
class ProductAdmin(admin.ModelAdmin):
    list_display = ('id', 'title', 'sku', 'category', 'supplier', 'price', 'stock_quantity', 'is_available')
    list_filter = ('category', 'is_available', 'supplier')
    search_fields = ('title', 'sku')
    list_editable = ('price', 'stock_quantity', 'is_available')


@admin.register(Customer)
class CustomerAdmin(admin.ModelAdmin):
    list_display = ('id', 'last_name', 'first_name', 'email', 'phone', 'discount_rate')
    search_fields = ('last_name', 'email', 'phone')
    list_filter = ('discount_rate',)


@admin.register(Order)
class OrderAdmin(admin.ModelAdmin):
    list_display = ('id', 'order_number', 'customer', 'product', 'quantity', 'total_price', 'status', 'created_at')
    list_filter = ('status', 'created_at')
    search_fields = ('order_number', 'customer__last_name', 'product__title')
    readonly_fields = ('total_price', 'created_at')

```

#### Вариант 2 (с обширными комментариями)

```python
from django.contrib import admin
from .models import Category, Supplier, Product, Customer, Order


# Декоратор admin.register регистрирует модель и связывает её с управляющим классом ModelAdmin
@admin.register(Category)
class CategoryAdmin(admin.ModelAdmin):
    # list_display задает столбцы, выводимые в таблице списка сущностей (READ)
    list_display = ('id', 'name', 'slug')
    # search_fields активирует поисковую строку по указанным атрибутам
    search_fields = ('name', 'description')
    # prepopulated_fields автоматически генерирует URL-слаг на основе введенного названия на лету через JS
    prepopulated_fields = {'slug': ('name',)}


@admin.register(Supplier)
class SupplierAdmin(admin.ModelAdmin):
    list_display = ('id', 'company_name', 'contact_email', 'phone', 'country')
    search_fields = ('company_name', 'contact_email')
    # list_filter выводит в правой части панель быстрой фильтрации по странам
    list_filter = ('country',)


@admin.register(Product)
class ProductAdmin(admin.ModelAdmin):
    list_display = (
        'id', 'title', 'sku', 'category', 'supplier',
        'price', 'stock_quantity', 'is_available'
    )
    # Фильтрация по внешним ключам и булевым флагам
    list_filter = ('category', 'is_available', 'supplier')
    search_fields = ('title', 'sku')
    # list_editable позволяет модифицировать данные прямо в таблице без перехода в детальный формуляр (быстрый UPDATE)
    list_editable = ('price', 'stock_quantity', 'is_available')


@admin.register(Customer)
class CustomerAdmin(admin.ModelAdmin):
    list_display = ('id', 'last_name', 'first_name', 'email', 'phone', 'discount_rate')
    search_fields = ('last_name', 'email', 'phone')
    list_filter = ('discount_rate',)


@admin.register(Order)
class OrderAdmin(admin.ModelAdmin):
    list_display = (
        'id', 'order_number', 'customer', 'product',
        'quantity', 'total_price', 'status', 'created_at'
    )
    list_filter = ('status', 'created_at')
    # Синтаксис через двойное подчеркивание (__) позволяет выполнять поиск по полям связанных моделей через JOIN
    search_fields = ('order_number', 'customer__last_name', 'product__title')
    # readonly_fields защищает поля от случайной ручной модификации в панели добавления/редактирования
    readonly_fields = ('total_price', 'created_at')

```

---

### Блок 3: Команды выполнения миграций и создания суперпользователя (CLI)

#### Вариант 1 (без комментариев)

```bash
python manage.py makemigrations shop
python manage.py migrate
python manage.py createsuperuser

```

#### Вариант 2 (с обширными комментариями)

```bash
# 1. Формирование нового файла миграции для нашего приложения 'shop'.
# Django сопоставит структуру models.py с предыдущей ревизией и создаст shop/migrations/0001_initial.py
python manage.py makemigrations shop

# 2. Применение миграций ко всей базе данных.
# Команда выполнит трансляцию Python-описания схемы в DDL SQL-запросы (CREATE TABLE, ADD CONSTRAINT),
# создаст 5 таблиц приложения в СУБД и сделает запись в журнал django_migrations.
python manage.py migrate

# 3. Интерактивное создание учетной записи администратора для входа в веб-панель Django Admin (логин, пароль, email).
# После этого панель CRUD будет доступна в браузере по адресу http://127.0.0.1:8000/admin/
python manage.py createsuperuser

```

---

### Блок 4: Заполнение базы начальными данными через ORM (`seed_data.py`)

По условию требуется наполнить базу данными с помощью инструментов фреймворка. Скрипт наполняет базу связными записями по всем 5 моделям.

#### Вариант 1 (без комментариев)

```python
import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'config.settings')
django.setup()

from shop.models import Category, Supplier, Product, Customer, Order


def run_seed():
    Order.objects.all().delete()
    Product.objects.all().delete()
    Customer.objects.all().delete()
    Supplier.objects.all().delete()
    Category.objects.all().delete()

    c_gpu = Category.objects.create(name='Видеокарты', slug='gpu', description='Графические ускорители')
    c_cpu = Category.objects.create(name='Процессоры', slug='cpu', description='Центральные процессоры')
    c_ram = Category.objects.create(name='Оперативная память', slug='ram', description='Модули DDR4 и DDR5')

    s_asus = Supplier.objects.create(company_name='ASUS Global', contact_email='b2b@asus.com', phone='+886-2-2894-3447', country='Тайвань')
    s_intel = Supplier.objects.create(company_name='Intel Corp', contact_email='sales@intel.com', phone='+1-408-765-8080', country='США')

    p1 = Product.objects.create(category=c_gpu, supplier=s_asus, title='ASUS ROG Strix RTX 4080', sku='ROG-4080-16G', price=134990.00, stock_quantity=12)
    p2 = Product.objects.create(category=c_cpu, supplier=s_intel, title='Intel Core i7-14700K', sku='BX8071514700K', price=42990.00, stock_quantity=25)
    p3 = Product.objects.create(category=c_ram, supplier=s_asus, title='Kingston Fury Beast 32GB', sku='KF556C36BBEK2-32', price=11990.00, stock_quantity=40)

    cust1 = Customer.objects.create(first_name='Иван', last_name='Петров', email='ivan.petrov@mail.ru', phone='+79991112233', discount_rate=5)
    cust2 = Customer.objects.create(first_name='Анна', last_name='Смирнова', email='anna.sm@yandex.ru', phone='+79994445566', discount_rate=10)

    Order.objects.create(customer=cust1, product=p1, order_number='ORD-2026-001', quantity=1, status='PROCESSING')
    Order.objects.create(customer=cust2, product=p2, order_number='ORD-2026-002', quantity=2, status='NEW')
    Order.objects.create(customer=cust1, product=p3, order_number='ORD-2026-003', quantity=4, status='DELIVERED')


if __name__ == '__main__':
    run_seed()

```

#### Вариант 2 (с обширными комментариями)

```python
import os
import django

# Устанавливаем переменную окружения, указывающую на файл конфигурации Django-проекта
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'config.settings')
# Инициализируем реестр приложений и метаданные моделей для автономного выполнения вне веб-сервера
django.setup()

from shop.models import Category, Supplier, Product, Customer, Order


def run_seed():
    # Предварительно каскадно очищаем таблицы для обеспечения идемпотентности скрипта
    # Метод .all().delete() генерирует прямой SQL-запрос DELETE FROM
    Order.objects.all().delete()
    Product.objects.all().delete()
    Customer.objects.all().delete()
    Supplier.objects.all().delete()
    Category.objects.all().delete()

    # 1. Создание категорий через Manager.create() — выполняет INSERT INTO shop_category
    c_gpu = Category.objects.create(
        name='Видеокарты',
        slug='gpu',
        description='Графические ускорители'
    )
    c_cpu = Category.objects.create(
        name='Процессоры',
        slug='cpu',
        description='Центральные процессоры'
    )
    c_ram = Category.objects.create(
        name='Оперативная память',
        slug='ram',
        description='Модули DDR4 и DDR5'
    )

    # 2. Создание поставщиков
    s_asus = Supplier.objects.create(
        company_name='ASUS Global',
        contact_email='b2b@asus.com',
        phone='+886-2-2894-3447',
        country='Тайвань'
    )
    s_intel = Supplier.objects.create(
        company_name='Intel Corp',
        contact_email='sales@intel.com',
        phone='+1-408-765-8080',
        country='США'
    )

    # 3. Создание товаров с передачей связанных экземпляров объектов в ForeignKey
    # ORM автоматически извлечет первичные ключи (id) переданных объектов и подставит их в столбцы category_id и supplier_id
    p1 = Product.objects.create(
        category=c_gpu,
        supplier=s_asus,
        title='ASUS ROG Strix RTX 4080',
        sku='ROG-4080-16G',
        price=134990.00,
        stock_quantity=12
    )
    p2 = Product.objects.create(
        category=c_cpu,
        supplier=s_intel,
        title='Intel Core i7-14700K',
        sku='BX8071514700K',
        price=42990.00,
        stock_quantity=25
    )
    p3 = Product.objects.create(
        category=c_ram,
        supplier=s_asus,
        title='Kingston Fury Beast 32GB',
        sku='KF556C36BBEK2-32',
        price=11990.00,
        stock_quantity=40
    )

    # 4. Создание покупателей
    cust1 = Customer.objects.create(
        first_name='Иван',
        last_name='Петров',
        email='ivan.petrov@mail.ru',
        phone='+79991112233',
        discount_rate=5
    )
    cust2 = Customer.objects.create(
        first_name='Анна',
        last_name='Смирнова',
        email='anna.sm@yandex.ru',
        phone='+79994445566',
        discount_rate=10
    )

    # 5. Создание заказов
    # При вызове create() сработает переопределенный метод save(), который рассчитает total_price с учетом скидки
    Order.objects.create(
        customer=cust1,
        product=p1,
        order_number='ORD-2026-001',
        quantity=1,
        status='PROCESSING'
    )
    Order.objects.create(
        customer=cust2,
        product=p2,
        order_number='ORD-2026-002',
        quantity=2,
        status='NEW'
    )
    Order.objects.create(
        customer=cust1,
        product=p3,
        order_number='ORD-2026-003',
        quantity=4,
        status='DELIVERED'
    )


if __name__ == '__main__':
    run_seed()

```

---

### Блок 5: Демонстрация программных методов CRUD через Django ORM (`crud_demo.py`)

Показывает реализацию операций манипуляции данными через программный интерфейс QuerySet.

#### Вариант 1 (без комментариев)

```python
import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'config.settings')
django.setup()

from shop.models import Product, Customer, Order


# CREATE
new_cust = Customer.objects.create(
    first_name='Сергей',
    last_name='Васильев',
    email='serg.v@example.com',
    phone='+79990001122',
    discount_rate=15
)

# READ
active_orders = Order.objects.select_related('customer', 'product').filter(status__in=['NEW', 'PROCESSING'])
for o in active_orders:
    print(f'{o.order_number} | {o.customer.last_name} | {o.product.title} | {o.total_price} руб.')

# UPDATE
Product.objects.filter(sku='ROG-4080-16G').update(price=129990.00)

# DELETE
Customer.objects.filter(email='serg.v@example.com').delete()

```

#### Вариант 2 (с обширными комментариями)

```python
import os
import django

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'config.settings')
django.setup()

from shop.models import Product, Customer, Order

# ------------------------------------------------------------------------------
# 1. CREATE (Создание новой записи)
# ------------------------------------------------------------------------------
# Метод objects.create() конструирует объект и немедленно отправляет запрос INSERT INTO в БД
new_cust = Customer.objects.create(
    first_name='Сергей',
    last_name='Васильев',
    email='serg.v@example.com',
    phone='+79990001122',
    discount_rate=15
)

# ------------------------------------------------------------------------------
# 2. READ (Чтение, выборка и оптимизация запросов через JOIN)
# ------------------------------------------------------------------------------
# Метод select_related() решает проблему «N+1 запросов»: он выполняет SQL INNER/LEFT JOIN,
# подтягивая данные связанных таблиц Customer и Product в одном первичном запросе
active_orders = Order.objects.select_related('customer', 'product').filter(
    # Lookup-оператор status__in формирует SQL условие: WHERE status IN ('NEW', 'PROCESSING')
    status__in=['NEW', 'PROCESSING']
)

for o in active_orders:
    # Доступ к полям o.customer и o.product не вызывает дополнительных запросов к БД благодаря select_related
    print(f'{o.order_number} | {o.customer.last_name} | {o.product.title} | {o.total_price} руб.')

# ------------------------------------------------------------------------------
# 3. UPDATE (Обновление существующей записи)
# ------------------------------------------------------------------------------
# Метод filter().update() транслируется напрямую в UPDATE shop_product SET price = 129990.00 WHERE sku = '...'
# Это выполняется атомарно на стороне СУБД без извлечения объекта в оперативную память Python
Product.objects.filter(sku='ROG-4080-16G').update(price=129990.00)

# ------------------------------------------------------------------------------
# 4. DELETE (Удаление записи)
# ------------------------------------------------------------------------------
# Метод delete() выполняет удаление записей, подходящих под критерий фильтрации
Customer.objects.filter(email='serg.v@example.com').delete()

```

---

## 4. Контрольные вопросы к лабораторной работе

### 1. Что такое ORM?

ORM (Object-Relational Mapping) — технологический слой, связывающий несовместимые парадигмы объектно-ориентированного программирования и реляционных баз данных. ORM представляет таблицы в виде классов, строки — в виде объектов в памяти, а столбцы — в виде свойств, автоматически транслируя манипуляции над объектами в SQL-запросы (`INSERT`, `SELECT`, `UPDATE`, `DELETE`).

### 2. Особенности низкоуровневого доступа к БД

При низкоуровневом доступе разработчик напрямую использует драйверы (например, `psycopg2` для PostgreSQL или `sqlite3`):

- Запросы пишутся вручную на сыром SQL, что требует строгой ручной защиты от SQL-инъекций (параметризация).
- Результаты запросов возвращаются в виде «сырых» кортежей или словарей, требуя ручного парсинга и маппинга в типы языка.
- Обеспечивается максимальная скорость выполнения и полный контроль над планом запроса (`EXPLAIN ANALYZE`), но кратно возрастают объемы однообразного шаблонного кода.

### 3. Паттерны работы с БД

- **Active Record**: Объект объединяет в себе данные строки и методы работы с БД (сохранение, обновление, удаление). Модель «знает», как сохранить саму себя в базу (реализован в Django ORM, Ruby on Rails).
- **Data Mapper**: Слой данных отделен от бизнес-логики. Сущности являются «чистыми» объектами (POPO / POJO / Dataclass), а операциями сохранения и извлечения управляет отдельный класс-маппер (реализован в SQLAlchemy, Hibernate).
- **Unit of Work**: Отслеживает все изменения бизнес-объектов в рамках транзакции и записывает их в БД единым пакетом при коммите.
- **Repository**: Инкапсулирует логику доступа к объектам в виде псевдо-коллекции в памяти, изолируя предметную область от конкретной СУБД.

### 4. Подходы взаимодействия СУБД с объектами ПО

- **Code-First / Model-First**: Структура схемы БД генерируется на основе классов в коде приложения с помощью миграций (подход Django ORM).
- **Database-First**: База данных проектируется в СУБД, после чего утилиты обратного инжиниринга (например, `python manage.py inspectdb`) генерируют классы моделей по существующим таблицам.
- **Schema-First**: Первичным является независимый контракт схемы (например, XML, JSON Schema, GraphQL), из которого генерируются как DDL-таблицы, так и классы в коде.

### 5. Миграция базы данных

Миграция базы данных — программно управляемое, версионируемое преобразование схемы БД между двумя состояниями. Миграция фиксирует дельту изменений (создание, изменение, удаление таблиц и колонок) в виде воспроизводимых файлов, гарантируя консистентность схемы между локальными средами разработки, тестовыми стендами и боевыми серверами.

### 6. Преимущества и недостатки ORM

- **Преимущества**:

- Высокая скорость разработки (Time-to-Market) и избавление от рутинного шаблонного кода.

- Абстракция от конкретной СУБД: переносимость кода с SQLite на PostgreSQL или MySQL заменой настроек подключения.
- Встроенная безопасность: автоматическая параметризация исключает уязвимости класса SQL Injection.
- Простота поддержки связей и каскадных операций.

- **Недостатки**:

- Оверхед производительности при генерации и парсинге запросов.

- Проблема «N+1 запросов» при неаккуратной работе со связями (требует использования `select_related` / `prefetch_related`).

- Неэффективность или невозможность выполнения специфичных сложных аналитических запросов (оконные функции, рекурсивные CTE), для которых приходится возвращаться к чистому SQL.

# configure-django-sqlite-lab4

Подключение к SQLite в Django настраивается в файле конфигурации проекта `settings.py` через словарь `DATABASES`. По умолчанию при создании проекта командой `django-admin startproject` этот адаптер уже выбран базовым, а сам файл базы данных `db.sqlite3` физически создается в корне проекта сразу после выполнения первой миграции.

### Блок 1: Конфигурация базы данных в `settings.py`

#### Вариант 1 (без комментариев)

Python

```
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parent.parent

DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'db.sqlite3',
    }
}
```

#### Вариант 2 (с обширными комментариями)

Python

```
# Импортируем класс Path из стандартного модуля pathlib для работы с путями файловой системы
from pathlib import Path

# BASE_DIR указывает на абсолютный путь к корневой директории проекта (где лежит manage.py).
# resolve() преобразует относительный путь в абсолютный, а .parent.parent поднимается на два уровня выше settings.py
BASE_DIR = Path(__file__).resolve().parent.parent

# DATABASES — системный словарь Django со списком всех подключений к базам данных
DATABASES = {
    # 'default' — обязательный идентификатор основного подключения
    'default': {
        # ENGINE указывает используемый бэкенд СУБД.
        # Для SQLite используется встроенный адаптер 'django.db.backends.sqlite3'
        'ENGINE': 'django.db.backends.sqlite3',

        # NAME задает путь к файлу БД. Оператор '/' объединяет путь BASE_DIR с именем файла 'db.sqlite3'.
        # При первом запуске миграций SQLite создаст этот файл автоматически
        'NAME': BASE_DIR / 'db.sqlite3',
    }
}
```

### Блок 2: Инициализация файла базы данных и применение миграций

После проверки параметров в `settings.py` выполняются команды в терминале для создания служебных таблиц и генерации файла `db.sqlite3`.

#### Вариант 1 (без комментариев)

Bash

```
python manage.py check --database default
python manage.py migrate
```

#### Вариант 2 (с обширными комментариями)

Bash

```
# Проверка корректности синтаксиса settings.py и доступности бэкенда базы данных без записи данных
python manage.py check --database default

# Применение системных и пользовательских миграций.
# На этом шаге SQLite создает локальный файл db.sqlite3 в корне проекта и накатывает таблицы
python manage.py migrate
```

### Блок 3: Дополнительная настройка таймаута для SQLite (при блокировках `database is locked`)

В SQLite доступ на запись блокирует весь файл базы. Если при разработке или параллельных запросах возникает ошибка `OperationalError: database is locked`, в `OPTIONS` добавляется параметр времени ожидания снятия блокировки.

#### Вариант 1 (без комментариев)

Python

```
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'db.sqlite3',
        'OPTIONS': {
            'timeout': 20,
        }
    }
}
```

#### Вариант 2 (с обширными комментариями)

Python

```
DATABASES = {
    'default': {
        'ENGINE': 'django.db.backends.sqlite3',
        'NAME': BASE_DIR / 'db.sqlite3',
        # OPTIONS передает низкоуровневые параметры в стандартный модуль Python sqlite3.connect()
        'OPTIONS': {
            # timeout задает время ожидания (в секундах) освобождения блокировки файла БД другим процессом,
            # прежде чем Django выбросит исключение OperationalError: database is locked
            'timeout': 20,
        }
    }
}
```

# django-project-setup-lab4

Чтобы код из четвертой лабораторной заработал без правки импортов, проект должен называться **`config`**, а приложение — **`shop`** (именно эти имена прописаны в `DJANGO_SETTINGS_MODULE` и импортах `from shop.models import ...`).

  

### Блок 1: Создание окружения, установка Django и инициализация структуры

Выполняется в терминале (VS Code, PyCharm или системной консоли) из пустой папки будущего проекта.

  

#### Вариант 1 (без комментариев)

Bash

```
mkdir lab4_orm
cd lab4_orm

python -m venv venv
# Для Windows:
venv\Scripts\activate
# Для Linux / macOS:
source venv/bin/activate

pip install django

django-admin startproject config .
python manage.py startapp shop
```

_(Примечание: строки активации для Windows и Linux/macOS выше приведены для выбора нужной ОС; ниже — полностью чистый вариант без единого комментария под Linux/macOS и Windows отдельно)._

  

**Вариант 1А — Windows (без комментариев):**

  

PowerShell

```
mkdir lab4_orm
cd lab4_orm
python -m venv venv
venv\Scripts\activate
pip install django
django-admin startproject config .
python manage.py startapp shop
```

**Вариант 1Б — Linux / macOS (без комментариев):**

  

Bash

```
mkdir lab4_orm
cd lab4_orm
python3 -m venv venv
source venv/bin/activate
pip install django
django-admin startproject config .
python manage.py startapp shop
```

#### Вариант 2 (с обширными комментариями)

Bash

```
# 1. Создаем корневую директорию лабораторной работы и переходим в нее
mkdir lab4_orm
cd lab4_orm

# 2. Создаем изолированное виртуальное окружение (папку venv).
# Ключ -m запускает встроенный модуль venv как скрипт. Изоляция нужна, чтобы зависимости проекта
# не конфликтовали с глобальными библиотеками Python в системе.
python -m venv venv

# 3. Активируем виртуальное окружение (выбери команду под свою операционную систему):
# Для Windows (CMD / PowerShell):
venv\Scripts\activate
# Для Linux / macOS (bash / zsh):
# source venv/bin/activate

# 4. Устанавливаем актуальную версию фреймворка Django внутрь активного окружения venv
pip install django

# 5. Инициализируем каркас проекта с именем 'config'.
# КРИТИЧЕСКИ ВАЖНО: точка '.' в конце указывает Django создать управляющий файл manage.py
# прямо в текущей папке lab4_orm, а не создавать лишнюю вложенную папку config/config.
django-admin startproject config .

# 6. Создаем внутри проекта приложение 'shop', в котором будут лежать наши 5 моделей и настройки админки
python manage.py startapp shop
```

### Блок 2: Подключение приложения `shop` в `config/settings.py`

После создания приложения Django еще не знает о его существовании. Необходимо зарегистрировать `'shop'` в списке `INSTALLED_APPS`, иначе команда `makemigrations` проигнорирует файл `models.py`. Дополнительно можно переключить язык админ-панели на русский.

  

#### Вариант 1 (без комментариев)

Python

```
INSTALLED_APPS = [
    'django.contrib.admin',
    'django.contrib.auth',
    'django.contrib.contenttypes',
    'django.contrib.sessions',
    'django.contrib.messages',
    'django.contrib.staticfiles',
    'shop',
]

LANGUAGE_CODE = 'ru-ru'
TIME_ZONE = 'Asia/Tomsk'
```

#### Вариант 2 (с обширными комментариями)

Python

```
# Открываем файл config/settings.py, находим список INSTALLED_APPS и дописываем в конец 'shop'.
# Без этой записи ORM-движок Django не будет сканировать файл shop/models.py при поиске таблиц
# и не подхватит регистрацию моделей из shop/admin.py.
INSTALLED_APPS = [
    'django.contrib.admin',        # Встроенная панель администратора (наш интерфейс для CRUD)
    'django.contrib.auth',         # Система аутентификации и прав доступа пользователей
    'django.contrib.contenttypes', # Фреймворк типов содержимого (связывает модели с правами)
    'django.contrib.sessions',     # Механизм хранения сессий авторизованных пользователей
    'django.contrib.messages',     # Всплывающие уведомления в админке («Объект успешно сохранен»)
    'django.contrib.staticfiles',  # Сборка и раздача CSS/JS файлов для стилизации админ-панели
    'shop',                        # Наше пользовательское приложение маркетплейса
]

# Опционально: перевод системных кнопок и сообщений админ-панели Django на русский язык
LANGUAGE_CODE = 'ru-ru'

# Часовой пояс для корректного сохранения меток времени в поле created_at (auto_now_add=True)
TIME_ZONE = 'Asia/Tomsk'
```

### Итоговая структура файлов проекта

После выполнения команд выше и переноса кода из предыдущего ответа дерево файлов должно выглядеть так:

  

- `lab4_orm/` _(корневая директория)_
    
      
    - `venv/` _(виртуальное окружение)_
        
          
        
    - `manage.py` _(утилита управления проектом)_
        
          
        
    - `seed_data.py` _(скрипт начального наполнения БД — создается вручную в корне)_
        
          
        
    - `crud_demo.py` _(скрипт проверки программного CRUD — создается вручную в корне)_
        
          
        
    - `config/` _(папка настроек проекта)_
        
          
        - `__init__.py`
            
              
            
        - `settings.py` _(конфигурация БД SQLite и `INSTALLED_APPS`)_
            
              
            
        - `urls.py` _(маршрутизация, где уже прописан путь `admin/`)_
            
              
            
        - `wsgi.py` / `asgi.py`
            
              
            
    - `shop/` _(папка приложения)_
        
          
        - `migrations/` _(сюда Django положит файл `0001_initial.py`)_
            
              
            
        - `__init__.py`
            
              
            
        - `admin.py` _(сюда вставляется код настройки Django Admin)_
            
              
            
        - `apps.py`
            
              
            
        - `models.py` _(сюда вставляется код 5 моделей ORM)_
            
              
            
        - `tests.py`
            
              
            
        - `views.py`
            
              
            

### Блок 3: Применение миграций, наполнение БД и запуск сервера

Когда файлы `shop/models.py`, `shop/admin.py` и `seed_data.py` заполнены кодом, выполняются финальные команды запуска.

#### Вариант 1 (без комментариев)

Bash

```
python manage.py makemigrations
python manage.py migrate
python seed_data.py
python crud_demo.py
python manage.py createsuperuser
python manage.py runserver
```

#### Вариант 2 (с обширными комментариями)

Bash

```
# 1. Анализируем shop/models.py и генерируем файл инструкций миграции shop/migrations/0001_initial.py
python manage.py makemigrations

# 2. Создаем в корне файл db.sqlite3 и разворачиваем в нем все системные таблицы и 5 наших таблиц
python manage.py migrate

# 3. Запускаем автономный скрипт наполнения базы тестовыми данными (категории, товары, заказы)
python seed_data.py

# 4. Запускаем демонстрационный скрипт проверки операций Create, Read, Update, Delete в консоли
python crud_demo.py

# 5. Создаем логин и пароль администратора для входа в веб-интерфейс
# (при вводе пароля символы в терминале не отображаются — это стандартная защита)
python manage.py createsuperuser

# 6. Запускаем локальный веб-сервер разработки.
# После запуска открываем в браузере http://127.0.0.1:8000/admin/ для работы с CRUD через интерфейс.
python manage.py runserver
```
