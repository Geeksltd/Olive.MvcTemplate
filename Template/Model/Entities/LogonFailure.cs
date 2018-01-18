namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
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
        public override string ToString()
        {
            return this.Email;
        }
        
        /// <summary>Returns a clone of this Logon failure.</summary>
        /// <returns>
        /// A new Logon failure object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new LogonFailure Clone()
        {
            return (LogonFailure)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this Logon failure.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            // Validate Attempts property:
            
            if (this.Attempts < 0)
            {
                result.Add("The value of Attempts must be 0 or more.");
            }
            
            // Validate Date property:
            
            if (this.Date < System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                result.Add("The specified Date is invalid.");
            }
            
            // Validate Email property:
            
            if (this.Email.IsEmpty())
            {
                result.Add("Email cannot be empty.");
            }
            
            if (this.Email != null && this.Email.Length > 200)
            {
                result.Add("The provided Email is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate IP property:
            
            if (this.IP.IsEmpty())
            {
                result.Add("IP cannot be empty.");
            }
            
            if (this.IP != null && this.IP.Length > 200)
            {
                result.Add("The provided IP is too long. A maximum of 200 characters is acceptable.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
    }
}