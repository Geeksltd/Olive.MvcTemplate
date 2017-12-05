
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
ECHO ::::::::: Installing NPM (globally) :::::::::::::::::::::::::::::::::::
ECHO.
call npm install -g

ECHO.
ECHO ::::::::: Ensuring bower is installed (globally) ::::::::::::::::::::::
ECHO.
WHERE bower > nul
if ERRORLEVEL 1 (	
	call npm install bower -g	
)

ECHO.
ECHO ::::::::: Installing Bower components :::::::::::::::::::::::::::::::::
ECHO.
call bower install

ECHO.
ECHO ::::::::: Installing Gulp :::::::::::::::::::::::::::::::::::::::::::::
ECHO.
call npm install gulp

ECHO.
ECHO ::::::::: Rebuilding node-sass module :::::::::::::::::::::::::::::::::
ECHO.
call npm rebuild node-sass

ECHO.
ECHO ::::::::: Running node-sass :::::::::::::::::::::::::::::::::::::::::::
ECHO.
call gulp build-sass

ECHO.
ECHO ::::::::: Restoring Nuget packages ::::::::::::::::::::::::::::::::::::
ECHO.
call dotnet restore -v q

ECHO.
ECHO ::::::::: Building @UI ::::::::::::::::::::::::::::::::::::::::::::::::
ECHO.
cd ..\@M#\@UI
call dotnet build -v q