# neo4j-graph-databases-lab3

## 1. Экспресс-курс: графовые СУБД, Neo4j и синтаксис Cypher

### Модель Property Graph

Графовые базы данных предназначены для хранения данных, чья ценность сосредоточена в связях между ними. В отличие от реляционных таблиц, где связи эмулируются внешними ключами и собираются ресурсоемкими операциями `JOIN`, в модели **Property Graph (графа свойств)** связи являются полноправными физическими объектами базы данных.

  

Модель состоит из четырех базовых элементов:

  

1. **Узлы (Nodes)** — сущности реального мира (аналог строк в таблицах).
    
      
    
2. **Метки (Labels)** — категории или типы сущностей (например, `:Person`, `:Movie`). Узел может иметь ноль, одну или несколько меток.
    
      
    
3. **Связи (Relationships)** — ориентированные ребра, связывающие строго два узла. Связь всегда имеет направление и ровно один тип (например, `-[:ACTED_IN]->`).
    
      
    
4. **Свойства (Properties)** — именованные значения (пары «ключ-значение»), которые могут храниться как внутри узлов, так и внутри связей.
    
      
    

Ключевое архитектурное преимущество Neo4j — **безындексная смежность (index-free adjacency)**: каждый узел физически хранит прямые указатели на соседние связанные узлы. Сложность обхода графа зависит только от локального подграфа ($O(k)$), а не от общего объема всей базы данных ($O(\log N)$).

  

### Основы языка Cypher

Cypher — декларативный язык запросов к графам, использующий визуальный ASCII-art синтаксис:

  

- Круглые скобки `(n)` обозначают **узел**.
    
      
    
- Квадратные скобки `[r]` внутри дефисов и стрелок `-->` обозначают **связь**.
    
      
    
- Двоеточие `:` отделяет переменную от метки или типа (`(p:Person)`, `-[r:WORKS_IN]->`).
    
      
    
- Фигурные скобки `{}` задают свойства (`{name: 'Alice', age: 29}`).
    
      
    

Базовые конструкции Cypher:

  

- `CREATE` — создание узлов и связей.
    
      
    
- `MATCH` — поиск паттерна в графе (аналог `SELECT ... FROM`).
    
      
    
- `WHERE` — фильтрация результатов по предикатам.
    
      
    
- `RETURN` — проекция и вывод найденных данных.
    
      
    
- `SET` / `REMOVE` — добавление, изменение или удаление свойств и меток.
    
      
    
- `DELETE` / `DETACH DELETE` — удаление узлов и связей (`DETACH` удаляет узел вместе со всеми инцидентными связями).
    
      
    
- `MERGE` — идемпотентное создание (найти узел/связь, если есть; если нет — создать).
    
      
    

## 2. Выполнение лабораторной работы

По заданию лабораторной работы требуется построить граф предметной области, содержащий **5 различных типов узлов (labels), по 5 узлов каждого типа (всего 25 узлов)**, а также сформировать связи между ними.

  

В качестве предметной области выбрана **«ИТ-компания: проекты, команды, технологии и клиенты»**:

  

1. `:Employee` (Сотрудники) — 5 узлов.
    
      
    
2. `:Department` (Отделы) — 5 узлов.
    
      
    
3. `:Project` (Проекты) — 5 узлов.
    
      
    
4. `:Technology` (Стек технологий) — 5 узлов.
    
      
    
5. `:Client` (Заказчики) — 5 узлов.
    
      
    

### Блок 1: Очистка базы данных

Перед наполнением графа удаляются все существующие узлы и связи, чтобы исключить конфликты данных.

  

#### Вариант 1 (без комментариев)

Cypher

```
MATCH (n)
DETACH DELETE n;
```

#### Вариант 2 (с обширными комментариями)

Cypher

```
// MATCH (n) находит абсолютно все узлы графа в рабочей базе данных, связывая каждый узел с переменной n.
MATCH (n)
// DETACH DELETE n принудительно удаляет найденные узлы вместе со всеми входящими и исходящими связями.
// Если опустить ключевое слово DETACH, Neo4j вернет ошибку при попытке удалить узел, имеющий активные связи.
DETACH DELETE n;
```

### Блок 2: Создание 25 узлов (5 типов по 5 узлов)

