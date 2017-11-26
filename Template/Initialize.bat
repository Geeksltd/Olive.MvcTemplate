
cd @M#\@Model
dotnet build

cd ..\..\Domain
dotnet build

cd ..\Website
wwwroot\Styles\build\Sass-to-css.bat

cd Website
dotnet build

cd ..\@M#\@UI
dotnet build

