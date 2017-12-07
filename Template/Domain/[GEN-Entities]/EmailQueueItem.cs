namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    
    /// <summary>Represents an instance of Email queue item entity type.</summary>
    [CacheObjects(false)]
    [SoftDelete]
    public partial class EmailQueueItem : GuidEntity, Olive.Services.Email.IEmailQueueItem
    {
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the EmailQueueItem class.</summary>
        public EmailQueueItem() => Date = LocalTime.Now;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of Attachments on this Email queue item instance.</summary>
        public string Attachments { get; set; }
        
        /// <summary>Gets or sets the value of Bcc on this Email queue item instance.</summary>
        public string Bcc { get; set; }
        
        /// <summary>Gets or sets the value of Body on this Email queue item instance.</summary>
        public string Body { get; set; }
        
        /// <summary>Gets or sets the value of Category on this Email queue item instance.</summary>
        public string Category { get; set; }
        
        /// <summary>Gets or sets the value of Cc on this Email queue item instance.</summary>
        public string Cc { get; set; }
        
        /// <summary>Gets or sets the value of Date on this Email queue item instance.</summary>
        public DateTime Date { get; set; }
        
        /// <summary>Gets or sets a value indicating whether this Email queue item instance Enable ssl.</summary>
        public bool EnableSsl { get; set; }
        
        /// <summary>Gets or sets a value indicating whether this Email queue item instance is Html.</summary>
        public bool Html { get; set; }
        
        /// <summary>Gets or sets the value of Password on this Email queue item instance.</summary>
        public string Password { get; set; }
        
        /// <summary>Gets or sets the value of Retries on this Email queue item instance.</summary>
        public int Retries { get; set; }
        
        /// <summary>Gets or sets the value of SenderAddress on this Email queue item instance.</summary>
        public string SenderAddress { get; set; }
        
        /// <summary>Gets or sets the value of SenderName on this Email queue item instance.</summary>
        public string SenderName { get; set; }
        
        /// <summary>Gets or sets the value of SmtpHost on this Email queue item instance.</summary>
        public string SmtpHost { get; set; }
        
        /// <summary>Gets or sets the value of SmtpPort on this Email queue item instance.</summary>
        public int? SmtpPort { get; set; }
        
        /// <summary>Gets or sets the value of Subject on this Email queue item instance.</summary>
        public string Subject { get; set; }
        
        /// <summary>Gets or sets the value of To on this Email queue item instance.</summary>
        public string To { get; set; }
        
        /// <summary>Gets or sets the value of Username on this Email queue item instance.</summary>
        public string Username { get; set; }
        
        /// <summary>Gets or sets the value of VCalendarView on this Email queue item instance.</summary>
        [System.ComponentModel.DisplayName("VCalendar view")]
        public string VCalendarView { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a textual representation of this Email queue item.</summary>
        /// <returns>A string value that represents this Email queue item instance.</returns>
        public override string ToString() => Subject;
        
        /// <summary>Returns a clone of this Email queue item.</summary>
        /// <returns>
        /// A new Email queue item object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new EmailQueueItem Clone() => (EmailQueueItem)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Email queue item.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override Task ValidateProperties()
        {
            if (Attachments?.Length > 200)
                throw new ValidationException("The provided Attachments is too long. A maximum of 200 characters is acceptable.");
            
            if (Bcc?.Length > 200)
                throw new ValidationException("The provided Bcc is too long. A maximum of 200 characters is acceptable.");
            
            if (Category?.Length > 200)
                throw new ValidationException("The provided Category is too long. A maximum of 200 characters is acceptable.");
            
            if (Cc?.Length > 200)
                throw new ValidationException("The provided Cc is too long. A maximum of 200 characters is acceptable.");
            
            if (Password?.Length > 200)
                throw new ValidationException("The provided Password is too long. A maximum of 200 characters is acceptable.");
            
            if (Retries < 0)
                throw new ValidationException("The value of Retries must be 0 or more.");
            
            if (SenderAddress?.Length > 200)
                throw new ValidationException("The provided Sender address is too long. A maximum of 200 characters is acceptable.");
            
            if (SenderName?.Length > 200)
                throw new ValidationException("The provided Sender name is too long. A maximum of 200 characters is acceptable.");
            
            if (SmtpHost?.Length > 200)
                throw new ValidationException("The provided Smtp host is too long. A maximum of 200 characters is acceptable.");
            
            if (SmtpPort < 0)
                throw new ValidationException("The value of Smtp port must be 0 or more.");
            
            if (Subject.IsEmpty())
                throw new ValidationException("Subject cannot be empty.");
            
            if (Subject?.Length > 200)
                throw new ValidationException("The provided Subject is too long. A maximum of 200 characters is acceptable.");
            
            if (To?.Length > 200)
                throw new ValidationException("The provided To is too long. A maximum of 200 characters is acceptable.");
            
            if (Username?.Length > 200)
                throw new ValidationException("The provided Username is too long. A maximum of 200 characters is acceptable.");
            
            if (VCalendarView?.Length > 200)
                throw new ValidationException("The provided VCalendar view is too long. A maximum of 200 characters is acceptable.");
            
            return Task.CompletedTask;
        }
    }
}