Создаются узлы для 5 типов сущностей с уникальными свойствами.

  

#### Вариант 1 (без комментариев)

Cypher

```
CREATE 
  (emp1:Employee {id: 'E01', name: 'Алексей Смирнов', position: 'Tech Lead', salary: 320000}),
  (emp2:Employee {id: 'E02', name: 'Елена Кузнецова', position: 'Backend Developer', salary: 220000}),
  (emp3:Employee {id: 'E03', name: 'Дмитрий Попов', position: 'Data Engineer', salary: 240000}),
  (emp4:Employee {id: 'E04', name: 'Ольга Соколова', position: 'Frontend Developer', salary: 190000}),
  (emp5:Employee {id: 'E05', name: 'Иван Морозов', position: 'DevOps Engineer', salary: 260000}),

  (dept1:Department {code: 'D01', name: 'Отдел бэкенд-разработки', floor: 4}),
  (dept2:Department {code: 'D02', name: 'Отдел фронтенда', floor: 4}),
  (dept3:Department {code: 'D03', name: 'Отдел анализа данных', floor: 5}),
  (dept4:Department {code: 'D04', name: 'Отдел инфраструктуры и Cloud', floor: 3}),
  (dept5:Department {code: 'D05', name: 'Отдел обеспечения качества', floor: 3}),

  (proj1:Project {code: 'P01', title: 'Финтех-платформа', budget: 15000000, status: 'Active'}),
  (proj2:Project {code: 'P02', title: 'Мобильный банк', budget: 8500000, status: 'Active'}),
  (proj3:Project {code: 'P03', title: 'Система антифрода', budget: 12000000, status: 'Active'}),
  (proj4:Project {code: 'P04', title: 'Витрина маркетплейса', budget: 6000000, status: 'Planning'}),
  (proj5:Project {code: 'P05', title: 'Архивный шлюз интеграции', budget: 2000000, status: 'Completed'}),

  (tech1:Technology {id: 'T01', name: 'Neo4j', category: 'Database', stableVersion: '5.x'}),
  (tech2:Technology {id: 'T02', name: 'Python', category: 'Programming Language', stableVersion: '3.12'}),
  (tech3:Technology {id: 'T03', name: 'Go', category: 'Programming Language', stableVersion: '1.22'}),
  (tech4:Technology {id: 'T04', name: 'TypeScript', category: 'Frontend', stableVersion: '5.4'}),
  (tech5:Technology {id: 'T05', name: 'Kubernetes', category: 'Infrastructure', stableVersion: '1.30'}),

  (cli1:Client {regNo: 'C01', companyName: 'Альфа-Инвест', industry: 'Финансы', tier: 'Enterprise'}),
  (cli2:Client {regNo: 'C02', companyName: 'СберТехнологии', industry: 'Банкинг', tier: 'Enterprise'}),
  (cli3:Client {regNo: 'C03', companyName: 'РитейлСити', industry: 'Электронная коммерция', tier: 'Middle'}),
  (cli4:Client {regNo: 'C04', companyName: 'МедКонсалт', industry: 'Здравоохранение', tier: 'Small'}),
  (cli5:Client {regNo: 'C05', companyName: 'ПромЛогистика', industry: 'Транспорт', tier: 'Middle'});
```

#### Вариант 2 (с обширными комментариями)

Cypher

