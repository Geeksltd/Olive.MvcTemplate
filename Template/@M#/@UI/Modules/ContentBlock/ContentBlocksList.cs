using MSharp;
using Domain;

namespace Modules
{
    public class ContentBlocksList : ListModule<Domain.ContentBlock>
    {
        public ContentBlocksList()
        {
            ShowHeaderRow().Sortable().HeaderText("Content Blocks");

            LinkColumn("Key")
                .HeaderText("Key")
                .Text("c#:item.Key")
                .SortKey("Key")
            .Action(x =>
            {
                x.Go<Admin.Settings.ContentBlocks.EnterPage>().SendReturnUrl()
                    /*M#:w[18]T-Prop:Key-Type:QueryStringParameter-The destination page doesn't seem to utilise Query String 'item' anywhere.*/.Send("item", "item.ID");
            });

            Column(x => x.Content).DisplayExpression("c#:item.Content.OrEmpty().RemoveHtmlTags().Summarize(80)");

            Button("New Content Block").Icon(FA.Plus)
                .Action(x => x.Go<Admin.Settings.ContentBlocks.EnterPage>().SendReturnUrl());
        }
    }
}
