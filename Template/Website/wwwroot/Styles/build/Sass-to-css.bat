@echo off

ECHO.
ECHO ::::::::: Rebuilding sass files :::::::::::::::::::::::::::::::::
ECHO.
call SassCompiler.exe ..\..\..\Sasscompilerconfig.json
