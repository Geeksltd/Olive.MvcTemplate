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
    
    /// <summary>Represents an instance of Password reset ticket entity type.</summary>
    public partial class PasswordResetTicket : GuidEntity
    {
        CachedReference<User> cachedUser = new CachedReference<User>();
        
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the PasswordResetTicket class.</summary>
        public PasswordResetTicket() => DateCreated = LocalTime.Now;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of DateCreated on this Password reset ticket instance.</summary>
        public DateTime DateCreated { get; set; }
        
        /// <summary>Gets the IsExpired property.</summary>
        [Calculated]
        [Newtonsoft.Json.JsonIgnore]
        public bool IsExpired
        {
            get => LocalTime.Now >= DateCreated.AddMinutes(Settings.Current.PasswordResetTicketExpiryMinutes);
        }
        
        /// <summary>Gets or sets a value indicating whether this Password reset ticket instance Is used.</summary>
        public bool IsUsed { get; set; }
        
        #region User Association
        
        /// <summary>Gets or sets the ID of the associated User.</summary>
        public Guid? UserId { get; set; }
        
        /// <summary>Gets or sets the value of User on this Password reset ticket instance.</summary>
        public User User
        {
            get => cachedUser.Get(UserId).AwaitResult();
            set => UserId = value?.ID;
        }
        
        #endregion
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a textual representation of this Password reset ticket.</summary>
        /// <returns>A string value that represents this Password reset ticket instance.</returns>
        public override string ToString() => $"Password reset ticket ({ID})";
        
        /// <summary>Returns a clone of this Password reset ticket.</summary>
        /// <returns>
        /// A new Password reset ticket object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new PasswordResetTicket Clone() => (PasswordResetTicket)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Password reset ticket.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override Task ValidateProperties()
        {
            if (UserId == null)
                throw new ValidationException("Please provide a value for User.");
            
            return Task.CompletedTask;
        }
    }
}