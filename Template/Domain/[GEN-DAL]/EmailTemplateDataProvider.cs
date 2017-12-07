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
    
    /// <summary>Provides data-access facilities for Email templates.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class EmailTemplateDataProvider : SqlDataProvider<Domain.EmailTemplate>
    {
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Email template record</summary>
        
        /// <summary>Gets the list of fields to use for loading Email templates.</summary>
        public override string GetFields()
        {
            return @"E.[ID] AS [EmailTemplates_Id],
            E.[Subject] AS [EmailTemplates_Subject],
            E.[Key] AS [EmailTemplates_Key],
            E.[Body] AS [EmailTemplates_Body],
            E.[MandatoryPlaceholders] AS [EmailTemplates_MandatoryPlaceholders]";
        }
        
        /// <summary>Provides the data source expression for querying Email template records.</summary>
        public override string GetTables()
        {
            return @"[EmailTemplates] AS E";
        }
        
        /// <summary>Gets a SQL command text to insert a record into EmailTemplates table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO [EmailTemplates]
        ([Id], [Subject], [Key], [Body], [MandatoryPlaceholders])
        VALUES
        (@Id, @Subject, @Key, @Body, @MandatoryPlaceholders)";
        
        /// <summary>Gets a SQL command text to update a record in EmailTemplates table.</summary>
        const string UPDATE_COMMAND = @"UPDATE [EmailTemplates] SET
        [Id] = @Id,
        [Subject] = @Subject,
        [Key] = @Key,
        [Body] = @Body,
        [MandatoryPlaceholders] = @MandatoryPlaceholders
        OUTPUT [INSERTED].[ID]
        WHERE [ID] = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from EmailTemplates table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM [EmailTemplates] WHERE [ID] = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified EmailTemplate property.</summary>
        public override string MapColumn(string propertyName) => $"E.[{propertyName}]";
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Email template instance from the database.<para/>
        /// </summary>
        public override Task<IEnumerable<string>> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Email template instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.EmailTemplate();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Email template instance.</summary>
        internal void FillData(IDataReader reader, Domain.EmailTemplate entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            entity.Subject = values[Fields.Subject] as string;
            entity.Key = values[Fields.Key] as string;
            entity.Body = values[Fields.Body] as string;
            entity.MandatoryPlaceholders = values[Fields.MandatoryPlaceholders] as string;
        }
        
        /// <summary>Saves the specified Email template instance in the database.</summary>
        public override async Task Save(IEntity record)
        {
            var item = record as Domain.EmailTemplate;
            
            if (record.IsNew)
                await Insert(item);
            else
                await Update(item);
        }
        
        /// <summary>Inserts the specified new Email template instance into the database.</summary>
        async Task Insert(Domain.EmailTemplate item)
        {
            await ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Email templates into the database.</summary>
        public override async Task BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.EmailTemplate>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            await Access.ExecuteBulkNonQueries(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Email template instance in the database.</summary>
        async Task Update(Domain.EmailTemplate item)
        {
            if ((await ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item))).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'EmailTemplates' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Email template records</summary>
        IDataParameter[] CreateParameters(Domain.EmailTemplate item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("Subject", item.Subject));
            result.Add(CreateParameter("Key", item.Key));
            result.Add(CreateParameter("Body", item.Body));
            result.Add(CreateParameter("MandatoryPlaceholders", item.MandatoryPlaceholders));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Email template instance from the database.</summary>
        public override async Task Delete(IEntity record)
        {
            await ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
        
        static class Fields
        {
            public const int Subject = 1;
            public const int Key = 2;
            public const int Body = 3;
            public const int MandatoryPlaceholders = 4;
        }
    }
}