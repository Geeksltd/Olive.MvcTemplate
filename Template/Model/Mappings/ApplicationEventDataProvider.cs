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
    
    /// <summary>Provides data-access facilities for Application events.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ApplicationEventDataProvider : SqlDataProvider
    {
        public override Type EntityType => typeof(Domain.ApplicationEvent);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Application event record</summary>
        
        /// <summary>Gets the list of fields to use for loading Application events.</summary>
        public override string GetFields()
        {
            return @"A.ID AS ApplicationEvents_Id,
            A.UserId AS ApplicationEvents_UserId,
            A.[Date] AS ApplicationEvents_Date,
            A.Event AS ApplicationEvents_Event,
            A.ItemType AS ApplicationEvents_ItemType,
            A.ItemKey AS ApplicationEvents_ItemKey,
            A.[Data] AS ApplicationEvents_Data,
            A.IP AS ApplicationEvents_IP";
        }
        
        /// <summary>Provides the data source expression for querying Application event records.</summary>
        public override string GetTables()
        {
            return @"ApplicationEvents AS A";
        }
        
        /// <summary>Gets a SQL command text to insert a record into ApplicationEvents table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO ApplicationEvents
        (Id, UserId, [Date], Event, ItemType, ItemKey, [Data], IP)
        VALUES
        (@Id, @UserId, @Date, @Event, @ItemType, @ItemKey, @Data, @IP)";
        
        /// <summary>Gets a SQL command text to update a record in ApplicationEvents table.</summary>
        const string UPDATE_COMMAND = @"UPDATE ApplicationEvents SET
        Id = @Id,
        UserId = @UserId,
        [Date] = @Date,
        Event = @Event,
        ItemType = @ItemType,
        ItemKey = @ItemKey,
        [Data] = @Data,
        IP = @IP
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from ApplicationEvents table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM ApplicationEvents WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified ApplicationEvent property.</summary>
        public override string MapColumn(string propertyName) => $"A.[{propertyName}]";
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Application event instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Application event instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.ApplicationEvent();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Application event instance.</summary>
        internal static void FillData(IDataReader reader, Domain.ApplicationEvent entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            if (values[1] != DBNull.Value) entity.UserId = values[1] as string;
            entity.Date = (DateTime)values[2];
            entity.Event = values[3] as string;
            
            if (values[4] != DBNull.Value) entity.ItemType = values[4] as string;
            
            if (values[5] != DBNull.Value) entity.ItemKey = values[5] as string;
            
            if (values[6] != DBNull.Value) entity.Data = values[6] as string;
            
            if (values[7] != DBNull.Value) entity.IP = values[7] as string;
        }
        
        /// <summary>Saves the specified Application event instance in the database.</summary>
        public override void Save(IEntity record)
        {
            var item = record as Domain.ApplicationEvent;
            
            if (record.IsNew)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>Inserts the specified new Application event instance into the database.</summary>
        void Insert(Domain.ApplicationEvent item)
        {
            ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Application events into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.ApplicationEvent>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Application event instance in the database.</summary>
        void Update(Domain.ApplicationEvent item)
        {
            if (ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item)).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'ApplicationEvents' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Application event records</summary>
        IDataParameter[] CreateParameters(Domain.ApplicationEvent item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("UserId", item.UserId));
            result.Add(CreateParameter("Date", item.Date));
            result.Add(CreateParameter("Event", item.Event));
            result.Add(CreateParameter("ItemType", item.ItemType));
            result.Add(CreateParameter("ItemKey", item.ItemKey));
            result.Add(CreateParameter("Data", item.Data));
            result.Add(CreateParameter("IP", item.IP));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Application event instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
}