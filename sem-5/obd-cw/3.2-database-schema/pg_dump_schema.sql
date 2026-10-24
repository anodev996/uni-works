--
-- PostgreSQL database dump
--

\restrict i8oaC04RDDT761ZUxmSAddnUqG0YHV8wI5aIkxg5RDytpTH5QK8GhxpPQXelJpw

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: автомобиль; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."автомобиль" (
    "ид_автомобиля" integer NOT NULL,
    "ид_модели" integer NOT NULL,
    "ид_типа_кузова" integer NOT NULL,
    "ид_типа_привода" integer NOT NULL,
    "ид_типа_кпп" integer NOT NULL,
    "ид_цвета" integer NOT NULL,
    "ид_страны" integer NOT NULL,
    "ид_статуса" integer NOT NULL,
    "vin_номер" character varying(17) NOT NULL,
    "год_выпуска" integer NOT NULL,
    "пробег" integer DEFAULT 0 NOT NULL,
    "цена" numeric(12,2) NOT NULL,
    "фотография" bytea
);


ALTER TABLE public."автомобиль" OWNER TO postgres;

--
-- Name: автомобиль_ид_автомобиля_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."автомобиль_ид_автомобиля_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."автомобиль_ид_автомобиля_seq" OWNER TO postgres;

--
-- Name: автомобиль_ид_автомобиля_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."автомобиль_ид_автомобиля_seq" OWNED BY public."автомобиль"."ид_автомобиля";


--
-- Name: вид_запчасти; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."вид_запчасти" (
    "ид_вида_запчасти" integer NOT NULL,
    "наименование_вида_запчасти" character varying(100) CONSTRAINT "вид_запчасти_наименование_ви_not_null" NOT NULL
);


ALTER TABLE public."вид_запчасти" OWNER TO postgres;

--
-- Name: вид_запчасти_ид_вида_запчасти_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."вид_запчасти_ид_вида_запчасти_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."вид_запчасти_ид_вида_запчасти_seq" OWNER TO postgres;

--
-- Name: вид_запчасти_ид_вида_запчасти_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."вид_запчасти_ид_вида_запчасти_seq" OWNED BY public."вид_запчасти"."ид_вида_запчасти";


--
-- Name: детализация_ремонта; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."детализация_ремонта" (
    "ид_детализации" integer CONSTRAINT "детализация_ре_ид_детализаци_not_null" NOT NULL,
    "ид_заказа" integer CONSTRAINT "детализация_ремонт_ид_заказа_not_null" NOT NULL,
    "ид_типа_услуги" integer,
    "ид_наименования" integer,
    "количество_запчастей" integer DEFAULT 1 CONSTRAINT "детализация_ре_количество_за_not_null" NOT NULL,
    "стоимость" numeric(12,2) CONSTRAINT "детализация_ремонт_стоимость_not_null" NOT NULL
);


ALTER TABLE public."детализация_ремонта" OWNER TO postgres;

--
-- Name: детализация_ремо_ид_детализации_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."детализация_ремо_ид_детализации_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."детализация_ремо_ид_детализации_seq" OWNER TO postgres;

--
-- Name: детализация_ремо_ид_детализации_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."детализация_ремо_ид_детализации_seq" OWNED BY public."детализация_ремонта"."ид_детализации";


--
-- Name: договор_купли_продажи; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."договор_купли_продажи" (
    "ид_договора" integer CONSTRAINT "договор_купли_про_ид_договора_not_null" NOT NULL,
    "ид_автомобиля" integer CONSTRAINT "договор_купли_п_ид_автомобиля_not_null" NOT NULL,
    "ид_клиента" integer CONSTRAINT "договор_купли_прод_ид_клиента_not_null" NOT NULL,
    "ид_сотрудника" integer CONSTRAINT "договор_купли_п_ид_сотрудника_not_null" NOT NULL,
    "дата_время_сделки" timestamp without time zone CONSTRAINT "договор_купли__дата_время_сде_not_null" NOT NULL,
    "итоговая_сумма" numeric(12,2) CONSTRAINT "договор_купли__итоговая_сумм_not_null" NOT NULL,
    "тип_расчета" character varying(50) CONSTRAINT "договор_купли_про_тип_расчета_not_null" NOT NULL
);


ALTER TABLE public."договор_купли_продажи" OWNER TO postgres;

