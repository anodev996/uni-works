import javafx.animation.KeyFrame;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.layout.*;
import javafx.scene.shape.Line;
import javafx.scene.shape.Circle;
import javafx.scene.shape.Polyline;
import javafx.scene.paint.Color;
import javafx.scene.text.Text;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.stage.Stage;
import javafx.util.Duration;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;

public class BisectionMethodVisualizer extends Application {
    
    // Компоненты интерфейса
    private ComboBox<String> functionComboBox;
    private TextField aField, bField, epsilonField;
    private Button startButton, pauseButton, prevButton, nextButton, resetButton;
    private TextArea outputArea;
    private Label currentIterationLabel;
    private Label resultLabel;
    private Label checkLabel; // Новая метка для проверки
    private Pane graphPane;
    
    // Алгоритмические переменные
    private Timeline timeline;
    private List<IterationStep> steps;
    private int currentStepIndex;
    private boolean isPaused;
    
    // Графические параметры
    private double graphWidth = 600;
    private double graphHeight = 400;
    private double padding = 50;
    
    // Определение функций
    @SuppressWarnings("unchecked")
    private final Function<Double, Double>[] functions = new Function[]{
        (Function<Double, Double>) x -> x * x * x - 9 * x * x + 27 * x - 28,
        (Function<Double, Double>) x -> x * x + 2 * x - 3,
        (Function<Double, Double>) x -> Math.exp(x) - 2,
        (Function<Double, Double>) x -> Math.sin(x) - 0.5
    };
    
    private final String[] functionNames = {
        "f(x) = x³ - 9x² + 27x - 28",
        "f(x) = x² + 2x - 3", 
        "f(x) = eˣ - 2",
        "f(x) = sin(x) - 0.5"
    };

    @Override
    public void start(Stage primaryStage) {
        initializeComponents();
        setupLayout(primaryStage);
        setupEventHandlers();
        plotFunction(); // Построить график при запуске
    }

    private void initializeComponents() {
        functionComboBox = new ComboBox<>();
        functionComboBox.getItems().addAll(functionNames);
        functionComboBox.setValue(functionNames[0]);
        
        aField = new TextField("0");
        bField = new TextField("5");
        epsilonField = new TextField("0.001");
        
        startButton = new Button("Запуск");
        pauseButton = new Button("Пауза");
        prevButton = new Button("← Предыдущая");
        nextButton = new Button("Следующая →");
        resetButton = new Button("Заново");
        
        pauseButton.setDisable(true);
        prevButton.setDisable(true);
        nextButton.setDisable(true);
        
        outputArea = new TextArea();
        outputArea.setEditable(false);
        outputArea.setPrefHeight(180);
        outputArea.setStyle("-fx-font-family: 'Monospaced';");
        
        currentIterationLabel = new Label("Итерация: -/-");
        resultLabel = new Label("Корень: не вычислен");
        resultLabel.setStyle("-fx-font-weight: bold; -fx-font-size: 14;");
        
        // Новая метка для проверки
        checkLabel = new Label("Проверка: не вычислено");
        checkLabel.setStyle("-fx-font-weight: bold; -fx-font-size: 14;");
        
        // Инициализация панели для графика
        graphPane = new Pane();
        graphPane.setPrefSize(graphWidth, graphHeight);
        graphPane.setStyle("-fx-background-color: white; -fx-border-color: #ccc; -fx-border-width: 1;");
        
        steps = new ArrayList<>();
        currentStepIndex = -1;
        isPaused = false;
    }

