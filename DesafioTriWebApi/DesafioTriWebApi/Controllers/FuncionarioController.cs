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

        [HttpGet("GetUltimasVendasFuncionario/{id:int}")]
        public IActionResult GetUltimasVendasFuncionario(int id)
        { 
            var result = _funcionario.GetUltimasVendasFuncionario(id);
            return new JsonResult(result);
        }
    }
}