--
-- Name: договор_купли_прода_ид_договора_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."договор_купли_прода_ид_договора_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."договор_купли_прода_ид_договора_seq" OWNER TO postgres;

--
-- Name: договор_купли_прода_ид_договора_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."договор_купли_прода_ид_договора_seq" OWNED BY public."договор_купли_продажи"."ид_договора";


--
-- Name: должность; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."должность" (
    "ид_должности" integer NOT NULL,
    "наименование_должности" character varying(100) CONSTRAINT "должность_наименование_должн_not_null" NOT NULL
);


ALTER TABLE public."должность" OWNER TO postgres;

--
-- Name: должность_ид_должности_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."должность_ид_должности_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."должность_ид_должности_seq" OWNER TO postgres;

--
-- Name: должность_ид_должности_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."должность_ид_должности_seq" OWNED BY public."должность"."ид_должности";


--
-- Name: заказ_наряд; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."заказ_наряд" (
    "ид_заказа" integer NOT NULL,
    "ид_клиента" integer NOT NULL,
    "ид_сотрудника" integer NOT NULL,
    "vin_автомобиля_клиента" character varying(17) CONSTRAINT "заказ_наряд_vin_автомобиля_кли_not_null" NOT NULL,
    "дата_заезда" date NOT NULL,
    "планируемая_дата_выдачи" date,
    "статус_готовности" character varying(50) CONSTRAINT "заказ_наряд_статус_готовност_not_null" NOT NULL
);


ALTER TABLE public."заказ_наряд" OWNER TO postgres;

--
-- Name: заказ_наряд_ид_заказа_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."заказ_наряд_ид_заказа_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."заказ_наряд_ид_заказа_seq" OWNER TO postgres;

--
-- Name: заказ_наряд_ид_заказа_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."заказ_наряд_ид_заказа_seq" OWNED BY public."заказ_наряд"."ид_заказа";


--
-- Name: заявка_на_тест_драйв; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."заявка_на_тест_драйв" (
    "ид_заявки" integer CONSTRAINT "заявка_на_тест_драй_ид_заявки_not_null" NOT NULL,
    "ид_автомобиля" integer CONSTRAINT "заявка_на_тест__ид_автомобиля_not_null" NOT NULL,
    "ид_клиента" integer CONSTRAINT "заявка_на_тест_дра_ид_клиента_not_null" NOT NULL,
    "дата_тест_драйва" date CONSTRAINT "заявка_на_тест__дата_тест_драй_not_null" NOT NULL,
    "время_тест_драйва" time without time zone CONSTRAINT "заявка_на_тест__время_тест_дра_not_null" NOT NULL,
    "комментарий" text
);


ALTER TABLE public."заявка_на_тест_драйв" OWNER TO postgres;

--
-- Name: заявка_на_тест_драйв_ид_заявки_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."заявка_на_тест_драйв_ид_заявки_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."заявка_на_тест_драйв_ид_заявки_seq" OWNER TO postgres;

--
-- Name: заявка_на_тест_драйв_ид_заявки_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."заявка_на_тест_драйв_ид_заявки_seq" OWNED BY public."заявка_на_тест_драйв"."ид_заявки";


--
-- Name: клиент; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."клиент" (
    "ид_клиента" integer NOT NULL,
    "фамилия" character varying(100) NOT NULL,
    "имя" character varying(100) NOT NULL,
    "отчество" character varying(100),
    "электронная_почта" character varying(255) NOT NULL,
    "номер_телефона" character varying(20) NOT NULL,
    "хеш_пароля" character varying(255) NOT NULL
);


ALTER TABLE public."клиент" OWNER TO postgres;

--
-- Name: клиент_ид_клиента_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."клиент_ид_клиента_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."клиент_ид_клиента_seq" OWNER TO postgres;

--
-- Name: клиент_ид_клиента_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."клиент_ид_клиента_seq" OWNED BY public."клиент"."ид_клиента";


--
-- Name: модель_авто; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."модель_авто" (
    "ид_модели" integer NOT NULL,
    "марка" character varying(100) NOT NULL,
    "модель" character varying(100) NOT NULL
);


ALTER TABLE public."модель_авто" OWNER TO postgres;

--
-- Name: модель_авто_ид_модели_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."модель_авто_ид_модели_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."модель_авто_ид_модели_seq" OWNER TO postgres;

