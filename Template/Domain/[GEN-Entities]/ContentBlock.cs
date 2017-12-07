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
    
    /// <summary>Represents an instance of Content block entity type.</summary>
    public partial class ContentBlock : GuidEntity
    {
        /// <summary>Stores a cache for the PasswordSuccessfullyReset Content block object.</summary>
        static ContentBlock passwordSuccessfullyReset;
        
        /// <summary>Stores a cache for the LoginIntro Content block object.</summary>
        static ContentBlock loginIntro;
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets the PasswordSuccessfullyReset Content block object.</summary>
        public static ContentBlock PasswordSuccessfullyReset
        {
            get
            {
                var result = passwordSuccessfullyReset;
                
                if (result == null)
                {
                    result = Task.Factory.RunSync(() =>Parse("PasswordSuccessfullyReset"));
                    
                    if (result != null && !Database.AnyOpenTransaction())
                    {
                        passwordSuccessfullyReset = result;
                        
                        void release() => passwordSuccessfullyReset = null;
                        
                        result.Saving.HandleWith(release);
                        result.Saved.HandleWith(release);
                        Database.CacheRefreshed.HandleWith(release);
                    }
                }
                
                return result;
            }
        }
        
        /// <summary>Gets the LoginIntro Content block object.</summary>
        public static ContentBlock LoginIntro
        {
            get
            {
                var result = loginIntro;
                
                if (result == null)
                {
                    result = Task.Factory.RunSync(() =>Parse("LoginIntro"));
                    
                    if (result != null && !Database.AnyOpenTransaction())
                    {
                        loginIntro = result;
                        
                        void release() => loginIntro = null;
                        
                        result.Saving.HandleWith(release);
                        result.Saved.HandleWith(release);
                        Database.CacheRefreshed.HandleWith(release);
                    }
                }
                
                return result;
            }
        }
        
        /// <summary>Gets or sets the value of Content on this Content block instance.</summary>
        public string Content { get; set; }
        
        /// <summary>Gets or sets the value of Key on this Content block instance.</summary>
        public string Key { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        /// <summary>
        /// Find and returns an instance of Content block from the database by its Key.<para/>
        ///                               If no matching Content block is found, it returns Null.<para/>
        /// </summary>
        /// <param name="key">The Key of the requested Content block.</param>
        /// <returns>
        /// The Content block instance with the specified Key or null if there is no Content block with that Key in the database.<para/>
        /// </returns>
        public static Task<ContentBlock> FindByKey(string key)
        {
            return Database.FirstOrDefault<ContentBlock>(c => c.Key == key);
        }
        
        /// <summary>
        /// Returns the Content block instance that is textually represented with a specified string value, or null if no such object is found.<para/>
        /// </summary>
        /// <param name="text">The text representing the Content block to be retrieved from the database.</param>
        /// <returns>The Content block object whose string representation matches the specified text.</returns>
        public static Task<ContentBlock> Parse(string text)
        {
            if (text.IsEmpty())
                throw new ArgumentNullException(nameof(text));
            
            return Database.FirstOrDefault<ContentBlock>(c => c.Key == text);
        }
        
        /// <summary>Returns a textual representation of this Content block.</summary>
        /// <returns>A string value that represents this Content block instance.</returns>
        public override string ToString() => Key;
        
        /// <summary>Returns a clone of this Content block.</summary>
        /// <returns>
        /// A new Content block object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new ContentBlock Clone() => (ContentBlock)base.Clone();
        
        /// <summary>
        /// Validates the data for the properties of this Content block.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override async Task ValidateProperties()
        {
            if (Content.IsEmpty())
                throw new ValidationException("Content cannot be empty.");
            
            if (Key.IsEmpty())
                throw new ValidationException("Key cannot be empty.");
            
            if (Key?.Length > 200)
                throw new ValidationException("The provided Key is too long. A maximum of 200 characters is acceptable.");
            
            // Ensure uniqueness of Key.
            
            if (await Database.Any<ContentBlock>(c => c.Key == Key && c != this))
                throw new ValidationException("Key must be unique. There is an existing Content block record with the provided Key.");
        }
    }
}