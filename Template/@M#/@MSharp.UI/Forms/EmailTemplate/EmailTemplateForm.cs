using MSharp;
using Domain;

namespace Modules
{
    public class EmailTemplateForm : FormModule<Domain.EmailTemplate>
    {
        public EmailTemplateForm()
        {
            SupportsAdd(false).SupportsEdit().HeaderText("Email Template Details");
            
            Field(x => x.Key);
            
            Field(x => x.Subject);
            
            Field(x => x.Body).ExtraControlAttributes("data_toolbar=\"Compact\"").Control(ControlType.HtmlEditor);
            
            Field(x => x.MandatoryPlaceholders);
            
            //================ Buttons: ================
            
            Button("Cancel").CausesValidation(false)
                .Action(x => x.ReturnToPreviousPage());
            
            Button("Save").IsDefault().Icon(FA.Check)
            .Action(x =>
            {
                x.SaveInDatabase();
                x.ReturnToPreviousPage();
            });
        }
    }
}