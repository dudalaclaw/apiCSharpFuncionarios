using System.Net;
using DesafioTriWebApi.Models;
using DesafioTriWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTriWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalarioController : ControllerBase
    {
        ISalarioRepository _salario;
        public SalarioController(ISalarioRepository salario)
        {
            _salario = salario;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _salario.GetAll();
            return new JsonResult(result);
        }

        [HttpGet("GetSalarioByFuncionario/{id:int}")]
        public IActionResult GetSalarioByFuncionario(int id)
        {
            var result = _salario.GetSalarioByFuncionario(id);
            return new JsonResult(result);
        }

        [HttpPost("PostSalario")]
        public IActionResult InserirSalario(Salario salario)
        {
            var result = _salario.InserirSalario(salario);
            if (string.IsNullOrEmpty(result))
                return Sucesso("Salario cadastrado com sucesso!");

            return Erro(result);

        }

        private static IActionResult Sucesso(string mensagem)
        {
            return new JsonResult(new
            {
                status = HttpStatusCode.OK,
                mensagem = mensagem

            });
        }

        private static IActionResult Erro(string result)
        {
            return new JsonResult(new
            {
                status = HttpStatusCode.InternalServerError,
                mensagem = result

            });
        }
    }
}
