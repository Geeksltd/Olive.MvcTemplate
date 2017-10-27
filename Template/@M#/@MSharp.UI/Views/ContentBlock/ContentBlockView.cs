using MSharp;
using Domain;

namespace Modules
{
    public class ContentBlockView : ViewModule<Domain.ContentBlock>
    {
        public ContentBlockView()
        {
            IsViewComponent().DataSource("await ContentBlock.FindByKey(info.Key)")
                .Markup("@info.Output.Raw()");
            
            //================ Code Extensions: ================
            
            Property<string>("Key");
            
            Property<string>("Output").Getter(@"if (Item == null)
            return ""No content found for key: '"" + Key + ""'"";
            
            return Item.Content;");
        }
    }
}