--
-- Name: модель_авто_ид_модели_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."модель_авто_ид_модели_seq" OWNED BY public."модель_авто"."ид_модели";


--
-- Name: наименование_запчасти; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."наименование_запчасти" (
    "ид_наименования" integer CONSTRAINT "наименование_з_ид_наименован_not_null" NOT NULL,
    "ид_вида_запчасти" integer CONSTRAINT "наименование_з_ид_вида_запчас_not_null" NOT NULL,
    "наименование_запчасти" character varying(255) CONSTRAINT "наименование_з_наименование__not_null" NOT NULL,
    "артикул" character varying(100) CONSTRAINT "наименование_запчаст_артикул_not_null" NOT NULL
);


ALTER TABLE public."наименование_запчасти" OWNER TO postgres;

--
-- Name: наименование_за_ид_наименования_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."наименование_за_ид_наименования_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."наименование_за_ид_наименования_seq" OWNER TO postgres;

--
-- Name: наименование_за_ид_наименования_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."наименование_за_ид_наименования_seq" OWNED BY public."наименование_запчасти"."ид_наименования";


--
-- Name: платеж_бронирования; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."платеж_бронирования" (
    "ид_платежа" integer CONSTRAINT "платеж_бронирован_ид_платежа_not_null" NOT NULL,
    "ид_автомобиля" integer CONSTRAINT "платеж_брониро_ид_автомобиля_not_null" NOT NULL,
    "сумма_платежа" numeric(12,2) CONSTRAINT "платеж_брониро_сумма_платежа_not_null" NOT NULL,
    "дата_время_платежа" timestamp without time zone CONSTRAINT "платеж_брониро_дата_время_пла_not_null" NOT NULL,
    "статус_платежа" character varying(50) CONSTRAINT "платеж_брониро_статус_платеж_not_null" NOT NULL
);


ALTER TABLE public."платеж_бронирования" OWNER TO postgres;

--
-- Name: платеж_бронирования_ид_платежа_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."платеж_бронирования_ид_платежа_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."платеж_бронирования_ид_платежа_seq" OWNER TO postgres;

--
-- Name: платеж_бронирования_ид_платежа_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."платеж_бронирования_ид_платежа_seq" OWNED BY public."платеж_бронирования"."ид_платежа";


--
-- Name: сотрудник; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."сотрудник" (
    "ид_сотрудника" integer NOT NULL,
    "ид_должности" integer NOT NULL,
    "фамилия" character varying(100) NOT NULL,
    "имя" character varying(100) NOT NULL,
    "отчество" character varying(100),
    "номер_телефона" character varying(20) NOT NULL
);


ALTER TABLE public."сотрудник" OWNER TO postgres;

--
-- Name: сотрудник_ид_сотрудника_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."сотрудник_ид_сотрудника_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."сотрудник_ид_сотрудника_seq" OWNER TO postgres;

--
-- Name: сотрудник_ид_сотрудника_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."сотрудник_ид_сотрудника_seq" OWNED BY public."сотрудник"."ид_сотрудника";


--
-- Name: статус_автомобиля; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."статус_автомобиля" (
    "ид_статуса" integer NOT NULL,
    "наименование_статуса" character varying(100) CONSTRAINT "статус_автомоб_наименование__not_null" NOT NULL
);


ALTER TABLE public."статус_автомобиля" OWNER TO postgres;

--
-- Name: статус_автомобиля_ид_статуса_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."статус_автомобиля_ид_статуса_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."статус_автомобиля_ид_статуса_seq" OWNER TO postgres;

--
-- Name: статус_автомобиля_ид_статуса_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."статус_автомобиля_ид_статуса_seq" OWNED BY public."статус_автомобиля"."ид_статуса";


--
-- Name: страна_производства; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."страна_производства" (
    "ид_страны" integer CONSTRAINT "страна_производств_ид_страны_not_null" NOT NULL,
    "название_страны" character varying(100) CONSTRAINT "страна_произво_название_стра_not_null" NOT NULL
);


ALTER TABLE public."страна_производства" OWNER TO postgres;

--
-- Name: страна_производства_ид_страны_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."страна_производства_ид_страны_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."страна_производства_ид_страны_seq" OWNER TO postgres;

