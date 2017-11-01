namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Olive;
    using Olive.Entities;
    using Olive.Services;
    using Olive.Services.ImpersonationSession;
    using Olive.Web;

    /// <summary> 
    /// Provides the business logic for Administrator class.
    /// </summary>
    partial class Administrator : IImpersonator
    {
        /// <summary>
        /// Gets the roles of this user.
        /// </summary>
        public override IEnumerable<string> GetRoles() => base.GetRoles().Concat("Administrator");

        bool IImpersonator.CanImpersonate(IUser user) => true;
    }
}