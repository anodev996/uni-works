@echo off
rem chcp 65001
echo .bat      start time : %time%
echo.

start /b /w "Task 1" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_small Main >nul
echo prog_small  end time : %time%

start /b /w "Task 2" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_medium Main >nul
echo prog_medium end time : %time%

start /b /w "Task 3" C:\_vscode_portable\jdk-21.0.9\bin\java.exe -Xint -XX:-TieredCompilation -XX:CICompilerCount=0 -cp prog_large Main >nul
echo prog_large  end time : %time%

echo.
echo .bat        end time : %time%
pause