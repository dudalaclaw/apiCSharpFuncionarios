using Dapper;
using DesafioTriWebApi.Dapper.Interfaces;
using DesafioTriWebApi.Models;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>
    {
        private readonly IConnectionFactory _connection;

        public FuncionarioRepository(IConnectionFactory connection)
        {
            _connection = connection;
        }

        public override IEnumerable<Funcionario> GetAll()
        {
            var connection = _connection.Connection();
            var scriptSql = @"SELECT id,
                                business_phone,
                                home_phone,
                                mobile_phone, 
                                address,
                                city,
                                state_province,
                                zip_postal_code,
                                country_region,
                                web_page,
                                notes 
                              FROM northwind.employees;";

            var funcionarios = connection.Query<Funcionario>(scriptSql);
            return funcionarios;
        }
    }
}
