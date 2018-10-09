using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceData : IReferenceData
    {
        IDatabase Database;
        public ReferenceData(IDatabase database) => Database = database;

        async Task<T> Create<T>(T item) where T : IEntity
        {
            await Context.Current.Database().Save(item, SaveBehaviour.BypassAll);
            return item;
        }

        public async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });

            await CreateContentBlocks();
            await CreateAdmin();
        }

        async Task CreateContentBlocks()
        {
            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.LoginIntro),
                Content = "<p>Welcome to our application.<br/>Please log in using the form below.</p>"
            });

            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.PasswordSuccessfullyReset),
                Content = "Your password has been successfully reset."
            });
        }

        Task CreateAdmin()
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