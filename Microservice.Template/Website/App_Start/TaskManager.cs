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
    using Olive.Services;
    using Hangfire;
    
    /// <summary>Executes the scheduled tasks in independent threads automatically.</summary>
    public static partial class TaskManager
    {
        /// <summary>
        /// This will start the scheduled activities.<para/>
        /// It should be called once in Application_Start global event.<para/>
        /// </summary>
        public static void Run()
        {
            RecurringJob.AddOrUpdate("Clean old temp uploads",() => CleanOldTempUploads(), Cron.MinuteInterval(10));
        }
        
        /// <summary>Clean old temp uploads</summary>
        public static async Task CleanOldTempUploads()
        {
            await Olive.Mvc.FileUploadService.DeleteTempFiles(olderThan: 1.Hours());
        }
    }
}