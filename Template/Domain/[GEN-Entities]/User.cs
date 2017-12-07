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
    
    /// <summary>Represents an instance of User entity type.</summary>
    public abstract partial class User : GuidEntity
    {
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the User class.</summary>
        protected User() => Deleting.Handle(Cascade_Deleting);
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of Email on this User instance.</summary>
        public string Email { get; set; }
        
        /// <summary>Gets or sets the value of FirstName on this User instance.</summary>
        public string FirstName { get; set; }
        
        /// <summary>Gets or sets a value indicating whether this User instance Is deactivated.</summary>
        public bool IsDeactivated { get; set; }
        
        /// <summary>Gets or sets the value of LastName on this User instance.</summary>
        public string LastName { get; set; }
        
        /// <summary>Gets the Name property.</summary>
        [Calculated]
        [Newtonsoft.Json.JsonIgnore]
        public string Name
        {
            get => FirstName + " " + LastName;
        }
        
        /// <summary>Gets or sets the value of Password on this User instance.</summary>
        public string Password { get; set; }
        
        /// <summary>Gets or sets the value of Salt on this User instance.</summary>
        public string Salt { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        /// <summary>
        /// Find and returns an instance of User from the database by its Email.<para/>
        ///                               If no matching User is found, it returns Null.<para/>
        /// </summary>
        /// <param name="email">The Email of the requested User.</param>
        /// <returns>
        /// The User instance with the specified Email or null if there is no User with that Email in the database.<para/>
        /// </returns>
        public static Task<User> FindByEmail(string email)
        {
            return Database.FirstOrDefault<User>(u => u.Email == email);
        }
        
        /// <summary>Returns a textual representation of this User.</summary>
        /// <returns>A string value that represents this User instance.</returns>
        public override string ToString() => FirstName;
        
        /// <summary>Returns a clone of this User.</summary>
        /// <returns>
        /// A new User object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new User Clone() => (User)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this User.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override async Task ValidateProperties()
        {
            if (Email.IsEmpty())
                throw new ValidationException("Email cannot be empty.");
            
            if (Email?.Length > 100)
                throw new ValidationException("The provided Email is too long. A maximum of 100 characters is acceptable.");
            
            // Ensure Email matches Email address pattern:
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(Email, "\\s*\\w+([-+.'\\w])*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*\\s*"))
                throw new ValidationException("The provided Email is not a valid Email address.");
            
            // Ensure uniqueness of Email.
            
            if (await Database.Any<User>(u => u.Email == Email && u != this))
                throw new ValidationException("Email must be unique. There is an existing User record with the provided Email.");
            
            if (FirstName.IsEmpty())
                throw new ValidationException("First name cannot be empty.");
            
            if (FirstName?.Length > 200)
                throw new ValidationException("The provided First name is too long. A maximum of 200 characters is acceptable.");
            
            if (LastName.IsEmpty())
                throw new ValidationException("Last name cannot be empty.");
            
            if (LastName?.Length > 200)
                throw new ValidationException("The provided Last name is too long. A maximum of 200 characters is acceptable.");
            
            if (Password?.Length > 100)
                throw new ValidationException("The provided Password is too long. A maximum of 100 characters is acceptable.");
            
            if (Salt?.Length > 200)
                throw new ValidationException("The provided Salt is too long. A maximum of 200 characters is acceptable.");
        }
        
        /// <summary>Handles the Deleting event of this User.</summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The CancelEventArgs instance containing the event data.</param>
        async Task Cascade_Deleting()
        {
            // Cascade delete the dependant Password reset tickets:
            await Database.DeleteAll<PasswordResetTicket>(p => p.User == this);
        }
    }
}