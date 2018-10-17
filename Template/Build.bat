@echo off

call dotnet tool install -g msharp-build
call msharp-build %1 %2 %3 %4 %5

if ERRORLEVEL 1 (    
	echo ##################################    
    set /p cont= Error occured. Press Enter to exit.
    exit /b -1
)