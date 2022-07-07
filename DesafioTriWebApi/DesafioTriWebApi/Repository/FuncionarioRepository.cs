using Dapper;
using DesafioTriWebApi.Dapper.Interfaces;
using DesafioTriWebApi.Models;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

            var scriptFoto = $"SELECT attachments FROM northwind.employees WHERE id = {id};";
            byte[] blobFoto = connection.Query<byte[]>(scriptFoto).FirstOrDefault();

            using (MemoryStream ms = new(blobFoto))
            {
                funcionario.Foto = Image.FromStream(ms);
            }

            return funcionario;
        }

        public IEnumerable<Venda> GetUltimasVendasFuncionario(int idFuncionario)
        {
            var connection = _connection.Connection();
            var scriptSql = $@"SELECT o.id 
                                FROM northwind.employees a
                                inner join northwind.orders o on a.id = o.employee_id
                                where a.id = {idFuncionario}
                                ORDER BY o.id desc
                                LIMIT 10;";

            var vendas = connection.Query<Venda>(scriptSql);
            return vendas;
        }
    }
}
