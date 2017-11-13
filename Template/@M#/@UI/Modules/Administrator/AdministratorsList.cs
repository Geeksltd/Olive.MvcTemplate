using MSharp;
using Domain;

namespace Modules
{
    public class AdministratorsList : ListModule<Domain.Administrator>
    {
        public AdministratorsList()
        {
            ShowFooterRow()
                .ShowHeaderRow()
                .Sortable()
                .UseDatabasePaging(false)
                .HeaderText("Administrators")
                .PageSize("10");

            //================ Search: ================

            Search(GeneralSearch.AllFields).Label("Find:");

            SearchButton("Search").Icon(FA.Search)
                .Action(x => x.Reload());

            //================ Columns: ================

            LinkColumn("Name").HeaderText("Name").Text("c#:item.Name").SortKey("Name")
            .Action(x =>
            {
                x.Go<Admin.Settings.Administrators.ViewPage>().SendReturnUrl()
                    .Send("item", "item.ID");
            });

            Column(x => x.Email);

            Column(x => x.IsDeactivated).NeedsMerging().DisplayExpression("c#:item.IsDeactivated");

            ButtonColumn("Edit").HeaderText("Actions").GridColumnCssClass("actions").Icon(FA.Edit)
            .Action(x => x.PopUp<Admin.Settings.Administrators.EnterPage>()
                          .Send("item", "item.ID"));

            ButtonColumn("Delete").HeaderText("Actions")
                .GridColumnCssClass("actions")
                .ConfirmQuestion("Are you sure you want to delete this Administrator?")
                .CssClass("btn-danger")
                .Icon(FA.Remove)
            .Action(x =>
            {
                x.DeleteItem();
                x.Reload();
            });

            Button("New Administrator").Icon(FA.Plus)
                .Action(x => x.PopUp<Admin.Settings.Administrators.EnterPage>());
        }
    }
}