--
-- Name: страна_производства_ид_страны_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."страна_производства_ид_страны_seq" OWNED BY public."страна_производства"."ид_страны";


--
-- Name: тип_кпп; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."тип_кпп" (
    "ид_типа_кпп" integer NOT NULL,
    "наименование_типа_кпп" character varying(100) NOT NULL
);


ALTER TABLE public."тип_кпп" OWNER TO postgres;

--
-- Name: тип_кпп_ид_типа_кпп_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."тип_кпп_ид_типа_кпп_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."тип_кпп_ид_типа_кпп_seq" OWNER TO postgres;

--
-- Name: тип_кпп_ид_типа_кпп_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."тип_кпп_ид_типа_кпп_seq" OWNED BY public."тип_кпп"."ид_типа_кпп";


--
-- Name: тип_кузова; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."тип_кузова" (
    "ид_типа_кузова" integer NOT NULL,
    "наименование_типа_кузова" character varying(100) CONSTRAINT "тип_кузова_наименование_типа__not_null" NOT NULL
);


ALTER TABLE public."тип_кузова" OWNER TO postgres;

--
-- Name: тип_кузова_ид_типа_кузова_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."тип_кузова_ид_типа_кузова_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."тип_кузова_ид_типа_кузова_seq" OWNER TO postgres;

--
-- Name: тип_кузова_ид_типа_кузова_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."тип_кузова_ид_типа_кузова_seq" OWNED BY public."тип_кузова"."ид_типа_кузова";


--
-- Name: тип_привода; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."тип_привода" (
    "ид_типа_привода" integer NOT NULL,
    "наименование_типа_привода" character varying(100) CONSTRAINT "тип_привода_наименование_тип_not_null" NOT NULL
);


ALTER TABLE public."тип_привода" OWNER TO postgres;

--
-- Name: тип_привода_ид_типа_привода_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."тип_привода_ид_типа_привода_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."тип_привода_ид_типа_привода_seq" OWNER TO postgres;

--
-- Name: тип_привода_ид_типа_привода_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."тип_привода_ид_типа_привода_seq" OWNED BY public."тип_привода"."ид_типа_привода";


--
-- Name: тип_услуги; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."тип_услуги" (
    "ид_типа_услуги" integer NOT NULL,
    "наименование_услуги" character varying(255) CONSTRAINT "тип_услуги_наименование_услу_not_null" NOT NULL,
    "базовая_стоимость" numeric(12,2) NOT NULL
);


ALTER TABLE public."тип_услуги" OWNER TO postgres;

--
-- Name: тип_услуги_ид_типа_услуги_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."тип_услуги_ид_типа_услуги_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."тип_услуги_ид_типа_услуги_seq" OWNER TO postgres;

--
-- Name: тип_услуги_ид_типа_услуги_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."тип_услуги_ид_типа_услуги_seq" OWNED BY public."тип_услуги"."ид_типа_услуги";


--
-- Name: цвет; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."цвет" (
    "ид_цвета" integer NOT NULL,
    "наименование_цвета" character varying(100) NOT NULL,
    "хекс_код" character varying(7)
);


ALTER TABLE public."цвет" OWNER TO postgres;

--
-- Name: цвет_ид_цвета_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."цвет_ид_цвета_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."цвет_ид_цвета_seq" OWNER TO postgres;

--
-- Name: цвет_ид_цвета_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."цвет_ид_цвета_seq" OWNED BY public."цвет"."ид_цвета";


--
-- Name: автомобиль ид_автомобиля; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль" ALTER COLUMN "ид_автомобиля" SET DEFAULT nextval('public."автомобиль_ид_автомобиля_seq"'::regclass);


--
-- Name: вид_запчасти ид_вида_запчасти; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."вид_запчасти" ALTER COLUMN "ид_вида_запчасти" SET DEFAULT nextval('public."вид_запчасти_ид_вида_запчасти_seq"'::regclass);


--
-- Name: детализация_ремонта ид_детализации; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."детализация_ремонта" ALTER COLUMN "ид_детализации" SET DEFAULT nextval('public."детализация_ремо_ид_детализации_seq"'::regclass);


--
-- Name: договор_купли_продажи ид_договора; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."договор_купли_продажи" ALTER COLUMN "ид_договора" SET DEFAULT nextval('public."договор_купли_прода_ид_договора_seq"'::regclass);


