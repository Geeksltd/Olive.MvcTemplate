using MSharp;
using Domain;

namespace Modules
{
    public class ContentBlockView : ViewModule<Domain.ContentBlock>
    {
        public ContentBlockView()
        {
            IsViewComponent()
                .DataSource("await ContentBlock.FindByKey(info.Key)")
                .Markup("@info.Output.Raw()");

            //================ Code Extensions: ================

            ViewModelProperty<string>("Key");

            ViewModelProperty<string>("Output")
                .Getter(@"if (Item == null) return ""No content found for key: '"" + Key + ""'"";
                             return Item.Content;");
        }
    }
}