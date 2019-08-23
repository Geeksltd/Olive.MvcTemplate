namespace Website
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    using Domain;
    
    /// <summary>Executes the scheduled tasks in independent threads automatically.</summary>
    [EscapeGCop("Auto generated code.")]
    #pragma warning disable
    public partial class TaskManager : BackgroundJobsPlan
    {
        /// <summary>Registers the scheduled activities.</summary>
        public override void Initialize()
        {
            Register(new BackgroundJob("Clean old temp uploads", () => CleanOldTempUploads(), Hangfire.Cron.MinuteInterval(10)));
        }
        
        /// <summary>Clean old temp uploads</summary>
        public static async Task CleanOldTempUploads()
        {
            await Olive.Mvc.FileUploadService.DeleteTempFiles(olderThan: 1.Hours());
        }
    }
}