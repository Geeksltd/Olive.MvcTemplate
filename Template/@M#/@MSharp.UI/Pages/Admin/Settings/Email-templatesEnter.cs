using MSharp;
using Domain;

namespace Admin.Settings.EmailTemplates
{
    public class EnterPage : SubPage<EmailTemplatesPage>
    {
        public EnterPage()
        {
            Add<Modules.EmailTemplateForm>();
        }
    }
}