# Olive.MvcTemplate

This project is a .NET Core MVC template for [M#](https://geeks.ltd.uk/about-us/msharp-technology) projects. 
 
## What is M#?

M# is a model-driven declarative programming model intended for developing data-focused .NET web applications. M# CLI (msharp.exe) will transform everything into standard ASP.NET MVC and C# project source code. So it's safe to adopt for mission critical projects.
M# makes development process much easier and more efficient. Check out [M# Docs](https://github.com/Geeksltd/MSharp.Docs) to get more information about M#.

## Getting Started


### Prerequisites

1. Windows 10
2. Visual Studio 2017 (latest build) with .NET Core and web development features installed.
3. GIT for Windows ([install from here](http://gitforwindows.org/))
4. Yarn Package Manager ([install from here](https://yarnpkg.com/latest.msi))
5. Docker, plus Windows runtime ([install from here](https://docs.docker.com/toolbox/toolbox_install_windows/))

### Build and running the code
1. clone the repo (or you can get the zip file which is not recommended)
2. Open your Visual Studio as ADMIN (it's recommended that you make this the default) then open the solution.
3. Open CMD as administrator, then go to the solution folder with file explorer, after that run Initialize.bat.

### Troubleshooting
If you experienced any problem try the following:
1. Open a cmd and run Initialize.bat file and watch for errors. You might have missing components.
2. Make sure all nuget packages are successfully restored.
3. make sure that the path to the solution is not too long or it does not contain special characters such as space.


## Contributing

As this solution is an opensource project, so contributions are welcome! Just fork the repo, do your changes then make a merge request. 
We'll review your code ASAP and we will do the merge if everything was just fine. If there was any kind of issues, please post it in the [issues](https://github.com/Geeksltd/Olive.MvcTemplate/issues) section.

## Authors

This project is maintained and supported by the GeeksLTD.

See also the list of [contributors](https://github.com/Geeksltd/Olive.MvcTemplate/contributors) who participated in this project.

## License

The template from which M# Core projects (MVC Core) are created. It's available under the GPLv3 license.
