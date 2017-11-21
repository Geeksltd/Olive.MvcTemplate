using MSharp;
using Domain;

namespace Modules
{
    public class Header : GenericModule
    {
        public Header()
        {
            IsInUse().IsViewComponent().WrapInForm(false)
                .Markup(@"<nav class=""navbar navbar-inverse navbar-static-top"">
            
            <div class=""nav-wrapper"">
            
            <div class=""navbar-header"">
            <div class=""navbar-brand"">[#BUTTONS(Logo)#]</div>
            [#BUTTONS(Burger)#]
            </div>
            
            <div class=""collapse navbar-collapse"">
            @(await Component.InvokeAsync<MainMenu>()))
            </div>
            </div>
            </nav>");
                        
            Image("Logo").CssClass("logo").ImageUrl("~/public/img/Logo.png")
                .OnClick(x => x.Go("~/"));
            
            Link("Burger").NoText()
                .ExtraTagAttributes("type=\"button\" data-toggle=\"collapse\" data-target=\".navbar-collapse\"")
                .CssClass("navbar-toggle collapsed").Icon(FA.Bars);
            
            Reference<MainMenu>();
        }
    }
}