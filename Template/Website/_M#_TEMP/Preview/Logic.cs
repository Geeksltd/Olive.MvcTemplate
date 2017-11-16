namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Services.Email;

    /// <summary>
    /// Provides the business logic for EmailTemplate class.
    /// </summary>
    partial class EmailTemplate : IEmailTemplate
    {
        /// <summary>
        /// Validates this instance to ensure it can be saved in a data repository.
        /// If this finds an issue, it throws a ValidationException for that.
        /// This calls ValidateProperties(). Override this method to provide custom validation logic in a type.
        /// </summary>
        public override async Task Validate()
        {
            await base.Validate();
            this.EnsurePlaceholders();
        }

        /// <summary>
        /// Sends an email to the specified user using this template and the merge data provided.
        /// </summary>
        public void Send(User toUser, object mergeData, Action<EmailQueueItem> customise = null)
        {
            Send(toUser.Email, mergeData, customise);
        }

        /// <summary>
        /// Sends an email to the specified email(s) using this template and the merge data provided.
        /// Use comma to separate multiple emails.
        /// </summary>
        public void Send(string to, object mergeData, Action<EmailQueueItem> customise = null)
        {
            if (mergeData == null) throw new ArgumentNullException(nameof(mergeData));
            if (to.IsEmpty()) throw new ArgumentNullException(nameof(to));

            var message = new EmailQueueItem
            {
                Html = true,
                Subject = this.MergeSubject(mergeData),
                Body = this.MergeBody(mergeData),
                To = to,
            };

            customise?.Invoke(message);

            Database.Save(message);
        }
    }
}