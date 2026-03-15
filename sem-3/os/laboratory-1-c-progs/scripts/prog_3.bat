@echo off
cd ..

echo prog_3 start time : %time%
start "Task 3" /dprog_3 /wait /b prog_3.exe
echo prog_3 end time   : %time%
echo.

pause