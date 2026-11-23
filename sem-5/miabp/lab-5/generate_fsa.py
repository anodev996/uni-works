import openpyxl
from openpyxl.styles import Font, PatternFill, Alignment, Border, Side
from openpyxl.chart import BarChart, Reference
from openpyxl.utils import get_column_letter


def build_fsa_workbook(filename: str = "Lab5_FSA_Remont_Kvartiry.xlsx") -> None:
    wb = openpyxl.Workbook()
    ws = wb.active
    ws.title = "ФСА_Ремонт_квартиры"
    ws.views.sheetView[0].showGridLines = True

    # Стили оформления
    font_title = Font(name="Calibri", size=14, bold=True, color="1F4E78")
    font_sec = Font(name="Calibri", size=11, bold=True, color="1F4E78")
    font_hdr = Font(name="Calibri", size=10, bold=True, color="FFFFFF")
    font_a0 = Font(name="Calibri", size=10, bold=True, color="000000")
    font_parent = Font(name="Calibri", size=10, bold=True, color="1F4E78")
    font_leaf = Font(name="Calibri", size=10, bold=False, color="000000")

    fill_hdr = PatternFill("solid", fgColor="2F5597")
    fill_subhdr = PatternFill("solid", fgColor="4472C4")
    fill_a0 = PatternFill("solid", fgColor="D9E1F2")
    fill_parent = PatternFill("solid", fgColor="E9EDF4")
    fill_leaf = PatternFill("solid", fgColor="FFFFFF")
    fill_rate_hdr = PatternFill("solid", fgColor="548235")

    thin = Side(border_style="thin", color="BFBFBF")
    med = Side(border_style="medium", color="2F5597")
    border_cell = Border(left=thin, right=thin, top=thin, bottom=thin)
    border_parent = Border(left=thin, right=thin, top=med, bottom=thin)

    align_left = Alignment(horizontal="left", vertical="center", wrap_text=True)
    align_center = Alignment(horizontal="center", vertical="center", wrap_text=True)
    align_right = Alignment(horizontal="right", vertical="center")

    num_fmt_money = "#,##0.00"
    num_fmt_time = "0.0"
    num_fmt_int = "0"
    num_fmt_pct = "0.0%"

    # Заголовок документа (строка 1)
    ws.merge_cells("A1:M1")
    ws["A1"] = 'Функционально-стоимостной анализ процесса «Ремонт квартиры» (Вариант 8)'
    ws["A1"].font = font_title
    ws["A1"].alignment = Alignment(horizontal="left", vertical="center")
    ws.row_dimensions[1].height = 28

    # Заголовки разделов (строка 3)
    ws["A3"] = "1. Расчёт стоимости функций по центрам затрат"
    ws["A3"].font = font_sec
    ws["K3"] = "2. Часовые ставки ресурсов"
    ws["K3"].font = font_sec
    ws.row_dimensions[3].height = 22

    # --- СПРАВОЧНИК СТАВОК (K4:M16) ---
    rate_headers = ["Ресурс / Статья расходов", "Ед. изм.", "Ставка, руб."]
    for idx, text in enumerate(rate_headers, start=11):
        cell = ws.cell(row=4, column=idx, value=text)
        cell.font = font_hdr
        cell.fill = fill_rate_hdr
        cell.alignment = align_center
        cell.border = border_cell

    rates = [
        ("Консультант по работе с клиентами", "руб./час", 500.0),    # M5
        ("Мастер", "руб./час", 800.0),                              # M6
        ("Дизайнер", "руб./час", 900.0),                            # M7
        ("Специалист по закупкам", "руб./час", 600.0),              # M8
        ("Бригада строительных рабочих (3 чел.)", "руб./час", 1800.0),  # M9
        ("Компьютер, оргтехника и ПО", "руб./час", 50.0),           # M10
        ("Измерительные инструменты", "руб./час", 80.0),            # M11
        ("Строительный инструмент и оборудование", "руб./час", 150.0),  # M12
        ("Грузовой транспорт / склад. техника", "руб./час", 300.0), # M13
        ("Офис (аренда и коммунальные услуги)", "руб./час", 100.0), # M14
        ("Складское помещение", "руб./час", 80.0),                  # M15
        ("Квартира клиента (выезд на объект)", "руб./час", 0.0),    # M16
    ]
    for offset, (name, unit, val) in enumerate(rates):
        r = 5 + offset
        c1 = ws.cell(row=r, column=11, value=name)
        c2 = ws.cell(row=r, column=12, value=unit)
        c3 = ws.cell(row=r, column=13, value=val)
        c1.font, c2.font, c3.font = font_leaf, font_leaf, font_leaf
        c1.alignment, c2.alignment, c3.alignment = align_left, align_center, align_right
        c3.number_format = num_fmt_money
        for c in (c1, c2, c3):
            c.border = border_cell

    # --- ШАПКА ОСНОВНОЙ ТАБЛИЦЫ (строки 4-5) ---
    ws.merge_cells("A4:A5")
    ws.merge_cells("B4:B5")
    ws.merge_cells("C4:C5")
    ws.merge_cells("D4:D5")
    ws.merge_cells("E4:I4")

    ws["A4"] = "Функции"
    ws["B4"] = "Повторения"
    ws["C4"] = "Время, час"
    ws["D4"] = "Стоимость функции, руб."
    ws["E4"] = "Центры затрат, руб."

    cost_centers = ["Зар. плата", "Оборудование", "Помещение", "Материалы", "Управление"]
    for idx, title in enumerate(cost_centers, start=5):
        ws.cell(row=5, column=idx, value=title)

    for r in (4, 5):
        ws.row_dimensions[r].height = 22
        for col in range(1, 10):
            cell = ws.cell(row=r, column=col)
            cell.font = font_hdr
            cell.fill = fill_hdr if r == 4 else fill_subhdr
            cell.alignment = align_center
            cell.border = border_cell

    # --- ДАННЫЕ ФУНКЦИЙ (строки 6..26) ---
    # Формат: (row, level, name, reps, time_h, sal_f, eq_f, room_f, mat_v, mgmt_v, children_rows)
    rows_spec = [
        (6, "A0", "А0 Организация и выполнение ремонта квартиры", None, None, None, None, None, None, None, [7, 12, 17, 22]),

        (7, "P", "А1 Приём заявки и обследование квартиры", 1, None, None, None, None, None, None, [8, 9, 10, 11]),
        (8, "L", "    А11 Консультирование клиента и приём заявки", 1, 0.5, "=C8*$M$5", "=C8*$M$10", "=C8*$M$14", 20.0, 30.0, None),
        (9, "L", "    А12 Согласование даты осмотра", 1, 0.5, "=C9*$M$5", "=C9*$M$10", "=C9*$M$14", 0.0, 50.0, None),
        (10, "L", "    А13 Осмотр квартиры и проведение замеров", 1, 2.0, "=C10*($M$6+$M$7)", "=C10*$M$11", "=C10*$M$16", 50.0, 100.0, None),
        (11, "L", "    А14 Определение и фиксация требований клиента", 1, 1.0, "=C11*($M$6+$M$7)", "=C11*$M$10", "=C11*$M$14", 50.0, 100.0, None),

        (12, "P", "А2 Разработка и согласование проекта", 1, None, None, None, None, None, None, [13, 14, 15, 16]),
        (13, "L", "    А21 Составление сметы", 1, 3.0, "=C13*$M$6", "=C13*$M$10", "=C13*$M$14", 50.0, 100.0, None),
        (14, "L", "    А22 Заключение договора", 1, 1.0, "=C14*$M$6", "=C14*$M$10", "=C14*$M$14", 100.0, 150.0, None),
        (15, "L", "    А23 Разработка дизайн-проекта", 1, 16.0, "=C15*$M$7", "=C15*$M$10", "=C15*$M$14", 500.0, 300.0, None),
        (16, "L", "    А24 Согласование дизайн-проекта", 1, 2.0, "=C16*($M$6+$M$7)", "=C16*$M$10", "=C16*$M$14", 100.0, 200.0, None),

        (17, "P", "А3 Планирование работ и закупка материалов", 1, None, None, None, None, None, None, [18, 19, 20, 21]),
        (18, "L", "    А31 Составление плана работ", 1, 2.0, "=C18*$M$6", "=C18*$M$10", "=C18*$M$14", 50.0, 250.0, None),
        (19, "L", "    А32 Определение материалов", 1, 2.0, "=C19*$M$6", "=C19*$M$10", "=C19*$M$14", 50.0, 100.0, None),
        (20, "L", "    А33 Оформление счёта", 1, 1.0, "=C20*$M$8", "=C20*$M$10", "=C20*$M$14", 30.0, 100.0, None),
        (21, "L", "    А34 Закупка и доставка", 1, 4.0, "=C21*$M$8", "=C21*$M$13", "=C21*$M$15", 85000.0, 300.0, None),

        (22, "P", "А4 Выполнение и приёмка ремонта", 1, None, None, None, None, None, None, [23, 24, 25, 26]),
        (23, "L", "    А41 Подготовка помещения к ремонту", 1, 8.0, "=C23*$M$9", "=C23*$M$12", "=C23*$M$16", 1500.0, 200.0, None),
        (24, "L", "    А42 Выполнение ремонтных работ (4 этапа)", 4, 20.0, "=C24*$M$9", "=C24*$M$12", "=C24*$M$16", 1000.0, 400.0, None),
        (25, "L", "    А43 Контроль качества и исправление дефектов", 4, 2.0, "=C25*$M$6", "=C25*$M$11", "=C25*$M$16", 250.0, 150.0, None),
        (26, "L", "    А44 Окончательная приёмка ремонта", 1, 2.0, "=C26*$M$6", "=C26*$M$10", "=C26*$M$16", 100.0, 300.0, None),
    ]

    for r, level, name, reps, time_h, sal_f, eq_f, room_f, mat_v, mgmt_v, children in rows_spec:
        ws.row_dimensions[r].height = 20
        ws.cell(row=r, column=1, value=name)
        if reps is not None:
            ws.cell(row=r, column=2, value=reps)

        if level == "L":
            ws.cell(row=r, column=3, value=time_h)
            ws.cell(row=r, column=4, value=f"=SUM(E{r}:I{r})")
            ws.cell(row=r, column=5, value=sal_f)
            ws.cell(row=r, column=6, value=eq_f)
            ws.cell(row=r, column=7, value=room_f)
            ws.cell(row=r, column=8, value=mat_v)
            ws.cell(row=r, column=9, value=mgmt_v)
        else:
            # Агрегация дочерних функций с учётом столбца $B (Повторения)
            ws.cell(row=r, column=3, value="=" + "+".join(f"C{ch}*$B{ch}" for ch in children))
            ws.cell(row=r, column=4, value=f"=SUM(E{r}:I{r})")
            for col_idx, col_let in enumerate(["E", "F", "G", "H", "I"], start=5):
                ws.cell(row=r, column=col_idx, value="=" + "+".join(f"{col_let}{ch}*$B{ch}" for ch in children))

        row_font = font_a0 if level == "A0" else (font_parent if level == "P" else font_leaf)
        row_fill = fill_a0 if level == "A0" else (fill_parent if level == "P" else fill_leaf)
        row_border = border_parent if level in ("A0", "P") else border_cell

        for col in range(1, 10):
            cell = ws.cell(row=r, column=col)
            cell.font = row_font
            cell.fill = row_fill
            cell.border = row_border
            if col == 1:
                cell.alignment = align_left
            elif col == 2:
                cell.alignment = align_center
                cell.number_format = num_fmt_int
            elif col == 3:
                cell.alignment = align_right
                cell.number_format = num_fmt_time
            else:
                cell.alignment = align_right
                cell.number_format = num_fmt_money

    # --- СВОДНАЯ ТАБЛИЦА ДЛЯ ГИСТОГРАММЫ (строки 29..34) ---
    ws["A28"] = "3. Сводная таблица затрат по основным блокам для построения гистограммы"
    ws["A28"].font = font_sec
    ws.row_dimensions[28].height = 22

    sum_headers = ["Блок", "Зар. плата", "Оборудование", "Помещение", "Материалы", "Управление", "Итого, руб.", "Доля в А0"]
    for idx, h in enumerate(sum_headers, start=1):
        c = ws.cell(row=29, column=idx, value=h)
        c.font = font_hdr
        c.fill = fill_hdr
        c.alignment = align_center
        c.border = border_cell

    summary_links = [
        ("A0", 6),
        ("A1", 7),
        ("A2", 12),
        ("A3", 17),
        ("A4", 22),
    ]
    for idx, (code, src_row) in enumerate(summary_links):
        r = 30 + idx
        ws.row_dimensions[r].height = 20
        ws.cell(row=r, column=1, value=code)
        for col_offset, src_col in enumerate(["E", "F", "G", "H", "I"], start=2):
            ws.cell(row=r, column=col_offset, value=f"={src_col}{src_row}")
        ws.cell(row=r, column=7, value=f"=SUM(B{r}:F{r})")
        ws.cell(row=r, column=8, value=f"=G{r}/$G$30")

        is_total = code == "A0"
        for col in range(1, 9):
            c = ws.cell(row=r, column=col)
            c.font = font_a0 if is_total else font_leaf
            c.fill = fill_a0 if is_total else fill_leaf
            c.border = border_cell
            if col == 1:
                c.alignment = align_center
            elif col == 8:
                c.alignment = align_right
                c.number_format = num_fmt_pct
            else:
                c.alignment = align_right
                c.number_format = num_fmt_money

    # --- ГИСТОГРАММА С НАКОПЛЕНИЕМ ---
    chart = BarChart()
    chart.type = "col"
    chart.style = 10
    chart.grouping = "stacked"
    chart.overlap = 100
    chart.title = "Структура затрат по центрам стоимости (ФСА процесса «Ремонт квартиры»)"
    chart.y_axis.title = "Затраты, руб."
    chart.x_axis.title = "Функциональные блоки IDEF0"
    chart.width = 20
    chart.height = 11

    data_ref = Reference(ws, min_col=2, min_row=29, max_col=6, max_row=34)
    cats_ref = Reference(ws, min_col=1, min_row=30, max_row=34)
    chart.add_data(data_ref, titles_from_data=True)
    chart.set_categories(cats_ref)
    ws.add_chart(chart, "A36")

    # Настройка ширины столбцов
    col_widths = {
        1: 48, 2: 13, 3: 13, 4: 22, 5: 15, 6: 15, 7: 14, 8: 15, 9: 15,
        10: 3, 11: 38, 12: 11, 13: 15
    }
    for col_idx, width in col_widths.items():
        ws.column_dimensions[get_column_letter(col_idx)].width = width

    wb.save(filename)
    print(f"Файл успешно создан: {filename}")


if __name__ == "__main__":
    build_fsa_workbook()