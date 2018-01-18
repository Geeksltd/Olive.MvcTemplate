namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
    /// <summary>Represents an instance of Administrator entity type.</summary>
    public partial class Administrator : User
    {
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of ImpersonationToken on this Administrator instance.</summary>
        public string ImpersonationToken { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a clone of this Administrator.</summary>
        /// <returns>
        /// A new Administrator object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new Administrator Clone()
        {
            return (Administrator)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this Administrator.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            try
            {
                base.ValidateProperties();
            }
            catch (ValidationException ex)
            {
                result.Add(ex.Message);
            }
            
            // Validate ImpersonationToken property:
            
            if (this.ImpersonationToken != null && this.ImpersonationToken.Length > 40)
            {
                result.Add("The provided Impersonation token is too long. A maximum of 40 characters is acceptable.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
    }
}