namespace Website
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;
    using System.Web;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    using Olive.Mvc;
    using Olive.Services;
    using Olive.Web;
    using Domain;
    using Hangfire;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using vm = ViewModel;
    
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
            
            RecurringJob.AddOrUpdate("Send email queue items",() => SendEmailQueueItems(), Cron.MinuteInterval(1));
        }
        
        /// <summary>Clean old temp uploads</summary>
        static async Task CleanOldTempUploads()
        {
            await FileUploadService.DeleteTempFiles(olderThan: 1.Hours());
        }
        
        /// <summary>Send email queue items</summary>
        static async Task SendEmailQueueItems()
        {
            await Olive.Services.Email.EmailService.SendAll();
        }
    }
}