```
// Команда CREATE формирует новые структуры в базе данных без предварительной сверки их существования.
// Каждый узел описывается в круглых скобках: (псевдоним:Метка {ключ: значение}).
// Псевдонимы (emp1, dept1 и т.д.) действуют локально в рамках одной транзакции выполнения запроса.
CREATE 
  // ---------------------------------------------------------------------------
  // ТИП 1: Employee (Сотрудники компании) - 5 узлов
  // ---------------------------------------------------------------------------
  (emp1:Employee {id: 'E01', name: 'Алексей Смирнов', position: 'Tech Lead', salary: 320000}),
  (emp2:Employee {id: 'E02', name: 'Елена Кузнецова', position: 'Backend Developer', salary: 220000}),
  (emp3:Employee {id: 'E03', name: 'Дмитрий Попов', position: 'Data Engineer', salary: 240000}),
  (emp4:Employee {id: 'E04', name: 'Ольга Соколова', position: 'Frontend Developer', salary: 190000}),
  (emp5:Employee {id: 'E05', name: 'Иван Морозов', position: 'DevOps Engineer', salary: 260000}),

  // ---------------------------------------------------------------------------
  // ТИП 2: Department (Департаменты / подразделения) - 5 узлов
  // ---------------------------------------------------------------------------
  (dept1:Department {code: 'D01', name: 'Отдел бэкенд-разработки', floor: 4}),
  (dept2:Department {code: 'D02', name: 'Отдел фронтенда', floor: 4}),
  (dept3:Department {code: 'D03', name: 'Отдел анализа данных', floor: 5}),
  (dept4:Department {code: 'D04', name: 'Отдел инфраструктуры и Cloud', floor: 3}),
  // Данный отдел намеренно оставлен без прикрепленных сотрудников для демонстрации несвязанности графа
  (dept5:Department {code: 'D05', name: 'Отдел обеспечения качества', floor: 3}),

  // ---------------------------------------------------------------------------
  // ТИП 3: Project (Проекты компании) - 5 узлов
  // ---------------------------------------------------------------------------
  (proj1:Project {code: 'P01', title: 'Финтех-платформа', budget: 15000000, status: 'Active'}),
  (proj2:Project {code: 'P02', title: 'Мобильный банк', budget: 8500000, status: 'Active'}),
  (proj3:Project {code: 'P03', title: 'Система антифрода', budget: 12000000, status: 'Active'}),
  (proj4:Project {code: 'P04', title: 'Витрина маркетплейса', budget: 6000000, status: 'Planning'}),
  (proj5:Project {code: 'P05', title: 'Архивный шлюз интеграции', budget: 2000000, status: 'Completed'}),

  // ---------------------------------------------------------------------------
  // ТИП 4: Technology (Технологический стек) - 5 узлов
  // ---------------------------------------------------------------------------
  (tech1:Technology {id: 'T01', name: 'Neo4j', category: 'Database', stableVersion: '5.x'}),
  (tech2:Technology {id: 'T02', name: 'Python', category: 'Programming Language', stableVersion: '3.12'}),
  (tech3:Technology {id: 'T03', name: 'Go', category: 'Programming Language', stableVersion: '1.22'}),
  (tech4:Technology {id: 'T04', name: 'TypeScript', category: 'Frontend', stableVersion: '5.4'}),
  (tech5:Technology {id: 'T05', name: 'Kubernetes', category: 'Infrastructure', stableVersion: '1.30'}),

  // ---------------------------------------------------------------------------
  // ТИП 5: Client (Клиенты и заказчики) - 5 узлов
  // ---------------------------------------------------------------------------
  (cli1:Client {regNo: 'C01', companyName: 'Альфа-Инвест', industry: 'Финансы', tier: 'Enterprise'}),
  (cli2:Client {regNo: 'C02', companyName: 'СберТехнологии', industry: 'Банкинг', tier: 'Enterprise'}),
  (cli3:Client {regNo: 'C03', companyName: 'РитейлСити', industry: 'Электронная коммерция', tier: 'Middle'}),
  (cli4:Client {regNo: 'C04', companyName: 'МедКонсалт', industry: 'Здравоохранение', tier: 'Small'}),
  // Данный клиент намеренно оставлен без активных заказов
  (cli5:Client {regNo: 'C05', companyName: 'ПромЛогистика', industry: 'Транспорт', tier: 'Middle'});
```

### Блок 3: Связывание узлов отношениями

Связи строятся через сопоставление существующих узлов командой `MATCH` с последующим вызовом `CREATE`.

  

#### Вариант 1 (без комментариев)

Cypher

