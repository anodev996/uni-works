@echo off
cd ..

echo programs start time : %time%
echo.

start "Task 1" /dprog_1 /b prog_1.exe

start "Task 2" /dprog_2 /b prog_2.exe

start "Task 3" /dprog_3 /wait /b prog_3.exe
echo.

echo programs end time   : %time%
echo.

pause