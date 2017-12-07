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
    
    /// <summary>Represents an instance of Logon failure entity type.</summary>
    public partial class LogonFailure : GuidEntity
    {
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of Attempts on this Logon failure instance.</summary>
        public int Attempts { get; set; }
        
        /// <summary>Gets or sets the value of Date on this Logon failure instance.</summary>
        public DateTime Date { get; set; }
        
        /// <summary>Gets or sets the value of Email on this Logon failure instance.</summary>
        public string Email { get; set; }
        
        /// <summary>Gets or sets the value of IP on this Logon failure instance.</summary>
        public string IP { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a textual representation of this Logon failure.</summary>
        /// <returns>A string value that represents this Logon failure instance.</returns>
        public override string ToString() => Email;
        
        /// <summary>Returns a clone of this Logon failure.</summary>
        /// <returns>
        /// A new Logon failure object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new LogonFailure Clone() => (LogonFailure)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Logon failure.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override Task ValidateProperties()
        {
            if (Attempts < 0)
                throw new ValidationException("The value of Attempts must be 0 or more.");
            
            if (Email.IsEmpty())
                throw new ValidationException("Email cannot be empty.");
            
            if (Email?.Length > 200)
                throw new ValidationException("The provided Email is too long. A maximum of 200 characters is acceptable.");
            
            if (IP.IsEmpty())
                throw new ValidationException("IP cannot be empty.");
            
            if (IP?.Length > 200)
                throw new ValidationException("The provided IP is too long. A maximum of 200 characters is acceptable.");
            
            return Task.CompletedTask;
        }
    }
}