```
MATCH (e:Employee), (d:Department)
WHERE e.id = 'E01' AND d.code = 'D01'
CREATE (e)-[:WORKS_IN {role: 'Lead Architect', since: 2021}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E02' AND d.code = 'D01'
CREATE (e)-[:WORKS_IN {role: 'Senior Backend Engineer', since: 2022}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E03' AND d.code = 'D03'
CREATE (e)-[:WORKS_IN {role: 'Data Engineer', since: 2023}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E04' AND d.code = 'D02'
CREATE (e)-[:WORKS_IN {role: 'Middle UI Engineer', since: 2023}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E05' AND d.code = 'D04'
CREATE (e)-[:WORKS_IN {role: 'Site Reliability Engineer', since: 2020}]->(d);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E01' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.6}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E01' AND p.code = 'P03'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.4}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E02' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 1.0}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E03' AND p.code = 'P03'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.8}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E04' AND p.code = 'P02'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 1.0}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E05' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.5}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E05' AND p.code = 'P05'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.5}]->(p);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E01' AND t.name IN ['Neo4j', 'Go', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E02' AND t.name IN ['Go', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Advanced'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E03' AND t.name IN ['Neo4j', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Advanced'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E04' AND t.name = 'TypeScript'
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E05' AND t.name = 'Kubernetes'
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

MATCH (p:Project), (t:Technology)
WHERE p.code = 'P01' AND t.name IN ['Go', 'Kubernetes']
CREATE (p)-[:BUILT_WITH]->(t);

MATCH (p:Project), (t:Technology)
WHERE p.code = 'P02' AND t.name = 'TypeScript'
CREATE (p)-[:BUILT_WITH]->(t);

MATCH (p:Project), (t:Technology)
WHERE p.code = 'P03' AND t.name IN ['Python', 'Neo4j']
CREATE (p)-[:BUILT_WITH]->(t);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C01' AND p.code = 'P01'
CREATE (c)-[:ORDERED {signedAt: 2023, contractVal: 15000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C02' AND p.code = 'P03'
CREATE (c)-[:ORDERED {signedAt: 2024, contractVal: 12000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C03' AND p.code = 'P04'
CREATE (c)-[:ORDERED {signedAt: 2024, contractVal: 6000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C04' AND p.code = 'P02'
CREATE (c)-[:ORDERED {signedAt: 2023, contractVal: 8500000}]->(p);

MATCH (e1:Employee), (e2:Employee)
WHERE e1.id = 'E01' AND e2.id = 'E02'
CREATE (e1)-[:MENTORS {started: 2023}]->(e2);
```

#### Вариант 2 (с обширными комментариями)

Cypher

