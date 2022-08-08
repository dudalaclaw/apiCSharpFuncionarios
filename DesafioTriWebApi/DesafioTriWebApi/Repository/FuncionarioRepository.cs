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
                                notes Observacao,
                                to_base64(attachments) as Foto
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

        public IEnumerable<Venda> GetUltimasVendasFuncionario()
        {
            //Abaixo fica a query das ultimas vendas realizadas
            var connection = _connection.Connection();
            var scriptSql = $@"select concat(a.first_name, ' ' , a.last_name) NomeEmpregado, 
                                d.product_name NomeProduto,
                                d.id IdProduto,
                                b.order_date as data_venda
                                from northwind.employees a
                                inner join northwind.orders b
                                on a.id = b.employee_id
                                and a.city in('Seattle')
                                inner join northwind.order_details c
                                on b.id = c.order_id
                                inner join northwind.products d
                                on c.product_id = d.id 
                                order by b.order_date desc
                                limit 10;";


            var vendas = connection.Query<Venda>(scriptSql);
            return vendas;
        }

        public IEnumerable<Produtos> GetProdutos()
        {
            //Abaixo fica a query das ultimas vendas realizadas
            var connection = _connection.Connection();
            var scriptSql = $@"SELECT concat(b.first_name, ' ', b.last_name) NomeFornecedor,
                                b.business_phone ContatoFornecedor,
                                b.email_address EmailFornecedor,
                                a.id IdProduto,
                                a.product_code CodigoProduto,
                                a.product_name NomeProduto,
                                a.description DescricaoProduto,
                                a.standard_cost Custo,
                                a.list_price PrecoVenda,
                                a.quantity_per_unit UnidadeVenda,
                                a.category Categoria,
                                to_base64(a.attachments) FotoProduto
                                from northwind.products a
                                inner
                                join northwind.suppliers b
                                on a.supplier_ids = b.id; ";


            var produtos = connection.Query<Produtos>(scriptSql);
            return produtos;
        }
    }
}
