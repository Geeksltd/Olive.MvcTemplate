
@echo off

echo {"runtimeOptions":{"tfm":"netcoreapp2.0","framework":{"name":"Microsoft.NETCore.App","version": "2.0.0"}}} >  M#\lib\netcoreapp2.0\MSharp.DSL.runtimeconfig.json

WHERE yarn > nul
if ERRORLEVEL 1 (
    echo YARN is not installed! You can install it from https://yarnpkg.com/latest.msi
	goto error
)

ECHO ::::::::: Ensuring WebPack is installed (globally) ::::::::::::::::::::::
call yarn global add webpack
if ERRORLEVEL 1 (goto error)

ECHO ::::::::: Ensuring bower is installed (globally) ::::::::::::::::::::::
WHERE bower > nul
if ERRORLEVEL 1 (call yarn global add bower)
WHERE bower > nul
if ERRORLEVEL 1 (goto error)


ECHO ::::::::: Building #Model :::::::::::::::::::::::::::::::::::::::::::::
cd M#\Model
call dotnet build -v q
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO ::::::::: Building Domain :::::::::::::::::::::::::::::::::::::::::::::
cd ..\..\Domain
call dotnet build -v q
if ERRORLEVEL 1 (goto error)

cd ..\Website
ECHO.
ECHO ::::::::: Installing YARN :::::::::::::::::::::::::::::::::::
call yarn install
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO ::::::::: Installing Bower components :::::::::::::::::::::::::::::::::
call bower install
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO ::::::::: Building sass files :::::::::::::::::::::::::::::::::
call wwwroot\Styles\build\SassCompiler.exe Compilerconfig.json
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO ::::::::: Restoring Olive DLLs ::::::::::::::::::::::::::::::::::::
call dotnet build
if ERRORLEVEL 1 (goto error)

ECHO.
ECHO ::::::::: Building #UI ::::::::::::::::::::::::::::::::::::::::::::::::
ECHO.
cd ..\M#\UI
call dotnet build -v q
if ERRORLEVEL 1 (goto error)

exit /b 0


:error
echo ##################################
echo Error occured!!!
echo Please run Initialize.bat again after fixing it.
echo ##################################
set /p cont= Press Enter to exit.
exit /b -1