```
// 1. Привязка сотрудников к департаментам через отношение :WORKS_IN
// MATCH отбирает узел сотрудника (e) и узел отдела (d) по их идентификаторам.
// Конструкция -[:WORKS_IN {...}]-> создает направленное ребро с атрибутами должности и года.
MATCH (e:Employee), (d:Department)
WHERE e.id = 'E01' AND d.code = 'D01'
CREATE (e)-[:WORKS_IN {role: 'Lead Architect', since: 2021}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E02' AND d.code = 'D01'
CREATE (e)-[:WORKS_IN {role: 'Senior Backend Engineer', since: 2022}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E03' AND d.code = 'D03'
CREATE (e)-[:WORKS_IN {role: 'Data Engineer', since: 2023}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E04' AND d.code = 'D02'
CREATE (e)-[:WORKS_IN {role: 'Middle UI Engineer', since: 2023}]->(d);

MATCH (e:Employee), (d:Department)
WHERE e.id = 'E05' AND d.code = 'D04'
CREATE (e)-[:WORKS_IN {role: 'Site Reliability Engineer', since: 2020}]->(d);

// 2. Назначение сотрудников на проекты через отношение :CONTRIBUTES_TO
// Свойство allocation задает долю занятости сотрудника на конкретном проекте (от 0.0 до 1.0).
MATCH (e:Employee), (p:Project)
WHERE e.id = 'E01' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.6}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E01' AND p.code = 'P03'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.4}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E02' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 1.0}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E03' AND p.code = 'P03'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.8}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E04' AND p.code = 'P02'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 1.0}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E05' AND p.code = 'P01'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.5}]->(p);

MATCH (e:Employee), (p:Project)
WHERE e.id = 'E05' AND p.code = 'P05'
CREATE (e)-[:CONTRIBUTES_TO {allocation: 0.5}]->(p);

// 3. Компетенции сотрудников: :HAS_SKILL
// Оператор IN позволяет сопоставить узел e сразу с несколькими узлами t, удовлетворяющими списку имен.
MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E01' AND t.name IN ['Neo4j', 'Go', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E02' AND t.name IN ['Go', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Advanced'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E03' AND t.name IN ['Neo4j', 'Python']
CREATE (e)-[:HAS_SKILL {proficiency: 'Advanced'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E04' AND t.name = 'TypeScript'
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

MATCH (e:Employee), (t:Technology)
WHERE e.id = 'E05' AND t.name = 'Kubernetes'
CREATE (e)-[:HAS_SKILL {proficiency: 'Expert'}]->(t);

// 4. Использование технологий в проектах: :BUILT_WITH
// Демонстрирует технологический стек каждого продукта.
MATCH (p:Project), (t:Technology)
WHERE p.code = 'P01' AND t.name IN ['Go', 'Kubernetes']
CREATE (p)-[:BUILT_WITH]->(t);

MATCH (p:Project), (t:Technology)
WHERE p.code = 'P02' AND t.name = 'TypeScript'
CREATE (p)-[:BUILT_WITH]->(t);

MATCH (p:Project), (t:Technology)
WHERE p.code = 'P03' AND t.name IN ['Python', 'Neo4j']
CREATE (p)-[:BUILT_WITH]->(t);

// 5. Заказы проектов клиентами: :ORDERED
// Отношение содержит финансовые атрибуты договора: год подписания и сумму контракта.
MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C01' AND p.code = 'P01'
CREATE (c)-[:ORDERED {signedAt: 2023, contractVal: 15000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C02' AND p.code = 'P03'
CREATE (c)-[:ORDERED {signedAt: 2024, contractVal: 12000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C03' AND p.code = 'P04'
CREATE (c)-[:ORDERED {signedAt: 2024, contractVal: 6000000}]->(p);

MATCH (c:Client), (p:Project)
WHERE c.regNo = 'C04' AND p.code = 'P02'
CREATE (c)-[:ORDERED {signedAt: 2023, contractVal: 8500000}]->(p);

// 6. Внутрикомандная связь между сотрудниками: :MENTORS
// Отношение направлено от наставника (e1) к подопечному (e2).
MATCH (e1:Employee), (e2:Employee)
WHERE e1.id = 'E01' AND e2.id = 'E02'
CREATE (e1)-[:MENTORS {started: 2023}]->(e2);
```

### Блок 4: Проверочные и демонстрационные запросы

Запросы для вывода графа в Neo4j Browser и проверки выполнения всех критериев задания.

  

#### Запрос 4.1: Полная визуализация всего созданного графа

Выводит все узлы и отношения на холст интерфейса.

  

##### Вариант 1 (без комментариев)

Cypher

```
MATCH (n)
OPTIONAL MATCH (n)-[r]->(m)
RETURN n, r, m;
```

##### Вариант 2 (с обширными комментариями)

Cypher

```
// MATCH (n) находит абсолютно все узлы графа
MATCH (n)
// OPTIONAL MATCH гарантирует, что если у узла нет исходящих связей (изолированные узлы),
// сам узел n все равно останется в результирующей выборке (аналог LEFT OUTER JOIN в SQL).
OPTIONAL MATCH (n)-[r]->(m)
// RETURN возвращает кортежи из узла-источника, связи и целевого узла для отрисовки в Neo4j Browser.
RETURN n, r, m;
```

#### Запрос 4.2: Подсчет узлов по типам (проверка условия: 5 типов по 5 узлов)

Группирует данные по меткам узлов и вычисляет количество.

  

##### Вариант 1 (без комментариев)

Cypher

```
MATCH (n)
RETURN labels(n)[0] AS NodeType, count(n) AS NodeCount
ORDER BY NodeType ASC;
```

##### Вариант 2 (с обширными комментариями)

Cypher

```
// MATCH (n) обращается ко всем узлам графа
MATCH (n)
// Функция labels(n) возвращает массив всех меток, назначенных узлу n.
// Индекс [0] извлекает первичную метку.
// Функция count(n) выполняет агрегирующий подсчет элементов в сформированной группе.
RETURN labels(n)[0] AS NodeType, count(n) AS NodeCount
// ORDER BY сортирует полученный список по алфавиту для наглядного отчета
ORDER BY NodeType ASC;
```

