@echo off

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
npm rebuild node-sass --force

ECHO Running node-sass ..............
call node_modules\.bin\gulp build-sass