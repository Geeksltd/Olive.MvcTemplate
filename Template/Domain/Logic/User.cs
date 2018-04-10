namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Security;
    using Olive.Web;

    partial class User : ILoginInfo
    {
        string ILoginInfo.DisplayName => Name;

        string ILoginInfo.ID => ID.ToString();

        TimeSpan? ILoginInfo.Timeout => Config.Get("Authentication:Timeout", defaultValue: 20).Minutes();

        /// <summary>
        /// Gets the roles of this user.
        /// </summary>
        public virtual IEnumerable<string> GetRoles()
        {
            if (Context.Current.Request().IsLocal()) yield return "Local.Request";
            yield return "User";
        }

        /// <summary>
        /// Specifies whether or not this user has a specified role.
        /// </summary>
        public bool IsInRole(string role) => GetRoles().Contains(role);

        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Insert)
                await PasswordResetService.RequestTicket(this);
        }
    }
}