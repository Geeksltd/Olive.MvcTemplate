namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    
    /// <summary>Represents an instance of Application event entity type.</summary>
    [CacheObjects(false)]
    public partial class ApplicationEvent : GuidEntity, IApplicationEvent
    {
        /* -------------------------- Constructor -----------------------*/
        
        /// <summary>Initializes a new instance of the ApplicationEvent class.</summary>
        public ApplicationEvent()
        {
            this.Date = LocalTime.Now;
        }
        
        /* -------------------------- Properties -------------------------*/
        
        /// <summary>Gets or sets the value of Data on this Application event instance.</summary>
        public string Data { get; set; }
        
        /// <summary>Gets or sets the value of Date on this Application event instance.</summary>
        public DateTime Date { get; set; }
        
        /// <summary>Gets or sets the value of Event on this Application event instance.</summary>
        public string Event { get; set; }
        
        /// <summary>Gets or sets the value of IP on this Application event instance.</summary>
        public string IP { get; set; }
        
        /// <summary>Gets or sets the value of ItemKey on this Application event instance.</summary>
        public string ItemKey { get; set; }
        
        /// <summary>Gets or sets the value of ItemType on this Application event instance.</summary>
        public string ItemType { get; set; }
        
        /// <summary>Gets or sets the value of UserId on this Application event instance.</summary>
        public string UserId { get; set; }
        
        /* -------------------------- Methods ----------------------------*/
        
        /// <summary>Returns a textual representation of this Application event.</summary>
        /// <returns>A string value that represents this Application event instance.</returns>
        public override string ToString()
        {
            return this.Event;
        }
        
        /// <summary>Returns a clone of this Application event.</summary>
        /// <returns>
        /// A new Application event object with the same ID of this instance and identical property values.<para/>
        ///  The difference is that this instance will be unlocked, and thus can be used for updating in database.<para/>
        /// </returns>
        public new ApplicationEvent Clone()
        {
            return (ApplicationEvent)base.Clone();
        }
        
        /// <summary>
        /// Validates the data for the properties of this Application event.<para/>
        /// It throws a ValidationException if an error is detected.<para/>
        /// </summary>
        protected override void ValidateProperties()
        {
            var result = new List<string>();
            
            // Validate Date property:
            
            if (this.Date < System.Data.SqlTypes.SqlDateTime.MinValue.Value)
            {
                result.Add("The specified Date is invalid.");
            }
            
            // Validate Event property:
            
            if (this.Event.IsEmpty())
            {
                result.Add("Event cannot be empty.");
            }
            
            if (this.Event != null && this.Event.Length > 200)
            {
                result.Add("The provided Event is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate IP property:
            
            if (this.IP != null && this.IP.Length > 200)
            {
                result.Add("The provided IP is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate ItemKey property:
            
            if (this.ItemKey != null && this.ItemKey.Length > 500)
            {
                result.Add("The provided Item key is too long. A maximum of 500 characters is acceptable.");
            }
            
            // Validate ItemType property:
            
            if (this.ItemType != null && this.ItemType.Length > 200)
            {
                result.Add("The provided Item type is too long. A maximum of 200 characters is acceptable.");
            }
            
            // Validate UserId property:
            
            if (this.UserId != null && this.UserId.Length > 200)
            {
                result.Add("The provided User id is too long. A maximum of 200 characters is acceptable.");
            }
            
            if (result.Any())
                throw new ValidationException(result.ToLinesString());
        }
    }
}