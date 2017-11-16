using MSharp;
using Domain;

namespace Modules
{
    public class ContentBlockForm : FormModule<Domain.ContentBlock>
    {
        public ContentBlockForm()
        {
            HeaderText("Content Block Details");

            Field(x => x.Key).Readonly();
            Field(x => x.Content).Control(ControlType.HtmlEditor);

            Field(x => x.Attachment);

            //================ Buttons: ================

            Button("Cancel").Action().ReturnToPreviousPage();

            Button("Save").IsDefault().Icon(FA.Check)
            .Action(x =>
            {
                x.SaveInDatabase();
                x.ReturnToPreviousPage();
            });
        }
    }
}