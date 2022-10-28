using System.Data;
using MySql.Data.MySqlClient;

namespace DesafioTriWebApi.Dapper.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
        MySqlConnection MySqlConnection();
    }
}
