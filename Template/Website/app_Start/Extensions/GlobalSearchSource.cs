using System.Security.Claims;
using System.Threading.Tasks;
using Olive.GlobalSearch;

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

            //TODO: Please add custom search data here
        }
    }
}