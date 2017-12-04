
@echo off

ECHO ::::::::: Building @Model ::::::::::::::::::::
cd @M#\@Model
call dotnet build -v q

ECHO ::::::::: Building Domain ::::::::::::::::::::
cd ..\..\Domain
call dotnet build -v q
cd ..\Website

ECHO ::::::::: Installing NPM (globally) ::::::::::::::::::::::::::
call npm install -g

ECHO ::::::::: Ensuring bower is installed (globally) ::::::::::::::::::::
WHERE bower > nul
if ERRORLEVEL 1 (	
	call npm install bower -g	
)

ECHO ::::::::: Installing Bower components ::::::::::::::::::::::::
call bower install

ECHO ::::::::: Installing Gulp ::::::::::::::::::::
call npm install gulp

ECHO ::::::::: Rebuilding node-sass module ::::::::::::::::::::
call npm rebuild node-sass

ECHO ::::::::: Running node-sass :::::::::::::::::::
call gulp build-sass

ECHO ::::::::: Restoring Nuget packages ::::::::::::::::::::
call dotnet restore -v q

ECHO ::::::::: Building @UI ::::::::::::::::::::
cd ..\@M#\@UI
call dotnet build -v q