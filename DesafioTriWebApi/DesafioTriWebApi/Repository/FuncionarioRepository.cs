using Dapper;
using DesafioTriWebApi.Dapper.Interfaces;
using DesafioTriWebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace DesafioTriWebApi.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
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
                                first_name Nome, 
                                last_name Sobrenome,
                                job_title Cargo,
                              FROM northwind.employees
                              WHERE city = 'Seattle';";

            var funcionarios = connection.Query<Funcionario>(scriptSql);
            return funcionarios;
        }

        public override Funcionario Get(int id)
        {
            var connection = _connection.Connection();
            var scriptSql = @$"SELECT id,
                                first_name Nome, 
                                last_name Sobrenome,
                                job_title Cargo,
                                business_phone TelefoneComercial,
                                home_phone TelefoneResidencial,
                                mobile_phone TelefoneCelular,
                                address Endereco,
                                city Cidade,
                                state_province Estado,
                                zip_postal_code CodigoPostal,
                                country_region Pais,
                                web_page Website,
                                notes Observacao
                              FROM northwind.employees
                              WHERE id = {id};";

            var funcionario = connection.Query<Funcionario>(scriptSql).FirstOrDefault();
            return funcionario;
        }
    }
}
