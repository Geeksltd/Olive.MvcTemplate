using MSharp;
using Domain;

namespace Modules
{
    public class EmailTemplateForm : FormModule<Domain.EmailTemplate>
    {
        public EmailTemplateForm()
        {
            SupportsAdd(false).HeaderText("Email Template Details");

            MasterDetail<Details>(x => x.Blocks);

            Box("Test", BoxTemplate.HeaderBox)
                .Add(
                    Field(x => x.Key),
                    Field(x => x.Subject),
                    Field(x => x.Body)
                         .AsHtmlEditor()
                         .ExtraControlAttributes("data_toolbar=\"Compact\""),
                    Field(x => x.MandatoryPlaceholders)
            );

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save")
                .IsDefault()
                .Icon(FA.Check)
                .OnClick(x =>
                {
                    x.SaveInDatabase();
                    x.ReturnToPreviousPage();
                });
        }
    }

    public class Details : FormModule<ContentBlock>
    {
        public Details()
        {
            Field(x => x.Key);
        }
    }
}