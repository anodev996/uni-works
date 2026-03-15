@echo off
cd ..

echo prog_2 start time : %time%
start "Task 2" /dprog_2 /wait /b prog_2.exe
echo prog_2 end time   : %time%
echo.

pause