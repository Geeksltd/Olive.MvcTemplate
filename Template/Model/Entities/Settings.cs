namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
    /// <summary>Represents an instance of Settings entity type.</summary>
    [SmallTable]
    public partial class Settings : GuidEntity
    {
        /* -------------------------- Fields -------------------------*/
        
        /// <summary>Stores a cache for the Current Settings object.</summary>
        private static Settings current;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets the Current Settings object.</summary>
        public static Settings Current
        {
            get
            {
                var result = current;
                
                if (result == null)
                {
                    result = Parse("Current");
                    
                    if (result != null && !Database.AnyOpenTransaction())
                    {
                        current = result;
                        
                        result.Saving += (o, e) => current = null;;
                        result.Saved += (o, e) => current = null;;
                        Database.CacheRefreshed += (o, e) => current = null;;
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
        public static Settings Parse(string text)
        {
            if (text.IsEmpty())
            {
                throw new ArgumentNullException(nameof(text));
            }
            
            return Database.Find<Settings>(s => s.Name == text);
        }
        
        /// <summary>Returns a textual representation of this Settings.</summary>
        /// <returns>A string value that represents this Settings instance.</returns>
        public override string ToString()
        {
            return this.Name;
        }
        
        /// <summary>Returns a clone of this Settings.</summary>
        /// <returns>
        /// A new Settings object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new Settings Clone()
        {
            return (Settings)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this Settings.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            // Validate CacheVersion property:
            
            if (this.CacheVersion < 0)
            {
                result.Add("The value of Cache version must be 0 or more.");
            }
            
            // Validate Name property:
            
            if (this.Name.IsEmpty())
            {
                result.Add("Name cannot be empty.");
            }
            
            if (this.Name != null && this.Name.Length > 200)
            {
                result.Add("The provided Name is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate PasswordResetTicketExpiryMinutes property:
            
            if (this.PasswordResetTicketExpiryMinutes < 0)
            {
                result.Add("The value of Password reset ticket expiry minutes must be 0 or more.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
    }
}