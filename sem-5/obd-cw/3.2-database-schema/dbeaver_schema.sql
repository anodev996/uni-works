-- public.вид_запчасти definition
-- Drop table
-- DROP TABLE public.вид_запчасти;

create table public.вид_запчасти (
	ид_вида_запчасти serial4 not null,
	наименование_вида_запчасти varchar(100) not null,
	constraint вид_запчасти_pkey primary key ("ид_вида_запчасти"),
	constraint вид_запчасти_ид_вида_запчасти_not_null not null "ид_вида_запчасти",
	constraint вид_запчасти_наименование_ви_not_null not null "наименование_вида_запчасти"
);
-- public.должность definition
-- Drop table
-- DROP TABLE public.должность;

create table public.должность (
	ид_должности serial4 not null,
	наименование_должности varchar(100) not null,
	constraint должность_pkey primary key ("ид_должности"),
	constraint должность_ид_должности_not_null not null "ид_должности",
	constraint должность_наименование_должн_not_null not null "наименование_должности"
);
-- public.клиент definition
-- Drop table
-- DROP TABLE public.клиент;

create table public.клиент (
	ид_клиента serial4 not null,
	фамилия varchar(100) not null,
	имя varchar(100) not null,
	отчество varchar(100) null,
	электронная_почта varchar(255) not null,
	номер_телефона varchar(20) not null,
	хеш_пароля varchar(255) not null,
	constraint клиент_pkey primary key ("ид_клиента"),
	constraint клиент_ид_клиента_not_null not null "ид_клиента",
	constraint клиент_имя_not_null not null "имя",
	constraint клиент_номер_телефона_not_null not null "номер_телефона",
	constraint клиент_фамилия_not_null not null "фамилия",
	constraint клиент_хеш_пароля_not_null not null "хеш_пароля",
	constraint клиент_электронная_почта_key unique ("электронная_почта"),
	constraint клиент_электронная_почта_not_null not null "электронная_почта"
);
-- public.модель_авто definition
-- Drop table
-- DROP TABLE public.модель_авто;

create table public.модель_авто (
	ид_модели serial4 not null,
	марка varchar(100) not null,
	модель varchar(100) not null,
	constraint модель_авто_pkey primary key ("ид_модели"),
	constraint модель_авто_ид_модели_not_null not null "ид_модели",
	constraint модель_авто_марка_not_null not null "марка",
	constraint модель_авто_модель_not_null not null "модель"
);
-- public.статус_автомобиля definition
-- Drop table
-- DROP TABLE public.статус_автомобиля;

create table public.статус_автомобиля (
	ид_статуса serial4 not null,
	наименование_статуса varchar(100) not null,
	constraint статус_автомоб_наименование__not_null not null "наименование_статуса",
	constraint статус_автомобиля_pkey primary key ("ид_статуса"),
	constraint статус_автомобиля_ид_статуса_not_null not null "ид_статуса"
);
-- public.страна_производства definition
-- Drop table
-- DROP TABLE public.страна_производства;

create table public.страна_производства (
	ид_страны serial4 not null,
	название_страны varchar(100) not null,
	constraint страна_произво_название_стра_not_null not null "название_страны",
	constraint страна_производств_ид_страны_not_null not null "ид_страны",
	constraint страна_производства_pkey primary key ("ид_страны")
);
-- public.тип_кпп definition
-- Drop table
-- DROP TABLE public.тип_кпп;

create table public.тип_кпп (
	ид_типа_кпп serial4 not null,
	наименование_типа_кпп varchar(100) not null,
	constraint тип_кпп_pkey primary key ("ид_типа_кпп"),
	constraint тип_кпп_ид_типа_кпп_not_null not null "ид_типа_кпп",
	constraint тип_кпп_наименование_типа_кпп_not_null not null "наименование_типа_кпп"
);
-- public.тип_кузова definition
-- Drop table
-- DROP TABLE public.тип_кузова;

create table public.тип_кузова (
	ид_типа_кузова serial4 not null,
	наименование_типа_кузова varchar(100) not null,
	constraint тип_кузова_pkey primary key ("ид_типа_кузова"),
	constraint тип_кузова_ид_типа_кузова_not_null not null "ид_типа_кузова",
	constraint тип_кузова_наименование_типа__not_null not null "наименование_типа_кузова"
);
-- public.тип_привода definition
-- Drop table
-- DROP TABLE public.тип_привода;

