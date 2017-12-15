
@echo off

ECHO.
ECHO ::::::::: Building @Model :::::::::::::::::::::::::::::::::::::::::::::
ECHO.
cd @M#\@Model
call dotnet build -v q

ECHO.
ECHO ::::::::: Building Domain :::::::::::::::::::::::::::::::::::::::::::::
ECHO.
cd ..\..\Domain
call dotnet build -v q
cd ..\Website

ECHO.
ECHO ::::::::: Installing YARN (globally) :::::::::::::::::::::::::::::::::::
ECHO.
call yarn install

ECHO.
ECHO ::::::::: Ensuring bower is installed (globally) ::::::::::::::::::::::
ECHO.
WHERE bower > nul
if ERRORLEVEL 1 (	
	call yarn global add bower	
)

ECHO.
ECHO ::::::::: Installing WebPack (globally) ::::::::::::::::::::::
ECHO.
npm install webpack -g

ECHO.
ECHO ::::::::: Installing Bower components :::::::::::::::::::::::::::::::::
ECHO.
call bower install


ECHO.
ECHO ::::::::: Rebuilding sass files :::::::::::::::::::::::::::::::::
ECHO.
call wwwroot\Styles\build\SassCompiler.exe Sasscompilerconfig.json

ECHO.
ECHO ::::::::: Restoring Nuget packages ::::::::::::::::::::::::::::::::::::
ECHO.
call dotnet restore -v q
call dotnet build

ECHO.
ECHO ::::::::: Building @UI ::::::::::::::::::::::::::::::::::::::::::::::::
ECHO.
cd ..\@M#\@UI
call dotnet build -v q