#### Запрос 4.3: Сложный аналитический запрос (цепочка «Клиент -> Проект -> Стек технологий -> Разработчики»)

Демонстрирует многоуровневый обход графа без соединений таблиц.

  

##### Вариант 1 (без комментариев)

Cypher

```
MATCH (c:Client)-[:ORDERED]->(p:Project)-[:BUILT_WITH]->(t:Technology)<-[:HAS_SKILL]-(e:Employee)
RETURN c.companyName AS Client,
       p.title AS Project,
       t.name AS StackTechnology,
       collect(DISTINCT e.name) AS QualifiedEngineers;
```

##### Вариант 2 (с обширными комментариями)

Cypher

```
// Выполняется сквозной поиск пути по графу по непрерывному графовому паттерну:
// 1. Клиент заказал проект: (c)-[:ORDERED]->(p)
// 2. Проект использует технологию: (p)-[:BUILT_WITH]->(t)
// 3. Та же технология освоена сотрудником: (t)<-[:HAS_SKILL]-(e) (обратное направление связи)
MATCH (c:Client)-[:ORDERED]->(p:Project)-[:BUILT_WITH]->(t:Technology)<-[:HAS_SKILL]-(e:Employee)
// RETURN проецирует данные в табличный вид
RETURN c.companyName AS Client,
       p.title AS Project,
       t.name AS StackTechnology,
       // Функция collect(DISTINCT ...) сворачивает список подходящих инженеров в массив без дубликатов
       collect(DISTINCT e.name) AS QualifiedEngineers;
```

## 3. Ответы на контрольные вопросы

### 1. Что представляет собой графовая база данных?

Графовая база данных — это специализированная NoSQL СУБД, использующая структуры графов (вершины, ребра и свойства) для семантического представления и хранения данных. В ней отношения между сущностями хранятся как фундаментальный тип данных первого уровня, что устраняет необходимость вычисления связей во время выполнения запроса.

  

### 2. Из чего состоит графовая база данных?

Графовая база данных (в модели графа свойств) состоит из:

  

- **Узлов (Nodes / Vertices)** — сущностей предметной области.
    
      
    
- **Меток (Labels)** — дескрипторов классификации и типизации узлов.
    
      
    
- **Отношений / Связей (Relationships / Edges)** — направленных типизированных линий связи между двумя узлами.
    
      
    
- **Свойств (Properties)** — скалярных пар «ключ-значение», привязанных к узлам или ребрам.
    
      
    

### 3. Ключевые отличия графовых баз данных от реляционных?

- **Модель хранения связей**: В реляционных базах (RDBMS) связи выражаются через внешние ключи (`FOREIGN KEY`) и материализуются «на лету» через операцию `JOIN`. В графовых СУБД ребра хранятся физически в виде прямых указателей на соседние узлы (безындексная смежность).
    
      
    
- **Производительность сложных запросов**: Сложность глубокого обхода графа в RDBMS растет экспоненциально из-за многократных `JOIN`-ов на всей таблице ($O(N)$), а в графовых СУБД зависит линейно лишь от количества локальных связей ($O(k)$).
    
      
    
- **Схема данных**: RDBMS требуют жесткой предварительной схемы (`schema-first`), тогда как графовые СУБД гибки или вовсе бессхемны (`schema-optional`).
    
      
    

### 4. Чем отличаются графовые модели Property Graph и RDF?

- **Property Graph (Граф свойств)**: Узлы и ребра содержат произвольный набор атрибутов (свойств в виде ключ-значение), ребра всегда направлены и имеют один тип. Модель оптимизирована для операционных транзакций (OLTP), высокоскоростного обхода и графовых алгоритмов (язык Cypher).
    
      
    
- **RDF (Resource Description Framework)**: Семантическая модель консорциума W3C, представляющая знания в виде триплетов: «Субъект — Предикат — Объект» (например, `<Алексей> <работаетВ> <ИТ>`). В RDF связь не может содержать внутренних свойств без механизма реификации. Оптимизирована для обмена метаданными в вебе, онтологий и логического вывода (язык SPARQL).
    
      
    

### 5. Какие области применения графовых баз данных?

- Социальные сети и сети связей сотрудников.
    
      
    
- Системы обнаружения мошенничества и антифрод-мониторинг (поиск циклических транзакций и синтетических личностей).
    
      
    
