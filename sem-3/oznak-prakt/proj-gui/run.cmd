@echo off
chcp 65001 >nul
set PATH=C:\_vscode_portable\jdk-21.0.9\bin;%PATH%
set PATH_TO_FX="C:\_vscode_portable\javafx-sdk-21.0.9\lib"

start "" javaw --module-path %PATH_TO_FX% --add-modules javafx.controls,javafx.fxml,javafx.graphics -cp out BisectionMethodVisualizer
