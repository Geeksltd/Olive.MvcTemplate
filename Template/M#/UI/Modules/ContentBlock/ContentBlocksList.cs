using Domain;
using MSharp;

namespace Modules
{
    public class ContentBlocksList : ListModule<Domain.ContentBlock>
    {
        public ContentBlocksList()
        {
            ShowHeaderRow().Sortable().HeaderText("Content Blocks");

            LinkColumn(x => x.Key)
                .OnClick(x =>
                {
                    x.Go<Admin.Settings.ContentBlocks.EnterPage>()
                        .SendReturnUrl()
                        .Send("item", "item.ID");
                });

            Column(x => x.Content)
                .DisplayExpression(cs("item.Content.OrEmpty().RemoveHtmlTags().Summarize(80)"));

            Button("New Content Block")
                .Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.ContentBlocks.EnterPage>().SendReturnUrl());
        }
    }
}
