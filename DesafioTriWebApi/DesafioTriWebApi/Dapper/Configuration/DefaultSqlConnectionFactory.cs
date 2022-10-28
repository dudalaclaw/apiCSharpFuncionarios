using DesafioTriWebApi.Dapper.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace DesafioTriWebApi.Dapper.Configuration
{
    public class DefaultSqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Connection()
        {
            return MySqlConnection();
            //return new MySqlConnection("Server=127.0.0.1;Database=northwind;Uid=root;Pwd=02!-@Llc!@poI;");
        }

        public MySqlConnection MySqlConnection()
        {
            return new MySqlConnection("Server=instance-desafio-tri-2.cnfq1q3xbuql.sa-east-1.rds.amazonaws.com;Database=db_desafio_tri;Uid=admin;Pwd=!Adamantium2022;");
        }
    }
}