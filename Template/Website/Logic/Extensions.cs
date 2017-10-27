namespace System
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Builder;
    using Olive;
    using Olive.Entities.Data;

    public static class Extensions
    {
        public static Task<User> Extract(this ClaimsPrincipal principal)
        {
            var id = principal?.FindFirst(ClaimTypes.Name)?.Value;
            return Database.Instance.GetOrDefault<User>(id);
        }

        public static RequestLocalizationOptions RequestLocalizationOptions(this CultureInfo culture)
        {
            return new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture),
                SupportedCultures = new List<CultureInfo> { culture },
                SupportedUICultures = new List<CultureInfo> { culture }
            };
        }
    }
}
