using MSharp;
using Domain;

namespace Admin.Settings
{
    public class EmailTemplatesPage : SubPage<SettingsPage>
    {
        public EmailTemplatesPage()
        {
            Add<Modules.EmailTemplatesList>();
        }
    }
}