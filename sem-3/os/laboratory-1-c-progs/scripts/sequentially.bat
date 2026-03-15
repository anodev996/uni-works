@echo off
cd ..

echo prog_1 start time : %time%
start "Task 1" /dprog_1 /wait /b prog_1.exe
echo prog_1 end time   : %time%

timeout /t 4 /nobreak
echo.


echo prog_2 start time : %time%
start "Task 2" /dprog_2 /wait /b prog_2.exe
echo prog_2 end time   : %time%

timeout /t 4 /nobreak
echo.

echo prog_3 start time : %time%
start "Task 3" /dprog_3 /wait /b prog_3.exe
echo prog_3 end time   : %time%
echo.

pause