--
-- Name: должность ид_должности; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."должность" ALTER COLUMN "ид_должности" SET DEFAULT nextval('public."должность_ид_должности_seq"'::regclass);


--
-- Name: заказ_наряд ид_заказа; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заказ_наряд" ALTER COLUMN "ид_заказа" SET DEFAULT nextval('public."заказ_наряд_ид_заказа_seq"'::regclass);


--
-- Name: заявка_на_тест_драйв ид_заявки; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заявка_на_тест_драйв" ALTER COLUMN "ид_заявки" SET DEFAULT nextval('public."заявка_на_тест_драйв_ид_заявки_seq"'::regclass);


--
-- Name: клиент ид_клиента; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."клиент" ALTER COLUMN "ид_клиента" SET DEFAULT nextval('public."клиент_ид_клиента_seq"'::regclass);


--
-- Name: модель_авто ид_модели; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."модель_авто" ALTER COLUMN "ид_модели" SET DEFAULT nextval('public."модель_авто_ид_модели_seq"'::regclass);


--
-- Name: наименование_запчасти ид_наименования; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."наименование_запчасти" ALTER COLUMN "ид_наименования" SET DEFAULT nextval('public."наименование_за_ид_наименования_seq"'::regclass);


--
-- Name: платеж_бронирования ид_платежа; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."платеж_бронирования" ALTER COLUMN "ид_платежа" SET DEFAULT nextval('public."платеж_бронирования_ид_платежа_seq"'::regclass);


--
-- Name: сотрудник ид_сотрудника; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."сотрудник" ALTER COLUMN "ид_сотрудника" SET DEFAULT nextval('public."сотрудник_ид_сотрудника_seq"'::regclass);


--
-- Name: статус_автомобиля ид_статуса; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."статус_автомобиля" ALTER COLUMN "ид_статуса" SET DEFAULT nextval('public."статус_автомобиля_ид_статуса_seq"'::regclass);


--
-- Name: страна_производства ид_страны; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."страна_производства" ALTER COLUMN "ид_страны" SET DEFAULT nextval('public."страна_производства_ид_страны_seq"'::regclass);


--
-- Name: тип_кпп ид_типа_кпп; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_кпп" ALTER COLUMN "ид_типа_кпп" SET DEFAULT nextval('public."тип_кпп_ид_типа_кпп_seq"'::regclass);


--
-- Name: тип_кузова ид_типа_кузова; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_кузова" ALTER COLUMN "ид_типа_кузова" SET DEFAULT nextval('public."тип_кузова_ид_типа_кузова_seq"'::regclass);


--
-- Name: тип_привода ид_типа_привода; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_привода" ALTER COLUMN "ид_типа_привода" SET DEFAULT nextval('public."тип_привода_ид_типа_привода_seq"'::regclass);


--
-- Name: тип_услуги ид_типа_услуги; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_услуги" ALTER COLUMN "ид_типа_услуги" SET DEFAULT nextval('public."тип_услуги_ид_типа_услуги_seq"'::regclass);


--
-- Name: цвет ид_цвета; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."цвет" ALTER COLUMN "ид_цвета" SET DEFAULT nextval('public."цвет_ид_цвета_seq"'::regclass);


--
-- Name: автомобиль автомобиль_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_pkey" PRIMARY KEY ("ид_автомобиля");


--
-- Name: автомобиль автомобиль_vin_номер_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_vin_номер_key" UNIQUE ("vin_номер");


--
-- Name: вид_запчасти вид_запчасти_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."вид_запчасти"
    ADD CONSTRAINT "вид_запчасти_pkey" PRIMARY KEY ("ид_вида_запчасти");


--
-- Name: детализация_ремонта детализация_ремонта_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."детализация_ремонта"
    ADD CONSTRAINT "детализация_ремонта_pkey" PRIMARY KEY ("ид_детализации");


--
-- Name: договор_купли_продажи договор_купли_продажи_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."договор_купли_продажи"
    ADD CONSTRAINT "договор_купли_продажи_pkey" PRIMARY KEY ("ид_договора");


--
-- Name: должность должность_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."должность"
    ADD CONSTRAINT "должность_pkey" PRIMARY KEY ("ид_должности");