create table public.тип_привода (
	ид_типа_привода serial4 not null,
	наименование_типа_привода varchar(100) not null,
	constraint тип_привода_pkey primary key ("ид_типа_привода"),
	constraint тип_привода_ид_типа_привода_not_null not null "ид_типа_привода",
	constraint тип_привода_наименование_тип_not_null not null "наименование_типа_привода"
);
-- public.тип_услуги definition
-- Drop table
-- DROP TABLE public.тип_услуги;

create table public.тип_услуги (
	ид_типа_услуги serial4 not null,
	наименование_услуги varchar(255) not null,
	базовая_стоимость numeric(12, 2) not null,
	constraint тип_услуги_pkey primary key ("ид_типа_услуги"),
	constraint тип_услуги_базовая_стоимость_not_null not null "базовая_стоимость",
	constraint тип_услуги_ид_типа_услуги_not_null not null "ид_типа_услуги",
	constraint тип_услуги_наименование_услу_not_null not null "наименование_услуги"
);
-- public.цвет definition
-- Drop table
-- DROP TABLE public.цвет;

create table public.цвет (
	ид_цвета serial4 not null,
	наименование_цвета varchar(100) not null,
	хекс_код varchar(7) null,
	constraint цвет_pkey primary key ("ид_цвета"),
	constraint цвет_ид_цвета_not_null not null "ид_цвета",
	constraint цвет_наименование_цвета_not_null not null "наименование_цвета"
);
-- public.автомобиль definition
-- Drop table
-- DROP TABLE public.автомобиль;

create table public.автомобиль (
	ид_автомобиля serial4 not null,
	ид_модели int4 not null,
	ид_типа_кузова int4 not null,
	ид_типа_привода int4 not null,
	ид_типа_кпп int4 not null,
	ид_цвета int4 not null,
	ид_страны int4 not null,
	ид_статуса int4 not null,
	vin_номер varchar(17) not null,
	год_выпуска int4 not null,
	пробег int4 default 0 not null,
	цена numeric(12, 2) not null,
	фотография bytea null,
	constraint автомобиль_pkey primary key ("ид_автомобиля"),
	constraint автомобиль_vin_номер_key unique ("vin_номер"),
	constraint автомобиль_vin_номер_not_null not null "vin_номер",
	constraint автомобиль_год_выпуска_not_null not null "год_выпуска",
	constraint автомобиль_ид_автомобиля_not_null not null "ид_автомобиля",
	constraint автомобиль_ид_модели_not_null not null "ид_модели",
	constraint автомобиль_ид_статуса_not_null not null "ид_статуса",
	constraint автомобиль_ид_страны_not_null not null "ид_страны",
	constraint автомобиль_ид_типа_кпп_not_null not null "ид_типа_кпп",
	constraint автомобиль_ид_типа_кузова_not_null not null "ид_типа_кузова",
	constraint автомобиль_ид_типа_привода_not_null not null "ид_типа_привода",
	constraint автомобиль_ид_цвета_not_null not null "ид_цвета",
	constraint автомобиль_пробег_not_null not null "пробег",
	constraint автомобиль_цена_not_null not null "цена",
	constraint автомобиль_ид_модели_fkey foreign key (ид_модели) references public.модель_авто(ид_модели),
	constraint автомобиль_ид_статуса_fkey foreign key (ид_статуса) references public.статус_автомобиля(ид_статуса),
	constraint автомобиль_ид_страны_fkey foreign key (ид_страны) references public.страна_производства(ид_страны),
	constraint автомобиль_ид_типа_кпп_fkey foreign key (ид_типа_кпп) references public.тип_кпп(ид_типа_кпп),
	constraint автомобиль_ид_типа_кузова_fkey foreign key (ид_типа_кузова) references public.тип_кузова(ид_типа_кузова),
	constraint автомобиль_ид_типа_привода_fkey foreign key (ид_типа_привода) references public.тип_привода(ид_типа_привода),
	constraint автомобиль_ид_цвета_fkey foreign key (ид_цвета) references public.цвет(ид_цвета)
);
-- public.заявка_на_тест_драйв definition
-- Drop table
-- DROP TABLE public.заявка_на_тест_драйв;

