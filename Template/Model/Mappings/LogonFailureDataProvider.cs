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
    
    /// <summary>Provides data-access facilities for Logon failures.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class LogonFailureDataProvider : SqlDataProvider
    {
        public override Type EntityType => typeof(Domain.LogonFailure);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Logon failure record</summary>
        
        /// <summary>Gets the list of fields to use for loading Logon failures.</summary>
        public override string GetFields()
        {
            return @"L.ID AS LogonFailures_Id,
            L.Email AS LogonFailures_Email,
            L.IP AS LogonFailures_IP,
            L.Attempts AS LogonFailures_Attempts,
            L.[Date] AS LogonFailures_Date";
        }
        
        /// <summary>Provides the data source expression for querying Logon failure records.</summary>
        public override string GetTables()
        {
            return @"LogonFailures AS L";
        }
        
        /// <summary>Gets a SQL command text to insert a record into LogonFailures table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO LogonFailures
        (Id, Email, IP, Attempts, [Date])
        VALUES
        (@Id, @Email, @IP, @Attempts, @Date)";
        
        /// <summary>Gets a SQL command text to update a record in LogonFailures table.</summary>
        const string UPDATE_COMMAND = @"UPDATE LogonFailures SET
        Id = @Id,
        Email = @Email,
        IP = @IP,
        Attempts = @Attempts,
        [Date] = @Date
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from LogonFailures table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM LogonFailures WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified LogonFailure property.</summary>
        public override string MapColumn(string propertyName) => $"L.[{propertyName}]";
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Logon failure instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Logon failure instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.LogonFailure();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Logon failure instance.</summary>
        internal static void FillData(IDataReader reader, Domain.LogonFailure entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            entity.Email = values[1] as string;
            entity.IP = values[2] as string;
            entity.Attempts = (int)values[3];
            entity.Date = (DateTime)values[4];
        }
        
        /// <summary>Saves the specified Logon failure instance in the database.</summary>
        public override void Save(IEntity record)
        {
            var item = record as Domain.LogonFailure;
            
            if (record.IsNew)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>Inserts the specified new Logon failure instance into the database.</summary>
        void Insert(Domain.LogonFailure item)
        {
            ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Logon failures into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.LogonFailure>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Logon failure instance in the database.</summary>
        void Update(Domain.LogonFailure item)
        {
            if (ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item)).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'LogonFailures' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Logon failure records</summary>
        IDataParameter[] CreateParameters(Domain.LogonFailure item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("Email", item.Email));
            result.Add(CreateParameter("IP", item.IP));
            result.Add(CreateParameter("Attempts", item.Attempts));
            result.Add(CreateParameter("Date", item.Date));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Logon failure instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
}