using DesafioTriWebApi.Dapper.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace DesafioTriWebApi.Dapper.Configuration
{
    public class DefaultSqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            return new MySqlConnection("Server=127.0.0.1;Database=northwind;Uid=root;Pwd=leonardo10;");
        }
    }
}