create table public.заявка_на_тест_драйв (
	ид_заявки serial4 not null,
	ид_автомобиля int4 not null,
	ид_клиента int4 not null,
	дата_тест_драйва date not null,
	время_тест_драйва time not null,
	комментарий text null,
	constraint заявка_на_тест__время_тест_дра_not_null not null "время_тест_драйва",
	constraint заявка_на_тест__дата_тест_драй_not_null not null "дата_тест_драйва",
	constraint заявка_на_тест__ид_автомобиля_not_null not null "ид_автомобиля",
	constraint заявка_на_тест_дра_ид_клиента_not_null not null "ид_клиента",
	constraint заявка_на_тест_драй_ид_заявки_not_null not null "ид_заявки",
	constraint заявка_на_тест_драйв_pkey primary key ("ид_заявки"),
	constraint заявка_на_тест_др_ид_автомобиля_fkey foreign key (ид_автомобиля) references public.автомобиль(ид_автомобиля),
	constraint заявка_на_тест_драйв_ид_клиента_fkey foreign key (ид_клиента) references public.клиент(ид_клиента)
);
-- public.наименование_запчасти definition
-- Drop table
-- DROP TABLE public.наименование_запчасти;

create table public.наименование_запчасти (
	ид_наименования int4 default nextval('"наименование_за_ид_наименования_seq"'::regclass) not null,
	ид_вида_запчасти int4 not null,
	наименование_запчасти varchar(255) not null,
	артикул varchar(100) not null,
	constraint наименование_з_ид_вида_запчас_not_null not null "ид_вида_запчасти",
	constraint наименование_з_ид_наименован_not_null not null "ид_наименования",
	constraint наименование_з_наименование__not_null not null "наименование_запчасти",
	constraint наименование_запчаст_артикул_not_null not null "артикул",
	constraint наименование_запчасти_pkey primary key ("ид_наименования"),
	constraint наименование_запчасти_артикул_key unique ("артикул"),
	constraint наименование_за_ид_вида_запчаст_fkey foreign key (ид_вида_запчасти) references public.вид_запчасти(ид_вида_запчасти)
);
-- public.платеж_бронирования definition
-- Drop table
-- DROP TABLE public.платеж_бронирования;

create table public.платеж_бронирования (
	ид_платежа serial4 not null,
	ид_автомобиля int4 not null,
	сумма_платежа numeric(12, 2) not null,
	дата_время_платежа timestamp not null,
	статус_платежа varchar(50) not null,
	constraint платеж_брониро_дата_время_пла_not_null not null "дата_время_платежа",
	constraint платеж_брониро_ид_автомобиля_not_null not null "ид_автомобиля",
	constraint платеж_брониро_статус_платеж_not_null not null "статус_платежа",
	constraint платеж_брониро_сумма_платежа_not_null not null "сумма_платежа",
	constraint платеж_бронирован_ид_платежа_not_null not null "ид_платежа",
	constraint платеж_бронирования_pkey primary key ("ид_платежа"),
	constraint платеж_бронирова_ид_автомобиля_fkey foreign key (ид_автомобиля) references public.автомобиль(ид_автомобиля)
);
-- public.сотрудник definition
-- Drop table
-- DROP TABLE public.сотрудник;

create table public.сотрудник (
	ид_сотрудника serial4 not null,
	ид_должности int4 not null,
	фамилия varchar(100) not null,
	имя varchar(100) not null,
	отчество varchar(100) null,
	номер_телефона varchar(20) not null,
	constraint сотрудник_pkey primary key ("ид_сотрудника"),
	constraint сотрудник_ид_должности_not_null not null "ид_должности",
	constraint сотрудник_ид_сотрудника_not_null not null "ид_сотрудника",
	constraint сотрудник_имя_not_null not null "имя",
	constraint сотрудник_номер_телефона_not_null not null "номер_телефона",
	constraint сотрудник_фамилия_not_null not null "фамилия",
	constraint сотрудник_ид_должности_fkey foreign key (ид_должности) references public.должность(ид_должности)
);
-- public.договор_купли_продажи definition
-- Drop table
-- DROP TABLE public.договор_купли_продажи;

