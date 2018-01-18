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
    
    /// <summary>Provides data-access facilities for Users.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UserDataProvider : SqlDataProvider
    {
        public override Type EntityType => typeof(Domain.User);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single User record</summary>
        
        /// <summary>Gets the list of fields to use for loading Users.</summary>
        public override string GetFields()
        {
            return @"U.ID AS Users_Id,
            U.FirstName AS Users_FirstName,
            U.LastName AS Users_LastName,
            U.Email AS Users_Email,
            U.Password AS Users_Password,
            U.Salt AS Users_Salt,
            U.IsDeactivated AS Users_IsDeactivated,
            A.ID AS Administrators_Id,
            A.ImpersonationToken AS Administrators_ImpersonationToken";
        }
        
        /// <summary>Provides the data source expression for querying User records.</summary>
        public override string GetTables()
        {
            return @"Users AS U
            LEFT OUTER JOIN Administrators AS A ON A.Id = U.ID";
        }
        
        /// <summary>Gets a SQL command text to insert a record into Users table.</summary>
        const string INSERT_COMMAND = @"INSERT INTO Users
        (Id, FirstName, LastName, Email, Password, Salt, IsDeactivated)
        VALUES
        (@Id, @FirstName, @LastName, @Email, @Password, @Salt, @IsDeactivated)";
        
        /// <summary>Gets a SQL command text to update a record in Users table.</summary>
        const string UPDATE_COMMAND = @"UPDATE Users SET
        Id = @Id,
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        Password = @Password,
        Salt = @Salt,
        IsDeactivated = @IsDeactivated
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from Users table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM Users WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified User property.</summary>
        public override string MapColumn(string propertyName) => $"U.[{propertyName}]";
        
        public override IEnumerable<IEntity> GetList(DatabaseQuery query)
        {
            using (var reader = ExecuteGetListReader(query))
            {
                var fields = ExtractFields(reader);
                var result = new List<IEntity>();
                while (reader.Read()) result.Add(Parse(reader, fields));
                return result;
            }
        }
        
        /// <summary>
        /// Lazy-loads the data for the specified many-to-many relation on the specified User instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>
        /// Extracts the order of all User fields (taking into account inheritence) from the specified data reader.<para/>
        /// </summary>
        UserFields ExtractFields(IDataReader reader)
        {
            return new UserFields
            {
                Users_Id = reader.FieldIndex(nameof(UserFields.Users_Id)),
                Users_FirstName = reader.FieldIndex(nameof(UserFields.Users_FirstName)),
                Users_LastName = reader.FieldIndex(nameof(UserFields.Users_LastName)),
                Users_Email = reader.FieldIndex(nameof(UserFields.Users_Email)),
                Users_Password = reader.FieldIndex(nameof(UserFields.Users_Password)),
                Users_Salt = reader.FieldIndex(nameof(UserFields.Users_Salt)),
                Users_IsDeactivated = reader.FieldIndex(nameof(UserFields.Users_IsDeactivated)),
                Administrators_Id = reader.FieldIndex(nameof(UserFields.Administrators_Id)),
                Administrators_ImpersonationToken = reader.FieldIndex(nameof(UserFields.Administrators_ImpersonationToken))
            };
        }
        
        /// <summary>Extracts the User instance from the current record of the specified data reader.</summary>
        public override IEntity Parse(IDataReader reader)
        {
            return Parse(reader, ExtractFields(reader));
        }
        
        internal virtual IEntity Parse(IDataReader reader, UserFields fields)
        {
            if (reader[fields.Administrators_Id] != DBNull.Value)
                return new AdministratorDataProvider().Parse(reader, fields);
            
            throw new DataException($"The record with ID of '{reader["Users_Id"]}' exists only in the abstract database table of 'Users' and no concrete table. The data needs cleaning-up.");
        }
        
        /// <summary>Loads the data from the specified data reader on the specified User instance.</summary>
        internal static void FillData(IDataReader reader, Domain.User entity, UserFields fields)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            entity.FirstName = values[fields.Users_FirstName] as string;
            entity.LastName = values[fields.Users_LastName] as string;
            entity.Email = values[fields.Users_Email] as string;
            
            if (values[fields.Users_Password] != DBNull.Value) entity.Password = values[fields.Users_Password] as string;
            
            if (values[fields.Users_Salt] != DBNull.Value) entity.Salt = values[fields.Users_Salt] as string;
            entity.IsDeactivated = (bool)values[fields.Users_IsDeactivated];
        }
        
        /// <summary>Saves the specified User instance in the database.</summary>
        public override void Save(IEntity record)
        {
            if (record.GetType() != typeof(Domain.User))
            {
                throw new ArgumentException($"Invalid argument type specified. Expected: 'Domain.User', Provided: '{record.GetType()}'");
            }
            
            var item = record as Domain.User;
            
            if (record.IsNew)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        
        /// <summary>Inserts the specified new User instance into the database.</summary>
        void Insert(Domain.User item)
        {
            ExecuteScalar(INSERT_COMMAND, CommandType.Text,
            CreateUserParameters(item));
        }
        
        /// <summary>Bulk inserts a number of specified Users into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.User>())
            {
                if (item.GetType() != typeof(Domain.User))
                {
                    throw new ArgumentException($"Invalid argument type specified. Expected: 'Domain.User', Provided: '{item.GetType()}'");
                }
                
                commands.Add(INSERT_COMMAND, CreateUserParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing User instance in the database.</summary>
        void Update(Domain.User item)
        {
            if (ExecuteScalar(UPDATE_COMMAND, CommandType.Text, CreateUserParameters(item)).ToStringOrEmpty().IsEmpty())
            {
                Cache.Current.Remove(item);
                throw new ConcurrencyException($"Failed to update the 'Users' table. There is no row with the ID of {item.ID}.");
            }
        }
        
        /// <summary>Creates parameters for Inserting or Updating User records</summary>
        IDataParameter[] CreateUserParameters(Domain.User item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("FirstName", item.FirstName));
            result.Add(CreateParameter("LastName", item.LastName));
            result.Add(CreateParameter("Email", item.Email));
            result.Add(CreateParameter("Password", item.Password));
            result.Add(CreateParameter("Salt", item.Salt));
            result.Add(CreateParameter("IsDeactivated", item.IsDeactivated));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified User instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
    
    /// <summary>
    /// Specifies the order of each field in the User inheritence hierarchy.<para/>
    /// This is used to speed up the load operations.<para/>
    /// </summary>
    struct UserFields
    {
        public int Users_Id;
        public int Users_FirstName;
        public int Users_LastName;
        public int Users_Email;
        public int Users_Password;
        public int Users_Salt;
        public int Users_IsDeactivated;
        public int Administrators_Id;
        public int Administrators_ImpersonationToken;
    }
}