- Рекомендательные системы в реальном времени.
    
      
    
- Управление мастер-данными (Master Data Management, MDM) и каталогизация активов.
    
      
    
- Графы знаний (Knowledge Graphs) и семантический поиск.
    
      
    
- Трассировка зависимостей в IT-инфраструктуре и микросервисах (CMDB).
    
      
    
- Логистика, маршрутизация и оптимизация путей поставки.
    
      
    

### 6. Какие примеры использования графовых баз данных есть?

- **Банки (Citi, UBS)**: анализ финансовых цепочек между счетами и контрагентами для выявления отмывания денег (AML).
    
      
    
- **LinkedIn**: расчет социальной дистанции («знакомые 1-го, 2-го, 3-го круга») и ранжирование релевантных вакансий.
    
      
    
- **NASA**: агрегация технической документации и уроков прошлых миссий в единый Граф Знаний для инженеров.
    
      
    
- **eBay / Walmart**: расчет взаимодополняющих товаров для корзины на лету на основе предпочтений похожих покупателей.
    
      
    

### 7. Что представляет собой база JanusGraph?

JanusGraph — масштабируемая распределенная графовая СУБД с открытым исходным кодом под управлением Linux Foundation. Архитектурно она отделяет вычислительный уровень от слоя хранения: данные могут размещаться в Apache Cassandra, Apache HBase, Google Cloud Bigtable или ScyllaDB, а индексация выполняется через Elasticsearch, Solr или Lucene. Поддерживает стек Apache TinkerPop и язык запросов Gremlin.

  

### 8. Что представляет собой база Dgraph?

Dgraph — нативная горизонтально масштабируемая графовая СУБД с открытым исходным кодом, написанная на языке Go. Оптимизирована для высокой производительности и масштабирования за счет разделения графа на партиции (шардинг). Использует протокол распределенного консенсуса Raft и поддерживает GraphQL в качестве основного интерфейса запросов (а также собственный диалект DQL).

  

### 9. Что представляет собой база DataStax Enterprise Graph?

DataStax Enterprise Graph (DSE Graph) — коммерческая распределенная графовая СУБД реального времени, построенная поверх ядра Apache Cassandra. Она спроектирована для работы в режиме непрерывной доступности 24/7 без единой точки отказа, масштабируется на сотни узлов в мультидатацентровой конфигурации и использует язык Gremlin (Apache TinkerPop).

  

### 10. Что представляет собой Neo4j?

Neo4j — транзакционная графовая СУБД с открытым исходным кодом (Community Edition) и коммерческой платформой (Enterprise / AuraDB), реализующая нативную модель графа свойств (LPG). В отличие от многослойных решений, Neo4j спроектирована с нулевого уровня для графового хранения: структура узлов и ребер отображается непосредственно в специализированные дисковые страницы памяти, обеспечивая сверхбыстрый обход связей.

  

### 11. Преимущества Neo4j

- Нативное графовое хранилище и движок обработки (Native Graph Processing).
    
      
    
- Полное соответствие требованиям ACID (Atomicity, Consistency, Isolation, Durability) для обеспечения целостности данных.
    
      
    
- Интуитивно понятный, лаконичный декларативный язык Cypher.
    
      
    
- Развитая инфраструктура разработчика: интерактивный Neo4j Browser, инструмент визуального исследования Neo4j Bloom, библиотека графовых алгоритмов Graph Data Science (GDS).
    
      
    
- Официальные драйверы высокой производительности для Java, Python, JavaScript, Go, .NET.
    
      
    

### 12. Элементы Neo4j

Ключевые конструктивные элементы базы данных:

  

- **Node (Узел)** — базовый объект хранения данных.
    
      
    
- **Relationship (Связь)** — направленное соединение между начальным и конечным узлом.
    
      
    
- **Label (Метка)** — классификатор узла, служащий также точкой построения индексов.
    
      
    
- **Relationship Type (Тип связи)** — обязательное имя-идентификатор связи.
    
      
    
- **Property (Свойство)** — типизированный атрибут (строка, число, дата, булево значение, массив) узла или связи.
    
      
    
