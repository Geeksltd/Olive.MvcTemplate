namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Web;
    using Olive.Security;

    partial class User : IUser, IPrincipal, IIdentity
    {
        /// <summary>
        /// Gets the roles of this user.
        /// </summary>
        public virtual IEnumerable<string> GetRoles()
        {
            if (Context.Request.IsLocal()) yield return "Local.Request";

            yield return "User";
        }

        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Insert)
                await PasswordResetService.RequestTicket(this);
        }

        #region IPrincipal

        IIdentity IPrincipal.Identity => this;

        /// <summary>
        /// Specifies whether or not this user has a specified role.
        /// </summary>
        public bool IsInRole(string role) => GetRoles().Contains(role);

        string IIdentity.AuthenticationType => "ApplicationAuthentication";

        bool IIdentity.IsAuthenticated => true;

        string IIdentity.Name => ID.ToString();

        #endregion
    }
}