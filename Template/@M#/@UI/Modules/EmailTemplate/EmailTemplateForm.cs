using MSharp;
using Domain;

namespace Modules
{
    public class EmailTemplateForm : FormModule<Domain.EmailTemplate>
    {
        public EmailTemplateForm()
        {
            SupportsAdd(false).SupportsEdit().HeaderText("Email Template Details");

            Box("Test", BoxTemplate.HeaderBox)
                .Add(
                    Field(x => x.Key),
                    Field(x => x.Subject),
                    Field(x => x.Body).ExtraControlAttributes("data_toolbar=\"Compact\"")
                                      .Control(ControlType.HtmlEditor),
                    Field(x => x.MandatoryPlaceholders)
            );

            Button("Cancel").CausesValidation(false).OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.ReturnToPreviousPage();
            });
        }
    }
}