    private void setupLayout(Stage primaryStage) {
        // Панель выбора функции и параметров
        GridPane inputGrid = new GridPane();
        inputGrid.setHgap(10);
        inputGrid.setVgap(10);
        inputGrid.setPadding(new Insets(10));
        
        inputGrid.add(new Label("Функция:"), 0, 0);
        inputGrid.add(functionComboBox, 1, 0);
        inputGrid.add(new Label("a:"), 0, 1);
        inputGrid.add(aField, 1, 1);
        inputGrid.add(new Label("b:"), 0, 2);
        inputGrid.add(bField, 1, 2);
        inputGrid.add(new Label("Точность ε:"), 0, 3);
        inputGrid.add(epsilonField, 1, 3);

        inputGrid.setAlignment(Pos.CENTER);
        
        // Кнопки управления
        HBox controlButtons = new HBox(10, startButton, pauseButton, resetButton);
        controlButtons.setAlignment(Pos.CENTER);
        
        HBox navigationButtons = new HBox(10, prevButton, nextButton);
        navigationButtons.setAlignment(Pos.CENTER);
        
        // Добавляем checkLabel в панель кнопок
        VBox buttonPanel = new VBox(10, controlButtons, navigationButtons, currentIterationLabel, resultLabel, checkLabel);
        buttonPanel.setAlignment(Pos.CENTER);
        buttonPanel.setPadding(new Insets(10));
        
        // Объединяем панель ввода и кнопки в левую колонку
        VBox leftColumn = new VBox(10);
        leftColumn.setPadding(new Insets(10));
        leftColumn.getChildren().addAll(
            createCenteredLabel("Метод половинного деления"),
            createCenteredLabel("Ввод данных"),
            inputGrid,
            buttonPanel
        );
        
        // Панель графика с заголовком (правая колонка)
        VBox graphContainer = new VBox(5);
        graphContainer.setPadding(new Insets(10));
        graphContainer.getChildren().addAll(
            new Label("График функции:"),
            graphPane
        );
        
        // Панель вывода процесса вычислений (нижняя часть)
        VBox outputContainer = new VBox(5);
        outputContainer.setPadding(new Insets(10));
        outputContainer.getChildren().addAll(
            new Label("Процесс вычислений:"),
            outputArea
        );
        
        // Верхняя часть: левая колонка (ввод+кнопки) и правая колонка (график)
        HBox topSection = new HBox(20);
        topSection.setPadding(new Insets(10));
        topSection.setAlignment(Pos.CENTER);
        topSection.getChildren().addAll(leftColumn, graphContainer);
        
        // Основной layout
        VBox root = new VBox(10);
        root.setPadding(new Insets(10));
        root.setAlignment(Pos.TOP_CENTER);
        root.getChildren().addAll(
            topSection,
            outputContainer
        );
        
        Scene scene = new Scene(root, 1244, 700);
        primaryStage.setTitle("BisectionMethodVisualizer");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    private void setupEventHandlers() {
        functionComboBox.setOnAction(e -> plotFunction());
        aField.setOnAction(e -> plotFunction());
        bField.setOnAction(e -> plotFunction());
        
        startButton.setOnAction(e -> startAlgorithm());
        pauseButton.setOnAction(e -> togglePause());
        prevButton.setOnAction(e -> showPreviousStep());
        nextButton.setOnAction(e -> showNextStep());
        resetButton.setOnAction(e -> resetAlgorithm());
        
        timeline = new Timeline(new KeyFrame(Duration.seconds(1), e -> {
            if (!isPaused && currentStepIndex < steps.size() - 1) {
                showNextStep();
            } else if (currentStepIndex >= steps.size() - 1) {
                timeline.stop();
                pauseButton.setDisable(true);
            }
        }));
        timeline.setCycleCount(Timeline.INDEFINITE);
    }

    private void resetAlgorithm() {
        // Останавливаем таймер
        if (timeline != null) {
            timeline.stop();
        }
        
        // Сбрасываем переменные алгоритма
        steps.clear();
        currentStepIndex = -1;
        isPaused = false;
        
        // Включаем/выключаем кнопки
        startButton.setDisable(false);
        pauseButton.setDisable(true);
        prevButton.setDisable(true);
        nextButton.setDisable(true);
        
        // Сбрасываем текстовые поля
        outputArea.clear();
        currentIterationLabel.setText("Итерация: -/-");
        resultLabel.setText("Корень: не вычислен");
        checkLabel.setText("Проверка: не вычислено");
        
        // Перерисовываем график (только функцию, без элементов алгоритма)
        plotFunction();
    }

    private void plotFunction() {
        try {
            double a = Double.parseDouble(aField.getText());
            double b = Double.parseDouble(bField.getText());
            int functionIndex = functionComboBox.getSelectionModel().getSelectedIndex();
            Function<Double, Double> function = functions[functionIndex];
            
            // Очищаем предыдущие данные
            graphPane.getChildren().clear();
            
            // Вычисляем диапазон значений функции
            double minY = Double.MAX_VALUE;
            double maxY = -Double.MAX_VALUE;
            int points = 200;
            
            for (int i = 0; i <= points; i++) {
                double x = a + i * (b - a) / points;
                double y = function.apply(x);
                minY = Math.min(minY, y);
                maxY = Math.max(maxY, y);
            }
            
            // Добавляем немного отступа по Y
            double yRange = maxY - minY;
            minY -= yRange * 0.1;
            maxY += yRange * 0.1;
            
            // Рисуем оси координат с подписями
            drawAxesWithLabels(a, b, minY, maxY);
            
            // Рисуем график функции
            drawFunction(function, a, b, minY, maxY);
            
        } catch (NumberFormatException ex) {
            // Игнорируем ошибки при начальной отрисовке
        }
    }

    private void drawAxesWithLabels(double minX, double maxX, double minY, double maxY) {
        // Преобразование координат
        double xToPixel = (graphWidth - 2 * padding) / (maxX - minX);
        double yToPixel = (graphHeight - 2 * padding) / (maxY - minY);
        
        // Ось X
        double zeroY = graphHeight - padding - (-minY) * yToPixel;
        if (zeroY >= padding && zeroY <= graphHeight - padding) {
            Line xAxis = new Line(padding, zeroY, graphWidth - padding, zeroY);
            xAxis.setStroke(Color.GRAY);
            xAxis.setStrokeWidth(1);
            graphPane.getChildren().add(xAxis);
            
            // Подписи на оси X
            int xLabels = 10;
            for (int i = 0; i <= xLabels; i++) {
                double xValue = minX + i * (maxX - minX) / xLabels;
                double xPixel = padding + (xValue - minX) * xToPixel;
                
                // Метка на оси
                Line tick = new Line(xPixel, zeroY - 5, xPixel, zeroY + 5);
                tick.setStroke(Color.GRAY);
                graphPane.getChildren().add(tick);
                
                // Текст подписи
                Text label = new Text(String.format("%.1f", xValue));
                label.setX(xPixel - 10);
                label.setY(zeroY + 20);
                graphPane.getChildren().add(label);
            }
            
            // Подпись оси X
            Text xAxisLabel = new Text("x");
            xAxisLabel.setX(graphWidth - padding + 5);
            xAxisLabel.setY(zeroY);
            graphPane.getChildren().add(xAxisLabel);
        }
        
        // Ось Y
        double zeroX = padding + (-minX) * xToPixel;
        if (zeroX >= padding && zeroX <= graphWidth - padding) {
            Line yAxis = new Line(zeroX, padding, zeroX, graphHeight - padding);
            yAxis.setStroke(Color.GRAY);
            yAxis.setStrokeWidth(1);
            graphPane.getChildren().add(yAxis);
            
            // Подписи на оси Y
            int yLabels = 8;
            for (int i = 0; i <= yLabels; i++) {
                double yValue = minY + i * (maxY - minY) / yLabels;
                double yPixel = graphHeight - padding - (yValue - minY) * yToPixel;
                
                // Метка на оси
                Line tick = new Line(zeroX - 5, yPixel, zeroX + 5, yPixel);
                tick.setStroke(Color.GRAY);
                graphPane.getChildren().add(tick);
                
                // Текст подписи
                Text label = new Text(String.format("%.1f", yValue));
                label.setX(zeroX - 30);
                label.setY(yPixel + 5);
                graphPane.getChildren().add(label);
            }
            
            // Подпись оси Y
            Text yAxisLabel = new Text("f(x)");
            yAxisLabel.setX(zeroX - 15);
            yAxisLabel.setY(padding - 10);
            graphPane.getChildren().add(yAxisLabel);
        }
    }

    private void drawFunction(Function<Double, Double> function, double a, double b, double minY, double maxY) {
        double xToPixel = (graphWidth - 2 * padding) / (b - a);
        double yToPixel = (graphHeight - 2 * padding) / (maxY - minY);
        
        int points = 200;
        Polyline polyline = new Polyline();
        
        for (int i = 0; i <= points; i++) {
            double x = a + i * (b - a) / points;
            double y = function.apply(x);
            
            double pixelX = padding + (x - a) * xToPixel;
            double pixelY = graphHeight - padding - (y - minY) * yToPixel;
            
            polyline.getPoints().addAll(pixelX, pixelY);
        }
        
        polyline.setStroke(Color.BLUE);
        polyline.setStrokeWidth(1);
        graphPane.getChildren().add(polyline);
    }

    private void drawAlgorithmStep(IterationStep step, double minY, double maxY) {
        int functionIndex = functionComboBox.getSelectionModel().getSelectedIndex();
        Function<Double, Double> function = functions[functionIndex];
        
        double a = Double.parseDouble(aField.getText());
        double b = Double.parseDouble(bField.getText());
        
        double xToPixel = (graphWidth - 2 * padding) / (b - a);
        double yToPixel = (graphHeight - 2 * padding) / (maxY - minY);
        
        // Очищаем предыдущие элементы алгоритма
        graphPane.getChildren().removeIf(node -> 
            node instanceof Circle || 
            node instanceof Line && ((Line)node).getStroke() != Color.GRAY);
        
        // Рисуем текущий интервал [a, b]
        double aX = padding + (step.a - a) * xToPixel;
        double bX = padding + (step.b - a) * xToPixel;
        double zeroY = graphHeight - padding - (-minY) * yToPixel;
        
        // Вертикальные линии на границах интервала
        Line aLine = new Line(aX, padding, aX, graphHeight - padding);
        aLine.setStroke(Color.GREEN);
        aLine.setStrokeWidth(1);
        aLine.getStrokeDashArray().addAll(5d, 5d);
        
        Line bLine = new Line(bX, padding, bX, graphHeight - padding);
        bLine.setStroke(Color.GREEN);
        bLine.setStrokeWidth(1);
        bLine.getStrokeDashArray().addAll(5d, 5d);
        
        // Точка c (середина интервала) - теперь на оси X
        double cX = padding + (step.c - a) * xToPixel;
        
        // Уменьшаем размер точки в 2 раза (радиус 2.5 вместо 5)
        Circle cPoint = new Circle(cX, zeroY, 2.5);
        cPoint.setFill(Color.GREEN);
        
        // Линия от точки c до функции (теперь вертикальная линия от оси X к функции)
        double cY = graphHeight - padding - (function.apply(step.c) - minY) * yToPixel;
        Line cLine = new Line(cX, zeroY, cX, cY);
        cLine.setStroke(Color.GREEN);
        cLine.setStrokeWidth(1);
        cLine.getStrokeDashArray().addAll(3d, 3d);
        
        // Если это последняя итерация, рисуем корень
        if (currentStepIndex == steps.size() - 1) {
            Circle rootPoint = new Circle(cX, zeroY, 3); // Уменьшили размер точки корня
            rootPoint.setFill(Color.RED);
            graphPane.getChildren().add(rootPoint);
            
            // Подпись корня
            Text rootLabel = new Text(String.format("Корень: %.6f", step.c));
            rootLabel.setX(cX - 40);
            rootLabel.setY(zeroY - 10);
            rootLabel.setFill(Color.RED);
            graphPane.getChildren().add(rootLabel);
        }
        
        graphPane.getChildren().addAll(aLine, bLine, cLine, cPoint);
    }

    private void startAlgorithm() {
        try {
            double a = Double.parseDouble(aField.getText());
            double b = Double.parseDouble(bField.getText());
            double epsilon = Double.parseDouble(epsilonField.getText());
            int functionIndex = functionComboBox.getSelectionModel().getSelectedIndex();
            
            if (a >= b) {
                showError("Ошибка: a должно быть меньше b");
                aField.requestFocus();
                return;
            }
            
            if (epsilon <= 0) {
                showError("Ошибка: точность должна быть положительной");
                epsilonField.requestFocus();
                return;
            }
            
            Function<Double, Double> function = functions[functionIndex];
            double fa = function.apply(a);
            double fb = function.apply(b);
            
            if (fa * fb >= 0) {
                showError("Ошибка: f(a) * f(b) должно быть < 0");
                aField.requestFocus();
                return;
            }
            
            runBisectionMethod(a, b, epsilon, function, functionIndex);
            
        } catch (NumberFormatException ex) {
            showError("Ошибка: введите корректные числовые значения");
            // Определяем, какое поле вызвало ошибку
            if (!isValidDouble(aField.getText())) {
                aField.requestFocus();
            } else if (!isValidDouble(bField.getText())) {
                bField.requestFocus();
            } else {
                epsilonField.requestFocus();
            }
        }
    }
    
    private boolean isValidDouble(String text) {
        try {
            Double.parseDouble(text);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }

    private void runBisectionMethod(double a, double b, double epsilon, 
                                   Function<Double, Double> function, int functionIndex) {
        steps.clear();
        outputArea.clear();
        currentStepIndex = -1;
        resultLabel.setText("Корень: вычисляется...");
        checkLabel.setText("Проверка: вычисляется...");
        
        double currentA = a;
        double currentB = b;
        int iteration = 0;
        
        outputArea.appendText("Итерация\t   a\t\t   b\t\t   c\t\t   f(c)\t   Погрешность\n");
        outputArea.appendText("------------------------------------------------------------------------\n");
        
        while (Math.abs(currentB - currentA) > epsilon && iteration < 100) {
            iteration++;
            double c = (currentA + currentB) / 2;
            double fc = function.apply(c);
            double error = Math.abs(currentB - currentA) / 2;
            
            steps.add(new IterationStep(iteration, currentA, currentB, c, fc, error));
            
            // Проверяем, не достигли ли мы корня с требуемой точностью
            if (Math.abs(fc) < epsilon) {
                break; // Нашли корень с достаточной точностью
            }
            
            double fa = function.apply(currentA);
            if (fa * fc < 0) {
                currentB = c;
            } else {
                currentA = c;
            }
            
            // Дополнительная проверка: если интервал стал очень маленьким, но еще не достигли точности
            if (Math.abs(currentB - currentA) < 1e-15) {
                break;
            }
        }
        
        // Добавляем финальный шаг, если алгоритм завершился по точности
        if (!steps.isEmpty()) {
            IterationStep lastStep = steps.get(steps.size() - 1);
            double finalC = (currentA + currentB) / 2;
            double finalFc = function.apply(finalC);
            double finalError = Math.abs(currentB - currentA) / 2;
            
            // Если последний шаг не соответствует финальному состоянию, добавляем его
            if (Math.abs(lastStep.c - finalC) > 1e-10) {
                steps.add(new IterationStep(steps.size() + 1, currentA, currentB, finalC, finalFc, finalError));
            }
            
            // Обновляем надпись с результатом и проверкой
            resultLabel.setText(String.format("Корень ≈ %.10f", finalC));
            checkLabel.setText(String.format("Проверка: f(x) ≈ %.6e", finalFc));
        }
        
        startButton.setDisable(true);
        pauseButton.setDisable(false);
        prevButton.setDisable(false);
        nextButton.setDisable(false);
        
        timeline.play();
    }

    private void showNextStep() {
        if (currentStepIndex < steps.size() - 1) {
            currentStepIndex++;
            displayCurrentStep();
        }
    }

    private void showPreviousStep() {
        if (currentStepIndex > 0) {
            currentStepIndex--;
            displayCurrentStep();
        }
    }

    private void displayCurrentStep() {
        if (currentStepIndex >= 0 && currentStepIndex < steps.size()) {
            IterationStep step = steps.get(currentStepIndex);
            
            // Обновляем текстовый вывод
            String output = String.format("%4d\t%10.6f\t%10.6f\t%10.6f\t%10.6f\t%10.6f\n",
                    step.iteration, step.a, step.b, step.c, step.fc, step.error);
            
            outputArea.setText("Итерация\t   a\t\t   b\t\t   c\t\t   f(c)\t   Погрешность\n");
            outputArea.appendText("------------------------------------------------------------------------\n");
            outputArea.appendText(output);
            
            currentIterationLabel.setText(String.format("Итерация: %d/%d", 
                    step.iteration, steps.size()));
            
            // Обновляем визуализацию на графике
            updateChartVisualization(step);
            
            outputArea.setScrollTop(Double.MAX_VALUE);
        }
        
        prevButton.setDisable(currentStepIndex <= 0);
        nextButton.setDisable(currentStepIndex >= steps.size() - 1);
    }

    private void updateChartVisualization(IterationStep step) {
        try {
            double a = Double.parseDouble(aField.getText());
            double b = Double.parseDouble(bField.getText());
            int functionIndex = functionComboBox.getSelectionModel().getSelectedIndex();
            Function<Double, Double> function = functions[functionIndex];
            
            // Вычисляем диапазон Y для правильного масштабирования
            double minY = Double.MAX_VALUE;
            double maxY = -Double.MAX_VALUE;
            int points = 200;
            
            for (int i = 0; i <= points; i++) {
                double x = a + i * (b - a) / points;
                double y = function.apply(x);
                minY = Math.min(minY, y);
                maxY = Math.max(maxY, y);
            }
            
            double yRange = maxY - minY;
            minY -= yRange * 0.1;
            maxY += yRange * 0.1;
            
            // Перерисовываем функцию и оси
            graphPane.getChildren().clear();
            drawAxesWithLabels(a, b, minY, maxY);
            drawFunction(function, a, b, minY, maxY);
            drawAlgorithmStep(step, minY, maxY);
            
        } catch (NumberFormatException ex) {
            // Игнорируем ошибки
        }
    }

    private void togglePause() {
        isPaused = !isPaused;
        pauseButton.setText(isPaused ? "Продолжить" : "Пауза");
        
        if (!isPaused && currentStepIndex < steps.size() - 1) {
            timeline.play();
        } else {
            timeline.pause();
        }
    }

    private void showError(String message) {
        Alert alert = new Alert(Alert.AlertType.ERROR);
        alert.setTitle("Ошибка");
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }

    private static class IterationStep {
        int iteration;
        double a, b, c, fc, error;
        
        IterationStep(int iteration, double a, double b, double c, double fc, double error) {
            this.iteration = iteration;
            this.a = a;
            this.b = b;
            this.c = c;
            this.fc = fc;
            this.error = error;
        }
    }

    private Label createCenteredLabel(String text) {
        Label label = new Label(text);
        label.setAlignment(Pos.CENTER);
        label.setMaxWidth(Double.MAX_VALUE);
        return label;
    }

    public static void main(String[] args) {
        launch(args);
    }
}
