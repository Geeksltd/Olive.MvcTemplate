namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
    /// <summary>Represents an instance of Password reset ticket entity type.</summary>
    public partial class PasswordResetTicket : GuidEntity
    {
        /* -------------------------- Fields -------------------------*/
        
        private CachedReference<User> cachedUser = new CachedReference<User>();
        
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the PasswordResetTicket class.</summary>
        public PasswordResetTicket()
        {
            this.DateCreated = LocalTime.Now;
        }
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of DateCreated on this Password reset ticket instance.</summary>
        public DateTime DateCreated { get; set; }
        
        /// <summary>Gets the IsExpired property.</summary>
        [Calculated]
        [Newtonsoft.Json.JsonIgnore]
        public bool IsExpired
        {
            get
            {
                return LocalTime.Now >= DateCreated.AddMinutes(Settings.Current.PasswordResetTicketExpiryMinutes);
            }
        }
        
        /// <summary>Gets or sets a value indicating whether this Password reset ticket instance Is used.</summary>
        public bool IsUsed { get; set; }
        
        #region User Association
        
        /// <summary>Gets or sets the ID of the associated User.</summary>
        public Guid? UserId { get; set; }
        
        /// <summary>Gets or sets the value of User on this Password reset ticket instance.</summary>
        public User User
        {
            get
            {
                return this.cachedUser.Get(UserId);
            }
            
            set
            {
                this.UserId = value.Get(u => u.ID);
            }
        }
        
        #endregion
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a textual representation of this Password reset ticket.</summary>
        /// <returns>A string value that represents this Password reset ticket instance.</returns>
        public override string ToString()
        {
            return string.Format("Password reset ticket ({0})", this.ID);
        }
        
        /// <summary>Returns a clone of this Password reset ticket.</summary>
        /// <returns>
        /// A new Password reset ticket object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new PasswordResetTicket Clone()
        {
            return (PasswordResetTicket)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this Password reset ticket.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            // Validate DateCreated property:
            
            if (this.DateCreated < System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                result.Add("The specified Date created is invalid.");
            }
            
            // Validate User property:
            
            if (this.UserId == null)
            {
                result.Add("Please provide a value for User.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
    }
}