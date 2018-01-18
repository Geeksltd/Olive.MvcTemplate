namespace App.DAL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using MSharp.Framework;
    using MSharp.Framework.Data;
    using MSharp.Framework.Data.Ado.Net;
    
    /// <summary>Provides data-access facilities for Settings.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SettingsDataProvider : SqlDataProvider
    {
        public override Type EntityType => typeof(Domain.Settings);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Settings record</summary>
        
        /// <summary>Gets the list of fields to use for loading Settings.</summary>
        public override string GetFields()
        {
            return @"S.ID AS Settings_Id,
            S.Name AS Settings_Name,
            S.PasswordResetTicketExpiryMinutes AS Settings_PasswordResetTicketExpiryMinutes,
            S.CacheVersion AS Settings_CacheVersion";
        }
        
        /// <summary>Provides the data source expression for querying Settings records.</summary>
        public override string GetTables()
        {
            return @"Settings AS S";
        }
        
        /// <summary>Gets a SQL command text to insert a record into Settings table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO Settings
        (Id, Name, PasswordResetTicketExpiryMinutes, CacheVersion)
        VALUES
        (@Id, @Name, @PasswordResetTicketExpiryMinutes, @CacheVersion)";
        
        /// <summary>Gets a SQL command text to update a record in Settings table.</summary>
        const string UPDATE_COMMAND = @"UPDATE Settings SET
        Id = @Id,
        Name = @Name,
        PasswordResetTicketExpiryMinutes = @PasswordResetTicketExpiryMinutes,
        CacheVersion = @CacheVersion
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from Settings table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM Settings WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified Settings property.</summary>
        public override string MapColumn(string propertyName) => $"S.[{propertyName}]";
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Settings instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Settings instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.Settings();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Settings instance.</summary>
        internal static void FillData(IDataReader reader, Domain.Settings entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            entity.Name = values[1] as string;
            entity.PasswordResetTicketExpiryMinutes = (int)values[2];
            entity.CacheVersion = (int)values[3];
        }
        
        /// <summary>Saves the specified Settings instance in the database.</summary>
        public override void Save(IEntity record)
        {
            var item = record as Domain.Settings;
            
            if (record.IsNew)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>Inserts the specified new Settings instance into the database.</summary>
        void Insert(Domain.Settings item)
        {
            ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Settings into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.Settings>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Settings instance in the database.</summary>
        void Update(Domain.Settings item)
        {
            if (ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item)).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'Settings' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Settings records</summary>
        IDataParameter[] CreateParameters(Domain.Settings item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("Name", item.Name));
            result.Add(CreateParameter("PasswordResetTicketExpiryMinutes", item.PasswordResetTicketExpiryMinutes));
            result.Add(CreateParameter("CacheVersion", item.CacheVersion));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Settings instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
}