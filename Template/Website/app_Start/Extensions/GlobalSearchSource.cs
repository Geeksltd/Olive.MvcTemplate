using System.Security.Claims;
using System.Threading.Tasks;
using Olive.GlobalSearch;

namespace Website
{
    public class GlobalSearchSource : SearchSource
    {
        public override async Task Process(ClaimsPrincipal user)
        {
            Add("Admin", "/admin");
            Add("Admin > Setting", "/admin/setting");
            Add("Admin > Settings > Administrators", "/admin/settings/administrators");
            Add("Admin > Settings > Content blocks", "/admin/settings/content-blocks");
            Add("Admin > Settings > General", "/admin/settings/general");

            //TODO: Add other pages here
        }
    }
}