--
-- Name: заказ_наряд заказ_наряд_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заказ_наряд"
    ADD CONSTRAINT "заказ_наряд_pkey" PRIMARY KEY ("ид_заказа");


--
-- Name: заявка_на_тест_драйв заявка_на_тест_драйв_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заявка_на_тест_драйв"
    ADD CONSTRAINT "заявка_на_тест_драйв_pkey" PRIMARY KEY ("ид_заявки");


--
-- Name: клиент клиент_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."клиент"
    ADD CONSTRAINT "клиент_pkey" PRIMARY KEY ("ид_клиента");


--
-- Name: клиент клиент_электронная_почта_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."клиент"
    ADD CONSTRAINT "клиент_электронная_почта_key" UNIQUE ("электронная_почта");


--
-- Name: модель_авто модель_авто_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."модель_авто"
    ADD CONSTRAINT "модель_авто_pkey" PRIMARY KEY ("ид_модели");


--
-- Name: наименование_запчасти наименование_запчасти_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."наименование_запчасти"
    ADD CONSTRAINT "наименование_запчасти_pkey" PRIMARY KEY ("ид_наименования");


--
-- Name: наименование_запчасти наименование_запчасти_артикул_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."наименование_запчасти"
    ADD CONSTRAINT "наименование_запчасти_артикул_key" UNIQUE ("артикул");


--
-- Name: платеж_бронирования платеж_бронирования_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."платеж_бронирования"
    ADD CONSTRAINT "платеж_бронирования_pkey" PRIMARY KEY ("ид_платежа");


--
-- Name: сотрудник сотрудник_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."сотрудник"
    ADD CONSTRAINT "сотрудник_pkey" PRIMARY KEY ("ид_сотрудника");


--
-- Name: статус_автомобиля статус_автомобиля_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."статус_автомобиля"
    ADD CONSTRAINT "статус_автомобиля_pkey" PRIMARY KEY ("ид_статуса");


--
-- Name: страна_производства страна_производства_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."страна_производства"
    ADD CONSTRAINT "страна_производства_pkey" PRIMARY KEY ("ид_страны");


--
-- Name: тип_кпп тип_кпп_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_кпп"
    ADD CONSTRAINT "тип_кпп_pkey" PRIMARY KEY ("ид_типа_кпп");


--
-- Name: тип_кузова тип_кузова_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_кузова"
    ADD CONSTRAINT "тип_кузова_pkey" PRIMARY KEY ("ид_типа_кузова");


--
-- Name: тип_привода тип_привода_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_привода"
    ADD CONSTRAINT "тип_привода_pkey" PRIMARY KEY ("ид_типа_привода");


--
-- Name: тип_услуги тип_услуги_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."тип_услуги"
    ADD CONSTRAINT "тип_услуги_pkey" PRIMARY KEY ("ид_типа_услуги");


--
-- Name: цвет цвет_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."цвет"
    ADD CONSTRAINT "цвет_pkey" PRIMARY KEY ("ид_цвета");


--
-- Name: автомобиль автомобиль_ид_модели_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_модели_fkey" FOREIGN KEY ("ид_модели") REFERENCES public."модель_авто"("ид_модели");


--
-- Name: автомобиль автомобиль_ид_статуса_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_статуса_fkey" FOREIGN KEY ("ид_статуса") REFERENCES public."статус_автомобиля"("ид_статуса");


--
-- Name: автомобиль автомобиль_ид_страны_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_страны_fkey" FOREIGN KEY ("ид_страны") REFERENCES public."страна_производства"("ид_страны");


--
-- Name: автомобиль автомобиль_ид_типа_кпп_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_типа_кпп_fkey" FOREIGN KEY ("ид_типа_кпп") REFERENCES public."тип_кпп"("ид_типа_кпп");


--
-- Name: автомобиль автомобиль_ид_типа_кузова_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_типа_кузова_fkey" FOREIGN KEY ("ид_типа_кузова") REFERENCES public."тип_кузова"("ид_типа_кузова");


--
-- Name: автомобиль автомобиль_ид_типа_привода_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_типа_привода_fkey" FOREIGN KEY ("ид_типа_привода") REFERENCES public."тип_привода"("ид_типа_привода");


--
-- Name: автомобиль автомобиль_ид_цвета_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."автомобиль"
    ADD CONSTRAINT "автомобиль_ид_цвета_fkey" FOREIGN KEY ("ид_цвета") REFERENCES public."цвет"("ид_цвета");


