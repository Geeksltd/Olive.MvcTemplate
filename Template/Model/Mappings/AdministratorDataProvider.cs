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
    
    /// <summary>Provides data-access facilities for Administrators.</summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AdministratorDataProvider : UserDataProvider
    {
        public override Type EntityType => typeof(Domain.Administrator);
        
        #region SQL Commands
        
        /// <summary>Gets a SQL command text to query a single Administrator record</summary>
        
        /// <summary>Gets the list of fields to use for loading Administrators.</summary>
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
        
        /// <summary>Provides the data source expression for querying Administrator records.</summary>
        public override string GetTables()
        {
            return @"Users AS U
            LEFT OUTER JOIN Administrators AS A ON A.Id = U.ID";
        }
        
        /// <summary>Gets a SQL command text to insert a record into Users table.</summary>
        const string INSERT_USERS_COMMAND = @"INSERT INTO Users
        (Id, FirstName, LastName, Email, Password, Salt, IsDeactivated)
        VALUES
        (@Id, @FirstName, @LastName, @Email, @Password, @Salt, @IsDeactivated)";
        
        /// <summary>Gets a SQL command text to insert a record into Administrators table.</summary>
        const string INSERT_ADMINISTRATORS_COMMAND = @"INSERT INTO Administrators
        (Id, ImpersonationToken)
        VALUES
        (@Id, @ImpersonationToken)";
        
        /// <summary>Gets a SQL command text to update a record in Users table.</summary>
        const string UPDATE_USERS_COMMAND = @"UPDATE Users SET
        Id = @Id,
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        Password = @Password,
        Salt = @Salt,
        IsDeactivated = @IsDeactivated
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to update a record in Administrators table.</summary>
        const string UPDATE_ADMINISTRATORS_COMMAND = @"UPDATE Administrators SET
        Id = @Id,
        ImpersonationToken = @ImpersonationToken
        OUTPUT INSERTED.ID
        WHERE ID = @OriginalId";
        
        /// <summary>Gets a SQL command text to delete a record from Users table.</summary>
        const string DELETE_COMMAND = @"DELETE FROM Users WHERE ID = @Id";
        
        #endregion
        
        /// <summary>Gets the database column name for a specified Administrator property.</summary>
        public override string MapColumn(string propertyName)
        {
            switch (propertyName)
            {
                case "FirstName": return "U.FirstName";
                case "LastName": return "U.LastName";
                case "Email": return "U.Email";
                case "Password": return "U.Password";
                case "Salt": return "U.Salt";
                case "IsDeactivated": return "U.IsDeactivated";
                default: return $"A.[{propertyName}]";
            }
        }
        
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
        /// Lazy-loads the data for the specified many-to-many relation on the specified Administrator instance from the database.<para/>
        /// </summary>
        public override IEnumerable<string> ReadManyToManyRelation(IEntity instance, string property)
        {
            throw new ArgumentException($"The property '{property}' is not supported for the instance of '{instance.GetType()}'");
        }
        
        /// <summary>
        /// Extracts the order of all Administrator fields (taking into account inheritence) from the specified data reader.<para/>
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
        
        /// <summary>Extracts the Administrator instance from the current record of the specified data reader.</summary>
        internal override IEntity Parse(IDataReader reader, UserFields fields)
        {
            var result = new Domain.Administrator();
            FillData(reader, result, fields);
            EntityManager.SetSaved(result, reader.GetGuid(0));
            return result;
        }
        
        /// <summary>Loads the data from the specified data reader on the specified Administrator instance.</summary>
        internal static void FillData(IDataReader reader, Domain.Administrator entity, UserFields fields)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            
            UserDataProvider.FillData(reader, entity, fields);
            
            if (values[fields.Administrators_ImpersonationToken] != DBNull.Value) entity.ImpersonationToken = values[fields.Administrators_ImpersonationToken] as string;
        }
        
        /// <summary>Saves the specified Administrator instance in the database.</summary>
        public override void Save(IEntity record)
        {
            if (record.GetType() != typeof(Domain.Administrator))
            {
                throw new ArgumentException($"Invalid argument type specified. Expected: 'Domain.Administrator', Provided: '{record.GetType()}'");
            }
            
            var item = record as Domain.Administrator;
            
            using (var scope = Database.CreateTransactionScope())
            {
                if (record.IsNew)
                {
                    Insert(item);
                }
                else
                {
                    Update(item);
                }
                
                scope.Complete();
            }
        }
        
        /// <summary>Inserts the specified new Administrator instance into the database.</summary>
        void Insert(Domain.Administrator item)
        {
            Action saveAll = () =>
            {
                ExecuteScalar(INSERT_USERS_COMMAND, CommandType.Text,
                CreateUserParameters(item));
                
                ExecuteScalar(INSERT_ADMINISTRATORS_COMMAND, CommandType.Text,
                CreateAdministratorParameters(item));
            };
            
            if (Database.AnyOpenTransaction()) saveAll();
            else using (var scope = Database.CreateTransactionScope()) { saveAll(); scope.Complete(); }
        }
        
        /// <summary>Bulk inserts a number of specified Administrators into the database.</summary>
        public override void BulkInsert(IEntity[] entities, int batchSize)
        {
            var commands = new List<KeyValuePair<string, IDataParameter[]>>();
            
            foreach (var item in entities.Cast<Domain.Administrator>())
            {
                if (item.GetType() != typeof(Domain.Administrator))
                {
                    throw new ArgumentException($"Invalid argument type specified. Expected: 'Domain.Administrator', Provided: '{item.GetType()}'");
                }
                
                commands.Add(INSERT_USERS_COMMAND, CreateUserParameters(item));
                
                commands.Add(INSERT_ADMINISTRATORS_COMMAND, CreateAdministratorParameters(item));
            }
            
            ExecuteNonQuery(CommandType.Text, commands);
        }
        
        /// <summary>Updates the specified existing Administrator instance in the database.</summary>
        void Update(Domain.Administrator item)
        {
            Action saveAll = () =>
            {
                if (ExecuteScalar(UPDATE_USERS_COMMAND, CommandType.Text, CreateUserParameters(item)).ToStringOrEmpty().IsEmpty())
                {
                    Cache.Current.Remove(item);
                    throw new ConcurrencyException($"Failed to update the 'Users' table. There is no row with the ID of {item.ID}.");
                }
                
                if (ExecuteScalar(UPDATE_ADMINISTRATORS_COMMAND, CommandType.Text, CreateAdministratorParameters(item)).ToStringOrEmpty().IsEmpty())
                {
                    Cache.Current.Remove(item);
                    throw new ConcurrencyException($"Failed to update the 'Administrators' table. There is no row with the ID of {item.ID}.");
                }
            };
            
            if (Database.AnyOpenTransaction()) saveAll();
            else using (var scope = Database.CreateTransactionScope()) { saveAll(); scope.Complete(); }
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
        
        /// <summary>Creates parameters for Inserting or Updating Administrator records</summary>
        IDataParameter[] CreateAdministratorParameters(Domain.Administrator item)
        {
            var result = new List<IDataParameter>();
            
            result.Add(CreateParameter("OriginalId", item.OriginalId));
            result.Add(CreateParameter("Id", item.GetId()));
            result.Add(CreateParameter("ImpersonationToken", item.ImpersonationToken));
            
            return result.ToArray();
        }
        
        /// <summary>Deletes the specified Administrator instance from the database.</summary>
        public override void Delete(IEntity record)
        {
            ExecuteNonQuery(DELETE_COMMAND, System.Data.CommandType.Text, CreateParameter("Id", record.GetId()));
        }
    }
}