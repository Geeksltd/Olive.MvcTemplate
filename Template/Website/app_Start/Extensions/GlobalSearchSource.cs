using Domain;
using Olive.Entities.Data;
using Olive.GlobalSearch;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Website
{
    public class GlobalSearchSource : SearchSource
    {
        public override async Task Process(ClaimsPrincipal user)
        {
            Add(new SearchResult()
            {
                Title = "Sample Title",
                Description = "Description here",
                Url = "/",
                IconUrl = ""
            });

            //TODO: Please add custom search data here. For instance:

            // foreach (var customer in Database.Instance.GetList<Customer>())
            // {
            //     if (!MatchesKeywords(customer, x => x.Name, x => x.CompanyName)) continue;

            //     Add(new SearchResult
            //     {
            //         Title = customer.ToString(),
            //         Url = "/customers/view/" + customer.ID,
            //         Description = "..."
            //     });
            // }
        }
    }
}