create table public.договор_купли_продажи (
	ид_договора int4 default nextval('"договор_купли_прода_ид_договора_seq"'::regclass) not null,
	ид_автомобиля int4 not null,
	ид_клиента int4 not null,
	ид_сотрудника int4 not null,
	дата_время_сделки timestamp not null,
	итоговая_сумма numeric(12, 2) not null,
	тип_расчета varchar(50) not null,
	constraint договор_купли__дата_время_сде_not_null not null "дата_время_сделки",
	constraint договор_купли__итоговая_сумм_not_null not null "итоговая_сумма",
	constraint договор_купли_п_ид_автомобиля_not_null not null "ид_автомобиля",
	constraint договор_купли_п_ид_сотрудника_not_null not null "ид_сотрудника",
	constraint договор_купли_про_ид_договора_not_null not null "ид_договора",
	constraint договор_купли_про_тип_расчета_not_null not null "тип_расчета",
	constraint договор_купли_прод_ид_клиента_not_null not null "ид_клиента",
	constraint договор_купли_продажи_pkey primary key ("ид_договора"),
	constraint договор_купли_про_ид_автомобиля_fkey foreign key (ид_автомобиля) references public.автомобиль(ид_автомобиля),
	constraint договор_купли_про_ид_сотрудника_fkey foreign key (ид_сотрудника) references public.сотрудник(ид_сотрудника),
	constraint договор_купли_продаж_ид_клиента_fkey foreign key (ид_клиента) references public.клиент(ид_клиента)
);
-- public.заказ_наряд definition
-- Drop table
-- DROP TABLE public.заказ_наряд;

create table public.заказ_наряд (
	ид_заказа serial4 not null,
	ид_клиента int4 not null,
	ид_сотрудника int4 not null,
	vin_автомобиля_клиента varchar(17) not null,
	дата_заезда date not null,
	планируемая_дата_выдачи date null,
	статус_готовности varchar(50) not null,
	constraint заказ_наряд_pkey primary key ("ид_заказа"),
	constraint заказ_наряд_vin_автомобиля_кли_not_null not null "vin_автомобиля_клиента",
	constraint заказ_наряд_дата_заезда_not_null not null "дата_заезда",
	constraint заказ_наряд_ид_заказа_not_null not null "ид_заказа",
	constraint заказ_наряд_ид_клиента_not_null not null "ид_клиента",
	constraint заказ_наряд_ид_сотрудника_not_null not null "ид_сотрудника",
	constraint заказ_наряд_статус_готовност_not_null not null "статус_готовности",
	constraint заказ_наряд_ид_клиента_fkey foreign key (ид_клиента) references public.клиент(ид_клиента),
	constraint заказ_наряд_ид_сотрудника_fkey foreign key (ид_сотрудника) references public.сотрудник(ид_сотрудника)
);
-- public.детализация_ремонта definition
-- Drop table
-- DROP TABLE public.детализация_ремонта;

create table public.детализация_ремонта (
	ид_детализации int4 default nextval('"детализация_ремо_ид_детализации_seq"'::regclass) not null,
	ид_заказа int4 not null,
	ид_типа_услуги int4 null,
	ид_наименования int4 null,
	количество_запчастей int4 default 1 not null,
	стоимость numeric(12, 2) not null,
	constraint детализация_ре_ид_детализаци_not_null not null "ид_детализации",
	constraint детализация_ре_количество_за_not_null not null "количество_запчастей",
	constraint детализация_ремонт_ид_заказа_not_null not null "ид_заказа",
	constraint детализация_ремонт_стоимость_not_null not null "стоимость",
	constraint детализация_ремонта_pkey primary key ("ид_детализации"),
	constraint детализация_рем_ид_наименовани_fkey foreign key (ид_наименования) references public.наименование_запчасти(ид_наименования),
	constraint детализация_ремо_ид_типа_услуги_fkey foreign key (ид_типа_услуги) references public.тип_услуги(ид_типа_услуги),
	constraint детализация_ремонта_ид_заказа_fkey foreign key (ид_заказа) references public.заказ_наряд(ид_заказа) on
delete
    cascade
);
