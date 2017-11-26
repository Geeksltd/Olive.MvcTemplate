
cd @M#\@Model
dotnet build

cd ..\..\Domain
dotnet build

cd..
wwwroot\Styles\build\Sass-to-css.bat

cd Website
dotnet build

cd ..\@M#\@UI
dotnet build

