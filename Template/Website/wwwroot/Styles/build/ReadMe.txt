You should do it once:

	1: right click on the Website in Solution explorer and  then "Open Folder in File Explorer".
	2: Click somewhere empty on the folder, Shift + Right click , Open Command Line Here
	3: Type wwwroot\Styles\build\build.Sass-to-css.bat


PREPARING FOR RELEASE
=======================================

When you are ready to deploy, for better performance run Minify.bat the same as above.
Also update web.config and set <compilation debug="false"/>.