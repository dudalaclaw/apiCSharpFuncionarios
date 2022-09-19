using System.Data;

namespace DesafioTriWebApi.Dapper.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection Connection();
    }
}
