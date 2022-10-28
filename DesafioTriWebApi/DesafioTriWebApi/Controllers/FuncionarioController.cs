using System.Net;
using DesafioTriWebApi.Models;
using DesafioTriWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTriWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        IFuncionarioRepository _funcionario;

        public FuncionarioController(IFuncionarioRepository funcionario)
        {
            _funcionario = funcionario;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _funcionario.GetAll();
            return new JsonResult(result);
        }

        [HttpGet("Get/{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _funcionario.Get(id);
            return new JsonResult(result);
        }

        [HttpGet("GetUltimasVendasFuncionario")]
        public IActionResult GetUltimasVendasFuncionario()
        {
            var result = _funcionario.GetUltimasVendasFuncionario();
            return new JsonResult(result);
        }

        [HttpGet("GetProdutos")]
        public IActionResult GetProdutos()
        {
            var result = _funcionario.GetProdutos();
            return new JsonResult(result);
        }

        [HttpPost("Post")]
        public IActionResult InserirFuncionario([FromForm] Funcionario funcionario)
        {
            var result = _funcionario.InserirFuncionario(funcionario);
            if (string.IsNullOrEmpty(result))
                return Sucesso("Funcionário cadastrado com sucesso!");

            return Erro(result);

        }

        [HttpDelete("Delete/{id:int}")]
        public IActionResult DeleteFuncionario(int id)
        {
            var result = _funcionario.DeleteFuncionario(id);
            if (string.IsNullOrEmpty(result))
                return Sucesso("Funcionário deletado com sucesso!");

            return Erro(result);
        }

        [HttpPut("Update/{id:int}")]
        public IActionResult UpdateFuncionario([FromForm] Funcionario funcionario)
        {
            var result = _funcionario.UpdateFuncionario(funcionario);
            if (string.IsNullOrEmpty(result))
                return Sucesso("Funcionário atualizado com sucesso!");

            return Erro(result);
        }

        [HttpPatch("patch/{id:int}")]
        public IActionResult PatchFuncionarioAtivo([FromForm] int id, [FromForm] bool ativo)
        {
            var result = _funcionario.PatchFuncionarioAtivo(id, ativo);
            if (string.IsNullOrEmpty(result))
                return Sucesso("Funcionário atualizado com sucesso!");

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

        [HttpPost("AtualizarFoto")]
        public IActionResult AtualizarFoto([FromForm] FotoFuncionario fotoFuncionario)
        {
            _funcionario.AtualizarImagem(fotoFuncionario);

            return Sucesso("Foto atualizada com sucesso!");
        }
    }
}