--
-- Name: детализация_ремонта детализация_рем_ид_наименовани_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."детализация_ремонта"
    ADD CONSTRAINT "детализация_рем_ид_наименовани_fkey" FOREIGN KEY ("ид_наименования") REFERENCES public."наименование_запчасти"("ид_наименования");


--
-- Name: детализация_ремонта детализация_ремо_ид_типа_услуги_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."детализация_ремонта"
    ADD CONSTRAINT "детализация_ремо_ид_типа_услуги_fkey" FOREIGN KEY ("ид_типа_услуги") REFERENCES public."тип_услуги"("ид_типа_услуги");


--
-- Name: детализация_ремонта детализация_ремонта_ид_заказа_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."детализация_ремонта"
    ADD CONSTRAINT "детализация_ремонта_ид_заказа_fkey" FOREIGN KEY ("ид_заказа") REFERENCES public."заказ_наряд"("ид_заказа") ON DELETE CASCADE;


--
-- Name: договор_купли_продажи договор_купли_про_ид_автомобиля_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."договор_купли_продажи"
    ADD CONSTRAINT "договор_купли_про_ид_автомобиля_fkey" FOREIGN KEY ("ид_автомобиля") REFERENCES public."автомобиль"("ид_автомобиля");


--
-- Name: договор_купли_продажи договор_купли_про_ид_сотрудника_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."договор_купли_продажи"
    ADD CONSTRAINT "договор_купли_про_ид_сотрудника_fkey" FOREIGN KEY ("ид_сотрудника") REFERENCES public."сотрудник"("ид_сотрудника");


--
-- Name: договор_купли_продажи договор_купли_продаж_ид_клиента_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."договор_купли_продажи"
    ADD CONSTRAINT "договор_купли_продаж_ид_клиента_fkey" FOREIGN KEY ("ид_клиента") REFERENCES public."клиент"("ид_клиента");


--
-- Name: заказ_наряд заказ_наряд_ид_клиента_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заказ_наряд"
    ADD CONSTRAINT "заказ_наряд_ид_клиента_fkey" FOREIGN KEY ("ид_клиента") REFERENCES public."клиент"("ид_клиента");


--
-- Name: заказ_наряд заказ_наряд_ид_сотрудника_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заказ_наряд"
    ADD CONSTRAINT "заказ_наряд_ид_сотрудника_fkey" FOREIGN KEY ("ид_сотрудника") REFERENCES public."сотрудник"("ид_сотрудника");


--
-- Name: заявка_на_тест_драйв заявка_на_тест_др_ид_автомобиля_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заявка_на_тест_драйв"
    ADD CONSTRAINT "заявка_на_тест_др_ид_автомобиля_fkey" FOREIGN KEY ("ид_автомобиля") REFERENCES public."автомобиль"("ид_автомобиля");


--
-- Name: заявка_на_тест_драйв заявка_на_тест_драйв_ид_клиента_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."заявка_на_тест_драйв"
    ADD CONSTRAINT "заявка_на_тест_драйв_ид_клиента_fkey" FOREIGN KEY ("ид_клиента") REFERENCES public."клиент"("ид_клиента");


--
-- Name: наименование_запчасти наименование_за_ид_вида_запчаст_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."наименование_запчасти"
    ADD CONSTRAINT "наименование_за_ид_вида_запчаст_fkey" FOREIGN KEY ("ид_вида_запчасти") REFERENCES public."вид_запчасти"("ид_вида_запчасти");


--
-- Name: платеж_бронирования платеж_бронирова_ид_автомобиля_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."платеж_бронирования"
    ADD CONSTRAINT "платеж_бронирова_ид_автомобиля_fkey" FOREIGN KEY ("ид_автомобиля") REFERENCES public."автомобиль"("ид_автомобиля");


--
-- Name: сотрудник сотрудник_ид_должности_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."сотрудник"
    ADD CONSTRAINT "сотрудник_ид_должности_fkey" FOREIGN KEY ("ид_должности") REFERENCES public."должность"("ид_должности");


--
-- PostgreSQL database dump complete
--

\unrestrict i8oaC04RDDT761ZUxmSAddnUqG0YHV8wI5aIkxg5RDytpTH5QK8GhxpPQXelJpw

