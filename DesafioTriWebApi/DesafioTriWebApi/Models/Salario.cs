using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using DesafioTriWebApi.Models;

namespace DesafioTriWebApi.Models
{
    public class Salario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Id Funcionário é obrigatório")]
        public int IdFuncionario { get; set; }
        public DateTime DataDoSalario { get; set; }
        [Required(ErrorMessage = "O campo Salário é obrigatório")]
        public decimal SalarioFuncionario { get; set; }
    }
}
