namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
    /// <summary>Represents an instance of User entity type.</summary>
    public abstract partial class User : GuidEntity
    {
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the User class.</summary>
        protected User()
        {
            this.Deleting += this.Cascade_Deleting;
        }
        
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
            get
            {
                return FirstName + " " + LastName;
            }
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
        public static User FindByEmail(string email)
        {
            return Database.Find<User>(u => u.Email == email);
        }
        
        /// <summary>Returns a textual representation of this User.</summary>
        /// <returns>A string value that represents this User instance.</returns>
        public override string ToString()
        {
            return this.FirstName;
        }
        
        /// <summary>Returns a clone of this User.</summary>
        /// <returns>
        /// A new User object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new User Clone()
        {
            return (User)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this User.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            // Validate Email property:
            
            if (this.Email.IsEmpty())
            {
                result.Add("Email cannot be empty.");
            }
            
            if (this.Email != null && this.Email.Length > 100)
            {
                result.Add("The provided Email is too long. A maximum of 100 characters is acceptable.");
            }
            
            // Ensure Email matches Email address pattern:
            
            if (this.Email.HasValue() && !System.Text.RegularExpressions.Regex.IsMatch(this.Email, "\\s*\\w+([-+.'\\w])*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*\\s*"))
            {
                result.Add("The provided Email is not a valid Email address.");
            }
            
            // Ensure uniqueness of Email.
            
            if (Database.Any<User>(u => u.Email == Email && u != this))
            {
                result.Add("Email must be unique. There is an existing User record with the provided Email.");
            }
            
            // Validate FirstName property:
            
            if (this.FirstName.IsEmpty())
            {
                result.Add("First name cannot be empty.");
            }
            
            if (this.FirstName != null && this.FirstName.Length > 200)
            {
                result.Add("The provided First name is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate LastName property:
            
            if (this.LastName.IsEmpty())
            {
                result.Add("Last name cannot be empty.");
            }
            
            if (this.LastName != null && this.LastName.Length > 200)
            {
                result.Add("The provided Last name is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate Password property:
            
            if (this.Password != null && this.Password.Length > 100)
            {
                result.Add("The provided Password is too long. A maximum of 100 characters is acceptable.");
            }
            
            // Validate Salt property:
            
            if (this.Salt != null && this.Salt.Length > 200)
            {
                result.Add("The provided Salt is too long. A maximum of 200 characters is acceptable.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
        
        /// <summary>Handles the Deleting event of this User.</summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The CancelEventArgs instance containing the event data.</param>
        private void Cascade_Deleting(object source, System.ComponentModel.CancelEventArgs e)
        {
            // Cascade delete the dependant Password reset tickets:
            Database.DeleteAll<PasswordResetTicket>(p => p.User == this);
        }
    }
}