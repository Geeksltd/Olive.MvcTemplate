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
    
    /// <summary>Represents an instance of Settings entity type.</summary>
    public partial class Settings : GuidEntity
    {
        /// <summary>Stores a cache for the Current Settings object.</summary>
        static Settings current;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets the Current Settings object.</summary>
        public static Settings Current
        {
            get
            {
                var result = current;
                
                if (result == null)
                {
                    result = Task.Factory.RunSync(() =>Parse("Current"));
                    
                    if (result != null && !Database.AnyOpenTransaction())
                    {
                        current = result;
                        
                        void release() => current = null;
                        
                        result.Saving.HandleWith(release);
                        result.Saved.HandleWith(release);
                        Database.CacheRefreshed.HandleWith(release);
                    }
                }
                
                return result;
            }
        }
        
        /// <summary>Gets or sets the value of CacheVersion on this Settings instance.</summary>
        public int CacheVersion { get; set; }
        
        /// <summary>Gets or sets the value of Name on this Settings instance.</summary>
        public string Name { get; set; }
        
        /// <summary>Gets or sets the value of PasswordResetTicketExpiryMinutes on this Settings instance.</summary>
        public int PasswordResetTicketExpiryMinutes { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>
        /// Returns the Settings instance that is textually represented with a specified string value, or null if no such object is found.<para/>
        /// </summary>
        /// <param name="text">The text representing the Settings to be retrieved from the database.</param>
        /// <returns>The Settings object whose string representation matches the specified text.</returns>
        public static Task<Settings> Parse(string text)
        {
            if (text.IsEmpty())
                throw new ArgumentNullException(nameof(text));
            
            return Database.FirstOrDefault<Settings>(s => s.Name == text);
        }
        
        /// <summary>Returns a textual representation of this Settings.</summary>
        /// <returns>A string value that represents this Settings instance.</returns>
        public override string ToString() => Name;
        
        /// <summary>Returns a clone of this Settings.</summary>
        /// <returns>
        /// A new Settings object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new Settings Clone() => (Settings)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Settings.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override Task ValidateProperties()
        {
            if (CacheVersion < 0)
                throw new ValidationException("The value of Cache version must be 0 or more.");
            
            if (Name.IsEmpty())
                throw new ValidationException("Name cannot be empty.");
            
            if (Name?.Length > 200)
                throw new ValidationException("The provided Name is too long. A maximum of 200 characters is acceptable.");
            
            if (PasswordResetTicketExpiryMinutes < 0)
                throw new ValidationException("The value of Password reset ticket expiry minutes must be 0 or more.");
            
            return Task.CompletedTask;
        }
    }
}