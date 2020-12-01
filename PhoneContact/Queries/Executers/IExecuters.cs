using Microsoft.Data.SqlClient;
using System;

namespace Queries.Executers
{
    public interface IExecuters
    {
        void ExecuteCommand(string connStr, Action<SqlConnection> task);
        T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task);
    }
}
