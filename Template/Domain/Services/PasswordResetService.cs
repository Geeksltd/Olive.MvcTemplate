namespace Domain
{
    using System;
    using System.Web;
    using Olive;
    using Olive.Services;
    using Olive.Entities;
    using Olive.Web;
    using System.Threading.Tasks;
    using Olive.Security;

    /// <summary>
    /// Provides the functionality to reset a user's password.
    /// </summary>
    public class PasswordResetService
    {
        User User;
        PasswordResetTicket Ticket;
        static IDatabase Database => Context.Current.Database();

        PasswordResetService(User user) { User = user; }

        /// <summary>
        /// Creates a new Password Reset Ticket for the specified user.
        /// </summary>
        public static async Task RequestTicket(User user)
        {
            var service = new PasswordResetService(user);

            using (var scope = Database.CreateTransactionScope())
            {
                await service.CreateTicket();
                service.SendEmail();
                scope.Complete();
            }
        }

        /// <summary>
        /// Completes the password recovery process.
        /// </summary>
        public static async Task Complete(PasswordResetTicket ticket, string newPassword)
        {
            if (newPassword.IsEmpty()) throw new ArgumentNullException(nameof(newPassword));

            if (ticket.IsExpired)
                throw new ValidationException("This ticket has expired. Please request a new ticket.");

            if (ticket.IsUsed) throw new ValidationException("This ticket has been used once. Please request a new ticket.");

            var service = new PasswordResetService(ticket.User);

            using (var scope = Database.CreateTransactionScope())
            {
                await service.UpdatePassword(newPassword);
                await Database.Update(ticket, t => t.IsUsed = true);

                scope.Complete();
            }
        }

        Task CreateTicket() => Database.Save(Ticket = new PasswordResetTicket { User = User });

        void SendEmail()
        {
            // TODO: Invoke the email service's API to send it.

            //EmailTemplate.RecoverPassword.Send(User, new
            //{
            //    UserId = User.Name,
            //    Link = $"<a href='{GetResetPasswordUrl()}'> Reset Your Password </a>",
            //});
        }

        string GetResetPasswordUrl() => Context.Current.Request().GetAbsoluteUrl("/password/reset/" + Ticket.ID);

        Task UpdatePassword(string clearTextPassword)
        {
            var pass = SecurePassword.Create(clearTextPassword);
            return Database.Update(User, u => { u.Password = pass.Password; u.Salt = pass.Salt; });
        }
    }
}
