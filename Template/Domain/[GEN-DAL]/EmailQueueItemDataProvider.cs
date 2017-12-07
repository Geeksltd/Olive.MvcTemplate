namespace AppData
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    
    /// <summary>Provides data-access facilities for Email queue items.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EmailQueueItemDataProvider : SqlDataProvider<Domain.EmailQueueItem>
    {
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Email queue item record</summary>
        
        /// <summary>Gets the list of fields to use for loading Email queue items.</summary>
        public override string GetFields()
        {
            return @"E.[ID] AS [EmailQueueItems_Id],
            E.[.Deleted] AS [EmailQueueItems__SoftDeleted],
            E.[Body] AS [EmailQueueItems_Body],
            E.[Date] AS [EmailQueueItems_Date],
            E.[EnableSsl] AS [EmailQueueItems_EnableSsl],
            E.[Html] AS [EmailQueueItems_Html],
            E.[SenderAddress] AS [EmailQueueItems_SenderAddress],
            E.[SenderName] AS [EmailQueueItems_SenderName],
            E.[Subject] AS [EmailQueueItems_Subject],
            E.[To] AS [EmailQueueItems_To],
            E.[Attachments] AS [EmailQueueItems_Attachments],
            E.[Bcc] AS [EmailQueueItems_Bcc],
            E.[Cc] AS [EmailQueueItems_Cc],
            E.[Retries] AS [EmailQueueItems_Retries],
            E.[VCalendarView] AS [EmailQueueItems_VCalendarView],
            E.[Username] AS [EmailQueueItems_Username],
            E.[Password] AS [EmailQueueItems_Password],
            E.[SmtpHost] AS [EmailQueueItems_SmtpHost],
            E.[SmtpPort] AS [EmailQueueItems_SmtpPort],
            E.[Category] AS [EmailQueueItems_Category]";
        }
        
        /// <summary>Provides the data source expression for querying Email queue item records.</summary>
        public override string GetTables()
        {
            return @"[EmailQueueItems] AS E";
        }
        
        /// <summary>Gets a SQL command text to insert a record into EmailQueueItems table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO [EmailQueueItems]
        ([Id], [Body], [Date], [EnableSsl], [Html], [SenderAddress], [SenderName], [Subject], [To], [Attachments], [Bcc], [Cc], [Retries], [VCalendarView], [Username], [Password], [SmtpHost], [SmtpPort], [Category], [.DELETED])
        VALUES
        (@Id, @Body, @Date, @EnableSsl, @Html, @SenderAddress, @SenderName, @Subject, @To, @Attachments, @Bcc, @Cc, @Retries, @VCalendarView, @Username, @Password, @SmtpHost, @SmtpPort, @Category, @_DELETED)";
        
        /// <summary>Gets a SQL command text to update a record in EmailQueueItems table.</summary>
        const string UPDATE_COMMAND = @"UPDATE [EmailQueueItems] SET
        [Id] = @Id,
        [Body] = @Body,
        [Date] = @Date,
        [EnableSsl] = @EnableSsl,
        [Html] = @Html,
        [SenderAddress] = @SenderAddress,
        [SenderName] = @SenderName,
        [Subject] = @Subject,
        [To] = @To,
        [Attachments] = @Attachments,
        [Bcc] = @Bcc,
        [Cc] = @Cc,
        [Retries] = @Retries,
        [VCalendarView] = @VCalendarView,
        [Username] = @Username,
        [Password] = @Password,
        [SmtpHost] = @SmtpHost,
        [SmtpPort] = @SmtpPort,
        [Category] = @Category,
        [.DELETED] = @_DELETED
        OUTPUT [INSERTED].[ID]
        WHERE [ID] = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from EmailQueueItems table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM [EmailQueueItems] WHERE [ID] = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified EmailQueueItem property.</summary>
        public override string MapColumn(string propertyName)
        {
            if (propertyName == "IsMarkedSoftDeleted") return "E.[.DELETED]";
            else return $"E.[{propertyName}]";
        }
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Email queue item instance from the database.<para/>
        /// </summary>
        public override Task<IEnumerable<string>> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Email queue item instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.EmailQueueItem();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Email queue item instance.</summary>
        internal void FillData(IDataReader reader, Domain.EmailQueueItem entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            if ((bool)values[1]) // Soft Deleted
            {
                EntityManager.MarkSoftDeleted(entity);
            }
            
            entity.Body = values[Fields.Body] as string;
            entity.Date = (DateTime)values[Fields.Date];
            entity.EnableSsl = (bool)values[Fields.EnableSsl];
            entity.Html = (bool)values[Fields.Html];
            entity.SenderAddress = values[Fields.SenderAddress] as string;
            entity.SenderName = values[Fields.SenderName] as string;
            entity.Subject = values[Fields.Subject] as string;
            entity.To = values[Fields.To] as string;
            entity.Attachments = values[Fields.Attachments] as string;
            entity.Bcc = values[Fields.Bcc] as string;
            entity.Cc = values[Fields.Cc] as string;
            entity.Retries = (int)values[Fields.Retries];
            entity.VCalendarView = values[Fields.VCalendarView] as string;
            entity.Username = values[Fields.Username] as string;
            entity.Password = values[Fields.Password] as string;
            entity.SmtpHost = values[Fields.SmtpHost] as string;
            
            if (values[Fields.SmtpPort] != DBNull.Value) entity.SmtpPort = (int)values[Fields.SmtpPort];
            entity.Category = values[Fields.Category] as string;
        }
        
        /// <summary>Saves the specified Email queue item instance in the database.</summary>
        public override async Task Save(IEntity record)
        {
            var item = record as Domain.EmailQueueItem;
            
            if (record.IsNew)
                await Insert(item);
            else
                await Update(item);
        }
        
        /// <summary>Inserts the specified new Email queue item instance into the database.</summary>
        async Task Insert(Domain.EmailQueueItem item)
        {
            await ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Email queue items into the database.</summary>
        public override async Task BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.EmailQueueItem>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            await Access.ExecuteBulkNonQueries(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Email queue item instance in the database.</summary>
        async Task Update(Domain.EmailQueueItem item)
        {
            if ((await ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item))).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'EmailQueueItems' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Email queue item records</summary>
        IDataParameter[] CreateParameters(Domain.EmailQueueItem item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("Body", item.Body));
            result.Add(CreateParameter("Date", item.Date, DbType.DateTime2));
            result.Add(CreateParameter("EnableSsl", item.EnableSsl));
            result.Add(CreateParameter("Html", item.Html));
            result.Add(CreateParameter("SenderAddress", item.SenderAddress));
            result.Add(CreateParameter("SenderName", item.SenderName));
            result.Add(CreateParameter("Subject", item.Subject));
            result.Add(CreateParameter("To", item.To));
            result.Add(CreateParameter("Attachments", item.Attachments));
            result.Add(CreateParameter("Bcc", item.Bcc));
            result.Add(CreateParameter("Cc", item.Cc));
            result.Add(CreateParameter("Retries", item.Retries));
            result.Add(CreateParameter("VCalendarView", item.VCalendarView));
            result.Add(CreateParameter("Username", item.Username));
            result.Add(CreateParameter("Password", item.Password));
            result.Add(CreateParameter("SmtpHost", item.SmtpHost));
            result.Add(CreateParameter("SmtpPort", item.SmtpPort));
            result.Add(CreateParameter("Category", item.Category));
            result.Add(CreateParameter("_DELETED", item.IsMarkedSoftDeleted));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Email queue item instance from the database.</summary>
        public override async Task Delete(IEntity record)
        {
            await ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
        
        static class Fields
        {
            public const int EmailQueueItems__SoftDeleted = 1;
            public const int Body = 2;
            public const int Date = 3;
            public const int EnableSsl = 4;
            public const int Html = 5;
            public const int SenderAddress = 6;
            public const int SenderName = 7;
            public const int Subject = 8;
            public const int To = 9;
            public const int Attachments = 10;
            public const int Bcc = 11;
            public const int Cc = 12;
            public const int Retries = 13;
            public const int VCalendarView = 14;
            public const int Username = 15;
            public const int Password = 16;
            public const int SmtpHost = 17;
            public const int SmtpPort = 18;
            public const int Category = 19;
        }
    }
}