using MSharp;
using Domain;

namespace Modules
{
    public class EmailTemplatesList : ListModule<Domain.EmailTemplate>
    {
        public EmailTemplatesList()
        {
            ShowHeaderRow().Sortable().HeaderText("Email Templates");
            
            //================ Columns: ================
            
            LinkColumn("Key").HeaderText("Key").Text("c#:item.Key").SortKey("Key")
            .Action(x =>
            {
                x.Go<Admin.Settings.EmailTemplates.EnterPage>().SendReturnUrl()
                    .Send("item", "item.ID");
            });
            
            Column(x => x.Subject);
            
            Column(x => x.Body).DisplayExpression("c#:item.Body.RemoveHtmlTags().Summarize(150).Remove(\"\\r\").KeepReplacing(\"\\n\\n\", \"\\n\").ToHtmlLines()");
            
            Column(x => x.MandatoryPlaceholders);
        }
    }
}