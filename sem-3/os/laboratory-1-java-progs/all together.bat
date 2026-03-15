@echo off
rem chcp 65001
echo .bat      start time : %time%
echo.

start "Task 1" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_small Main

start "Task 2" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_medium Main

start /w "Task 3" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_large Main

echo.
echo .bat        end time : %time%
pause