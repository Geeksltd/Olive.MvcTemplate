using MSharp;
using Domain;

namespace Modules
{
    public class Header : GenericModule
    {
        public Header()
        {
            IsInUse().IsViewComponent().WrapInForm(false);
            WrapInForm();
            Using("Olive.Security");
            RootCssClass("header-wrapper");
            var logo = Image("Logo").CssClass("logo").ImageUrl("~/img/Logo.png")
                  .OnClick(x => x.Go("~/"));

            var burger = Link("Burger")
                .NoText()
                .Icon("fas")
                 .CssClass("menu-toggler burger-icon")

                .ExtraTagAttributes("type=\"button\"");

            var login = Link("Login").Icon(FA.UnlockAlt)
                        .VisibleIf(AppRole.Anonymous)
                        .OnClick(x => x.Go<LoginPage>());

            var logout = Link("Logout")
                         .CssClass("align-bottom logout")
                         .ValidateAntiForgeryToken(false)
                         .VisibleIf(CommonCriterion.IsUserLoggedIn)
                         .OnClick(x =>
                         {
                             x.CSharp("await OAuth.Instance.LogOff();");
                             x.Go<LoginPage>();
                         });
            Markup($@"
            <nav class=""navbar"">
              <div class=""header-left-actions-wrapper"">
                      {burger.Ref}
                      {logo.Ref}
              </div>
              <div class=""header-account-wrapper"">
                    {logout.Ref}
                  </div>
            </nav>");

        }
    }
}