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
    
    /// <summary>Provides data-access facilities for Content blocks.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ContentBlockDataProvider : SqlDataProvider
    {
        public override Type EntityType => typeof(Domain.ContentBlock);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Content block record</summary>
        
        /// <summary>Gets the list of fields to use for loading Content blocks.</summary>
        public override string GetFields()
        {
            return @"C.ID AS ContentBlocks_Id,
            C.[Key] AS ContentBlocks_Key,
            C.Content AS ContentBlocks_Content";
        }
        
        /// <summary>Provides the data source expression for querying Content block records.</summary>
        public override string GetTables()
        {
            return @"ContentBlocks AS C";
        }
        
        /// <summary>Gets a SQL command text to insert a record into ContentBlocks table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO ContentBlocks
        (Id, [Key], Content)
        VALUES
        (@Id, @Key, @Content)";
        
        /// <summary>Gets a SQL command text to update a record in ContentBlocks table.</summary>
        const string UPDATE_COMMAND = @"UPDATE ContentBlocks SET
        Id = @Id,
        [Key] = @Key,
        Content = @Content
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from ContentBlocks table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM ContentBlocks WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified ContentBlock property.</summary>
        public override string MapColumn(string propertyName) => $"C.[{propertyName}]";
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified Content block instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>Extracts the Content block instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            var result = new Domain.ContentBlock();
            FillData(reader, result);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Content block instance.</summary>
        internal static void FillData(IDataReader reader, Domain.ContentBlock entity)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            entity.Key = values[1] as string;
            entity.Content = values[2] as string;
        }
        
        /// <summary>Saves the specified Content block instance in the database.</summary>
        public override void Save(IEntity record)
        {
            var item = record as Domain.ContentBlock;
            
            if (record.IsNew)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>Inserts the specified new Content block instance into the database.</summary>
        void Insert(Domain.ContentBlock item)
        {
            ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Content blocks into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.ContentBlock>())
            {
                commands.Add(INSERT_COMMAND, CreateParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Content block instance in the database.</summary>
        void Update(Domain.ContentBlock item)
        {
            if (ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateParameters(item)).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'ContentBlocks' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating Content block records</summary>
        IDataParameter[] CreateParameters(Domain.ContentBlock item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("Key", item.Key));
            result.Add(CreateParameter("Content", item.Content));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Content block instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
}