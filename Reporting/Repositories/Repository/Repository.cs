using DataModels;
using Queries.Commands;
using Queries.Executers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Reporting.Repositories;
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

            long id = _executers.ExecuteCommand(
                _connStr,
                conn =>
                {
                    return conn.ExecuteScalar<long>(
                        _commandText.CreateCommand,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                });

            return Read(id);
        }

        public T Read(long id)
        {
            setCurrentTable();

            var parameters = new
            {
                Id = id
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

        public bool Delete(long id)
        {
            setCurrentTable();

            var parameters = new
            {
                Id = id
            };
            var isDeleted = _executers.ExecuteCommand(
                 _connStr,
                 conn =>
                 {
                     return conn.ExecuteScalar<bool>(
                         _commandText.DeleteCommand,
                         parameters,
                         commandType: CommandType.StoredProcedure
                     );
                 });

            return isDeleted;
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

        public IEnumerable<T> ListAllByMaster(long masterId)
        {
            setCurrentTable();
            var parameters = new
            {
                Master = masterId,
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

        public IEnumerable<T> ListByCommand(string command)
        {
            setCurrentTable();

            return _executers.ExecuteCommand(
                _connStr,
                conn => conn.Query<T>(
                    command, 
                    commandType: CommandType.StoredProcedure
                ));
        }

        public IEnumerable<T> ListByCommand(string command, object parameters)
        {
            setCurrentTable();

            return _executers.ExecuteCommand(
                _connStr,
                conn => conn.Query<T>(
                    command,
                    parameters,
                    commandType: CommandType.StoredProcedure
                ));
        }
        private string getConnectionString()
        {
            return _configuration.GetSection("ConnectionStrings:ReportingDBConnectionString").Value;
        }

        private void setCurrentTable()
        {
            _commandText.CurrentTableName = _tableName;
        }

       
    }
}
