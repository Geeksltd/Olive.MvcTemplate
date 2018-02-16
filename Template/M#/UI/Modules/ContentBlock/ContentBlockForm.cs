using MSharp;

namespace Modules
{
    public class ContentBlockForm : FormModule<Domain.ContentBlock>
    {
        public ContentBlockForm()
        {
            HeaderText("Content Block Details");

            Field(x => x.Key).Readonly();
            Field(x => x.Content).AsHtmlEditor();

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
                .OnClick(x =>
                {
                    x.SaveInDatabase();
                    x.ReturnToPreviousPage();
                });
        }
    }
}