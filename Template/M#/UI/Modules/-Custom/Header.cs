using MSharp;
using Domain;

namespace Modules
{
    public class Header : GenericModule
    {
        public Header()
        {
            IsInUse().IsViewComponent().WrapInForm(false);

            var logo = Image("Logo").CssClass("logo").ImageUrl("~/img/Logo.png")
                  .OnClick(x => x.Go("~/"));

            var burger = Link("Burger")
                .NoText()
                .CssClass("navbar-toggler collapsed")
                .Icon(FA.Bars)
                .ExtraTagAttributes("type=\"button\" data-toggle=\"collapse\" " +
                "data-target=\".navbar-collapse\" aria-expanded=\"false\" " +
                "aria-controls=\".navbar-collapse\" aria-label=\"Toggle navigation\"");

            Markup($@"
            <nav class=""navbar navbar-expand-md navbar-inverse bg-dark sticky-top"">
                      {logo.Ref}
                      {burger.Ref}
                 <div class=""collapse navbar-collapse"">
                     @(await Component.InvokeAsync<MainMenu>())
                 </div>
            </nav>");

            Reference<MainMenu>();
        }
    }
}