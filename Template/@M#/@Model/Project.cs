using MSharp;

namespace App
{
    public class Project : MSharp.Project
    {
        public Project()
        {
            AjaxRedirect()
                .UseSqlDatetime2()
                .AvoidColonForFormElements()
                .AvoidColonForSearchElements()
                .AvoidColonForViewElements()
                .NetCore()
                .StyleRequiredFormElements()
                .SupportsCSharp6()
                .Name("MY.PROJECT.NAME")
                .DataAccessNamespace("AppData")
                .ValidationStyle("Tooltip")
                .Encoding("UTF-8")
                .GeneratedDALFolder("DAL")
                .ModelProjectFolder("Domain")
                .PageModuleContainerCss("module")
                .SolutionFile("MY.PROJECT.NAME.sln")
                .DefaultDateFormat("{0:yyyy-MM-dd}")
                .DefaultDateTimeFormat("{0:yyyy-MM-dd @ hh:mm tt}")
                .DefaultTimeFormat("{0:hh:mm tt}")
                .ListButtonsLocation("Top");

            Role("User");
            Role("Local.Request");
            Role("Anonymous");
            Role("Administrator").SkipQueryStringSecurity();

            Layout("Front end").AjaxRedirect().Default().VirtualPath("~/Views/Layouts/FrontEnd.cshtml");
            Layout("Front end Modal").Modal().VirtualPath("~/Views/Layouts/FrontEnd.Modal.cshtml");

            PageSettings("LeftMenu", "SubMenu", "TopMenu");

            // ------------------ Automated Tasks ------------------

            AutoTask("Clean old temp uploads").Every(10, TimeUnit.Minute)
                .Run("await FileUploadService.DeleteTempFiles(olderThan: 1.Hours());");

            AutoTask("Send email queue items").Every(1, TimeUnit.Minute)
                .Run("await Olive.Services.Email.EmailService.SendAll();");
        }
    }
}