
@echo off

cd @M#\@Model
call dotnet build

cd ..\..\Domain
call dotnet build

cd ..\Website

ECHO Checking bower is installed globally..............
where bower > nul
if ERRORLEVEL 1 (
	echo Installing bower globally...
	npm install bower -g	
)

ECHO Running NPM ..............
call npm install

ECHO Running BOWER ..............
call bower install

ECHO Rebuilding node-sass module ..............
call npm rebuild node-sass

ECHO Running node-sass ..............
call node_modules\.bin\gulp build-sass

cd Website
call dotnet build

cd ..\@M#\@UI
call dotnet build