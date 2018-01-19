using MSharp;

namespace App
{
    public class Project : MSharp.Project
    {
        public Project()
        {
            Name("MY.MICROSERVICE.NAME").SolutionFile("MY.MICROSERVICE.NAME.sln");

            Layout("Default").AjaxRedirect().Default().VirtualPath("~/Views/Layouts/Default.cshtml");
            Layout("Modal").Modal().VirtualPath("~/Views/Layouts/Modal.cshtml");

            // ------------------ Automated Tasks ------------------

            AutoTask("Clean old temp uploads").Every(10, TimeUnit.Minute)
                .Run("await Olive.Mvc.FileUploadService.DeleteTempFiles(olderThan: 1.Hours());");

            foreach (var role in "Dev,QA,BA,PM,AM,Director,Designer,IT,Reception,PA,Sales".Split(','))
                foreach (var level in ",Junior,Senior,Lead,Head".Split(','))
                    Role(level + role);
        }
    }
}