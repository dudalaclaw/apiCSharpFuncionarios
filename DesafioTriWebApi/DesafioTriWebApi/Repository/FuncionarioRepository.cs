using Dapper;
using DesafioTriWebApi.Dapper.Interfaces;
using DesafioTriWebApi.Models;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
                              FROM db_desafio_tri.employees
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
                                notes Observacao,
                                to_base64(attachments) as Foto,
                                ativo Ativo
                              FROM db_desafio_tri.employees
                              WHERE id = {id};";

            var funcionario = connection.Query<Funcionario>(scriptSql).FirstOrDefault();
            return funcionario;
        }

        public IEnumerable<Venda> GetUltimasVendasFuncionario()
        {
            //Abaixo fica a query das ultimas vendas realizadas
            var connection = _connection.Connection();
            var scriptSql = $@"select concat(a.first_name, ' ' , a.last_name) NomeEmpregado, 
                                replace(d.product_name, 'Northwind Traders', '') NomeProduto,
                                d.id IdProduto,
                                b.order_date as data_venda
                                from db_desafio_tri.employees a
                                inner join db_desafio_tri.orders b
                                on a.id = b.employee_id
                                and a.city in('Seattle')
                                inner join db_desafio_tri.order_details c
                                on b.id = c.order_id
                                inner join db_desafio_tri.products d
                                on c.product_id = d.id 
                                order by b.order_date desc
                                limit 10;";


            var vendas = connection.Query<Venda>(scriptSql);
            return vendas;
        }

        public IEnumerable<Produtos> GetProdutos()
        {
            //Abaixo fica a query dos produtos
            var connection = _connection.Connection();
            var scriptSql = $@"SELECT concat(b.first_name, ' ', b.last_name) NomeFornecedor,
                                b.business_phone ContatoFornecedor,
                                b.email_address EmailFornecedor,
                                a.id IdProduto,
                                a.product_code CodigoProduto,
                                replace(a.product_name, 'Northwind Traders', '') NomeProduto,
                                a.description DescricaoProduto,
                                a.standard_cost Custo,
                                a.list_price PrecoVenda,
                                a.quantity_per_unit UnidadeVenda,
                                a.category Categoria,
                                to_base64(a.attachments) FotoProduto
                                from db_desafio_tri.products a
                                inner
                                join db_desafio_tri.suppliers b
                                on a.supplier_ids = b.id;";


            var produtos = connection.Query<Produtos>(scriptSql);
            return produtos;
        }


        public string InserirFuncionario(Funcionario funcionario)
        {
            var retorno = string.Empty;
            var userImage = ConverterImagemEmBytes(funcionario.FotoArquivo);

            string sql = @"INSERT INTO db_desafio_tri.employees 
                               (employees.first_name,
                                employees.last_name, 
                                employees.email_address, 
                                employees.job_title,
                                employees.business_phone, 
                                employees.home_phone, 
                                employees.mobile_phone,
                                employees.address, 
                                employees.city, 
                                employees.state_province, 
                                employees.zip_postal_code,
                                employees.country_region, 
                                employees.web_page, 
                                employees.notes,
                                employees.attachments,
                                employees.ativo)
                                VALUES (@Nome, @Sobrenome, @Email, @Cargo, @TelefoneComercial, @TelefoneResidencial, @TelefoneCelular, 
                                        @Endereco, @Cidade, @Estado, @CodigoPostal, @Pais, @Website, @Observacao, @Foto, @Ativo);";
            MySqlConnection con = null;

            try
            {
                con = _connection.MySqlConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                var paramUserImage = new MySqlParameter("@Foto", MySqlDbType.Blob, userImage.Length);
                paramUserImage.Value = userImage;

                cmd.Parameters.AddWithValue("@Id", funcionario.Id);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@Email", funcionario.Email);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
                cmd.Parameters.AddWithValue("@TelefoneComercial", funcionario.TelefoneComercial);
                cmd.Parameters.AddWithValue("@TelefoneResidencial", funcionario.TelefoneResidencial);
                cmd.Parameters.AddWithValue("@TelefoneCelular", funcionario.TelefoneCelular);
                cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Estado", funcionario.Estado);
                cmd.Parameters.AddWithValue("@CodigoPostal", funcionario.CodigoPostal);
                cmd.Parameters.AddWithValue("@Pais", funcionario.Pais);
                cmd.Parameters.AddWithValue("@Website", funcionario.Website);
                cmd.Parameters.AddWithValue("@Observacao", funcionario.Observacao);
                cmd.Parameters.Add(paramUserImage);
                cmd.Parameters.AddWithValue("@Ativo", funcionario.Ativo);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return retorno;
        }


        public string DeleteFuncionario(int id)
        {
            var retorno = string.Empty;
            string sql = @"DELETE 
                            FROM db_desafio_tri.employees 
                            WHERE (ID = @Id);";
            MySqlConnection con = null;

            try
            {
                con = _connection.MySqlConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return retorno;
        }

        public string UpdateFuncionario(Funcionario funcionario)
        {
            var retorno = string.Empty;
            var userImage = ConverterImagemEmBytes(funcionario.FotoArquivo);

            string sql = @"UPDATE db_desafio_tri.employees 
                            SET employees.first_name= @Nome, 
                            employees.last_name = @Sobrenome, 
                            employees.email_address = @Email, 
                            employees.job_title = @Cargo,
                            employees.business_phone = @TelefoneComercial, 
                            employees.home_phone = @TelefoneResidencial, 
                            employees.mobile_phone = @TelefoneCelular,
                            employees.address = @Endereco, 
                            employees.city = @Cidade, 
                            employees.state_province = @Estado, 
                            employees.zip_postal_code = @CodigoPostal,
                            employees.country_region = @Pais, 
                            employees.web_page = @Website, 
                            employees.notes = @Observacao,
                            employees.attachments = @Foto,
                            employees.ativo = @Ativo    
                            WHERE ID=@Id;";
            MySqlConnection con = null;

            try
            {
                con = _connection.MySqlConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                var paramUserImage = new MySqlParameter("@Foto", MySqlDbType.Blob, userImage.Length);
                paramUserImage.Value = userImage;

                cmd.Parameters.AddWithValue("@Id", funcionario.Id);
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);
                cmd.Parameters.AddWithValue("@Email", funcionario.Email);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
                cmd.Parameters.AddWithValue("@TelefoneComercial", funcionario.TelefoneComercial);
                cmd.Parameters.AddWithValue("@TelefoneResidencial", funcionario.TelefoneResidencial);
                cmd.Parameters.AddWithValue("@TelefoneCelular", funcionario.TelefoneCelular);
                cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Estado", funcionario.Estado);
                cmd.Parameters.AddWithValue("@CodigoPostal", funcionario.CodigoPostal);
                cmd.Parameters.AddWithValue("@Pais", funcionario.Pais);
                cmd.Parameters.AddWithValue("@Website", funcionario.Website);
                cmd.Parameters.AddWithValue("@Observacao", funcionario.Observacao);
                cmd.Parameters.Add(paramUserImage);
                cmd.Parameters.AddWithValue("@Ativo", funcionario.Ativo);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return retorno;
        }

        public string PatchFuncionarioAtivo(int id, bool ativo)
        {
            var retorno = string.Empty;
            string sql = @"UPDATE db_desafio_tri.employees 
                            SET employees.ativo = @Ativo    
                            WHERE ID=@Id;";
            MySqlConnection con = null;

            try
            {
                con = _connection.MySqlConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Ativo", ativo);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return retorno;

        }


        public void AtualizarImagem(FotoFuncionario fotoFuncionario)
        {
            var userImage = ConverterImagemEmBytes(fotoFuncionario.File);
            MySqlConnection con = _connection.MySqlConnection();

            con.Open();

            var cmd = new MySqlCommand("", con);

            cmd.CommandText = @"UPDATE db_desafio_tri.employees  
                                SET attachments = @Foto
                                WHERE ID  = @Id;";

            var paramUserImage = new MySqlParameter("@Foto", MySqlDbType.Blob, userImage.Length);
            var paramUserId = new MySqlParameter("@Id", MySqlDbType.VarChar, 256);

            paramUserImage.Value = userImage;
            paramUserId.Value = fotoFuncionario.IdFuncionario;

            cmd.Parameters.Add(paramUserImage);
            cmd.Parameters.Add(paramUserId);

            cmd.ExecuteNonQuery();

            con.Close();
        }

        public byte[] ConverterImagemEmBytes(IFormFile file) // IMAGEM EM BINÁRIO 
        {
            using (var ms = new System.IO.MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public IFormFile ValidarImagem(IFormFile file)
        {
            if (file != null)
            {
                return file;
            }
            else
            {
                return null;
            }
        }

        //public int ValidarAtivo(bool ativo)
        //{
        //    if (ativo == true)
        //    {
        //        Convert.ToInt64(ativo);
        //        ativo = 1;
        //    }
        //    else if (ativo == false)
        //    {
        //        Convert.ToInt64(ativo);
        //        ativo = 0;
        //    }
        //}
    }
}
