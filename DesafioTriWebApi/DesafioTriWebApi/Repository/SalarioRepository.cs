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
using System;
using DesafioTriWebApi.Models;
using DesafioTriWebApi.Repository;

namespace DesafioTriWebApi.Repository
{
    public class SalarioRepository : Repository<Salario>, ISalarioRepository
    {
        private readonly IConnectionFactory _connection;

        public SalarioRepository(IConnectionFactory connection)
        {
            _connection = connection;
        }
        
        public override IEnumerable<Salario> GetAll()
        {
            var connection = _connection.Connection();
            var sql = @$"SELECT id,
                                employee_id IdFuncionario, 
                                dataDosalario DataDoSalario,
                                salario SalarioFuncionario
                            FROM db_desafio_tri.Employee_Salary;";

            var salarios = connection.Query<Salario>(sql);


            return salarios;
        }
       
        public IEnumerable<Salario> GetSalarioByFuncionario(int idFuncionario)
        {
            var connection = _connection.Connection();
            var sql = @$"SELECT id,
                                employee_id IdFuncionario, 
                                dataDosalario DataDoSalario,
                                salario SalarioFuncionario
                            FROM db_desafio_tri.Employee_Salary
                                            WHERE employee_id = {idFuncionario};";

            var salarioById = connection.Query<Salario>(sql);

            return salarioById;
        }


        public string InserirSalario(Salario salario)
        {

            var retorno = string.Empty;

            string sql = @"INSERT INTO db_desafio_tri.Employee_Salary 
                               (Employee_Salary.employee_id,
                                Employee_Salary.DataDoSalario,
                                Employee_Salary.Salario)
                                VALUES (@IdFuncionario, @DataDoSalario, @SalarioFuncionario);";
            MySqlConnection con = null;

            try
            {
                con = _connection.MySqlConnection();
                MySqlCommand cmd = new MySqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@IdFuncionario", salario.IdFuncionario);
                cmd.Parameters.AddWithValue("@DataDoSalario", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@SalarioFuncionario", salario.SalarioFuncionario);
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
    }
}
