using DataModels;
using Queries.Commands;
using Queries.Executers;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using PhoneContact.Repositories;
using System;

namespace Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : DataModel
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandText _commandText;
        private readonly string _connStr;
        private readonly IExecuters _executers;
        private readonly string _tableName;

        IUnitOfWork IRepository<T>.UnitOfWork => throw new System.NotImplementedException();

        public Repository(IConfiguration configuration, ICommandText commandText, IExecuters executers, string tableName)
        {
            _commandText = commandText;
            _executers = executers;
            _tableName = tableName;
            setCurrentTable();
            _configuration = configuration;
            _connStr = getConnectionString();
        }

        public T Create(object parameters)
        {
            setCurrentTable();

            var UIID = _executers.ExecuteCommand(
                _connStr,
                conn =>
                {
                    return conn.ExecuteScalar<Guid>(
                        _commandText.CreateCommand,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                });

            return Read(UIID);
        }

        public T Read(Guid UIID)
        {
            setCurrentTable();

            var parameters = new
            {
                UIID
            };

            return _executers.ExecuteCommand(
                _connStr,
                conn => conn.QueryFirstOrDefault<T>(
                    _commandText.ReadCommmand,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ));
        }

        public T Update(object parameters)
        {
            setCurrentTable();

           var dataModel = _executers.ExecuteCommand(
                _connStr,
                conn =>
                {
                    return conn.QueryFirstOrDefault<T>(
                        _commandText.UpdateCommand,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                });

            return dataModel;
        }

        public bool Delete(Guid UIID)
        {
            setCurrentTable();

            var parameters = new
            {
                UIID
            };
            return _executers.ExecuteCommand(
                 _connStr,
                 conn =>
                 {
                     return conn.ExecuteScalar<bool>(
                         _commandText.DeleteCommand,
                         parameters,
                         commandType: CommandType.StoredProcedure
                     );
                 });
        }

        public IEnumerable<T> ListAll()
        {
            setCurrentTable();

            var items = _executers.ExecuteCommand(
                           _connStr,
                           conn => conn.Query<T>(
                               _commandText.ListAllCommand,
                               commandType: CommandType.StoredProcedure
                           ));

            return items;
        }

        public IEnumerable<T> ListAllByMaster(Guid masterUIID)
        {
            setCurrentTable();
            var parameters = new
            {
                masterUIID
            };
            var items = _executers.ExecuteCommand(
                         _connStr,
                         conn => conn.Query<T>(
                             _commandText.ListAllCommand,
                            parameters,
                            commandType: CommandType.StoredProcedure
                         ));

            return items;
        }

        public IEnumerable<T> Search(object parameters)
        {
            setCurrentTable();

            var items = _executers.ExecuteCommand(
                           _connStr,
                           conn => conn.Query<T>(
                               _commandText.SearchCommand,
                               parameters,
                               commandType: CommandType.StoredProcedure
                           ));

            return items;
        }
        public T Find(object parameters)
        {
            setCurrentTable();

            return _executers.ExecuteCommand(
                _connStr,
                conn => conn.QueryFirstOrDefault<T>(
                    _commandText.FindCommand,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ));
        }

        private string getConnectionString()
        {
            return _configuration.GetSection("DataSource:ConnectionString").Value;
        }

        private void setCurrentTable()
        {
            _commandText.CurrentTableName = _tableName;
        }
    }
}
