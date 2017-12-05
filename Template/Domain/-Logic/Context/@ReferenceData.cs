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
        static Task Create(IEntity item) => Database.Instance.Save(item, SaveBehaviour.BypassAll);

        public static async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });

            await CreateEmailTemplates();
            await CreateContentBlocks();
            await CreateAdmin();
        }

        static Task CreateEmailTemplates()
        {
            return Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.RecoverPassword),
                Subject = "Recover password",
                MandatoryPlaceholders = "USERID, LINK",
                Body = @"Dear [#USERID#],</br></br>
                         Please click on the following link to reset your password.</br>
                         If you did not request this password reset then please contact us.</br></br>
                         <div>[#LINK#]</div><br/>
                         Best regards"
            });
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
                Email = "admin@uat.co",
                FirstName = "Geeks",
                LastName = "Admin",
                Password = pass.Password,
                Salt = pass.Salt
            });
        }
    }
}