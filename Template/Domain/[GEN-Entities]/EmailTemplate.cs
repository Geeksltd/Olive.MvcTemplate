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
    
    /// <summary>Represents an instance of Email template entity type.</summary>
    public partial class EmailTemplate : GuidEntity
    {
        /// <summary>Stores a cache for the Recover password Email template object.</summary>
        static EmailTemplate recoverPassword;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets the Recover password Email template object.</summary>
        public static EmailTemplate RecoverPassword
        {
            get
            {
                var result = recoverPassword;
                
                if (result == null)
                {
                    result = Task.Factory.RunSync(() =>Parse("Recover password"));
                    
                    if (result != null && !Database.AnyOpenTransaction())
                    {
                        recoverPassword = result;
                        
                        void release() => recoverPassword = null;
                        
                        result.Saving.HandleWith(release);
                        result.Saved.HandleWith(release);
                        Database.CacheRefreshed.HandleWith(release);
                    }
                }
                
                return result;
            }
        }
        
        /// <summary>Gets or sets the value of Body on this Email template instance.</summary>
        public string Body { get; set; }
        
        /// <summary>Gets or sets the value of Key on this Email template instance.</summary>
        public string Key { get; set; }
        
        /// <summary>Gets or sets the value of MandatoryPlaceholders on this Email template instance.</summary>
        public string MandatoryPlaceholders { get; set; }
        
        /// <summary>Gets or sets the value of Subject on this Email template instance.</summary>
        public string Subject { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        /// <summary>
        /// Find and returns an instance of Email template from the database by its Key.<para/>
        ///                               If no matching Email template is found, it returns Null.<para/>
        /// </summary>
        /// <param name="key">The Key of the requested Email template.</param>
        /// <returns>
        /// The Email template instance with the specified Key or null if there is no Email template with that Key in the database.<para/>
        /// </returns>
        public static Task<EmailTemplate> FindByKey(string key)
        {
            return Database.FirstOrDefault<EmailTemplate>(e => e.Key == key);
        }
        
        /// <summary>
        /// Returns the Email template instance that is textually represented with a specified string value, or null if no such object is found.<para/>
        /// </summary>
        /// <param name="text">The text representing the Email template to be retrieved from the database.</param>
        /// <returns>The Email template object whose string representation matches the specified text.</returns>
        public static Task<EmailTemplate> Parse(string text)
        {
            if (text.IsEmpty())
                throw new ArgumentNullException(nameof(text));
            
            return Database.FirstOrDefault<EmailTemplate>(e => e.Subject == text);
        }
        
        /// <summary>Returns a textual representation of this Email template.</summary>
        /// <returns>A string value that represents this Email template instance.</returns>
        public override string ToString() => Subject;
        
        /// <summary>Returns a clone of this Email template.</summary>
        /// <returns>
        /// A new Email template object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new EmailTemplate Clone() => (EmailTemplate)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Email template.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override async Task ValidateProperties()
        {
            if (Body.IsEmpty())
                throw new ValidationException("Body cannot be empty.");
            
            if (Key.IsEmpty())
                throw new ValidationException("Key cannot be empty.");
            
            if (Key?.Length > 200)
                throw new ValidationException("The provided Key is too long. A maximum of 200 characters is acceptable.");
            
            // Ensure uniqueness of Key.
            
            if (await Database.Any<EmailTemplate>(e => e.Key == Key && e != this))
                throw new ValidationException("Key must be unique. There is an existing Email template record with the provided Key.");
            
            if (MandatoryPlaceholders?.Length > 200)
                throw new ValidationException("The provided Mandatory placeholders is too long. A maximum of 200 characters is acceptable.");
            
            if (Subject.IsEmpty())
                throw new ValidationException("Subject cannot be empty.");
            
            if (Subject?.Length > 200)
                throw new ValidationException("The provided Subject is too long. A maximum of 200 characters is acceptable.");
        }
    }
}