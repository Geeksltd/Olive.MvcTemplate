using System;
using System.Threading.Tasks;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;

namespace Domain
{
    public class ReferenceData
    {
        static Task<T> Create<T>(T item) where T : IEntity
        {
            return Context.Current.Database().Save(item, SaveBehaviour.BypassAll).ContinueWith(x => item);
        }

        public static async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });

            await CreateContentBlocks();
            await CreateAdmin();
        }

        static async Task CreateContentBlocks()
        {
            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.LoginIntro),
                Content = "Welcome to our application. Please log in using the form below."
            });

            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.PasswordSuccessfullyReset),
                Content = "Your password has been successfully reset."
            });
        }

        static Task CreateAdmin()
        {
            var pass = SecurePassword.Create("test");
            return Create(new Administrator
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = "admin@uat.co",
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = "Geeks",
                LastName = "Admin",
                Password = pass.Password,
                Salt = pass.Salt
            });
        }
    }
}