- **Index (Индекс)** — вспомогательная B-Tree / Text структура для моментального нахождения стартовых узлов обхода.
    
      
    

### 13. Какие команды чтения Neo4j CQL есть?

- `MATCH` — базовый шаблон поиска узлов, путей и отношений.
    
      
    
- `OPTIONAL MATCH` — аналог внешнего соединения: сохраняет результаты, если шаблон не найден.
    
      
    
- `WHERE` — предикатная фильтрация результатов `MATCH`.
    
      
    
- `RETURN` — указание полей, узлов и агрегатов, возвращаемых клиенту.
    
      
    
- `WITH` — передача промежуточных результатов конвейера следующей части запроса.
    
      
    
- `ORDER BY` — сортировка итогового набора данных по возрастанию (`ASC`) или убыванию (`DESC`).
    
      
    
- `SKIP` / `LIMIT` — пагинация (пропуск $k$ строк и ограничение выборки).
    
      
    
- `UNWIND` — разворачивание массива в строки.
    
      
    

### 14. Какие команды записи Neo4j CQL есть?

- `CREATE` — безусловное создание узлов и связей.
    
      
    
- `MERGE` — идемпотентное создание: если элемент найден по шаблону — выполняется ветка `ON MATCH`, если отсутствует — ветка `ON CREATE`.
    
      
    
- `SET` — установка значений свойств и добавление меток к узлам.
    
      
    
- `REMOVE` — удаление свойства у узла/связи или снятие метки с узла.
    
      
    
- `DELETE` — удаление узлов или связей.
    
      
    
- `DETACH DELETE` — удаление узлов с предварительным каскадным удалением всех инцидентных им связей.
    
      
    

### 15. Какие общие команды Neo4j CQL есть?

- `CALL` — вызов встроенных или пользовательских хранимых процедур и функций (включая плагины APOC и GDS).
    
      
    
- `UNION` / `UNION ALL` — объединение результатов нескольких подзапросов с дедупликацией или без нее.
    
      
    
- `EXPLAIN` — просмотр скомпилированного плана выполнения запроса без его физического запуска.
    
      
    
- `PROFILE` — выполнение запроса с профилированием фактического времени, числа обращений к хранилищу (DB Hits) и строк.
    
      
    
- `USE` — переключение контекста исполнения на конкретную базу данных в кластере.
    
      
    

### 16. Какие функции Neo4j CQL есть?

- **Агрегатные**: `count()`, `sum()`, `avg()`, `min()`, `max()`, `collect()` (сборка значений в список).
    
      
    
- **Предикатные**: `all()`, `any()`, `none()`, `single()`, `exists()`.
    
      
    
- **Скалярные и структурные**: `id()`, `elementId()`, `labels()`, `type()`, `keys()`, `properties()`, `size()`, `coalesce()`.
    
      
    
- **Функции списков и путей**: `nodes(path)`, `relationships(path)`, `length(path)`, `head()`, `last()`, `tail()`.
    
      
    
- **Математические и строковые**: `round()`, `sqrt()`, `toInteger()`, `toFloat()`, `toUpper()`, `toLower()`, `trim()`, `split()`, `substring()`.
    
      
    

### 17. Плюсы и минусы графовых баз данных

|**Параметр**|**Достоинства (Плюсы)**|**Недостатки (Минусы)**|
|---|---|---|
|**Производительность**|Скорость обхода сложных цепочек связей не деградирует при увеличении общего объема данных в БД.|Неэффективны для типовых аналитических OLAP-запросов (сканирование миллиардов несвязанных записей с агрегацией по столбцам).|
|**Гибкость модели**|Естественное моделирование сетевых структур реального мира; легкая модификация схемы без блокировок.|Отсутствие жестких ограничений схемы на уровне движка может привести к засорению графа некорректными свойствами.|
|**Запросы**|Декларативный синтаксис (Cypher), устраняющий многоэтажные конструкции из десятков `JOIN`.|Языки запросов менее стандартизированы, чем классический SQL (хотя внедряется международный стандарт GQL).|
|**Масштабирование**|Вертикальное масштабирование дает максимальную производительность благодаря удержанию графа в RAM.|Сложность горизонтального шардинга: разделение плотно связанного графа между серверами приводит к дорогостоящему сетевому взаимодействию.|