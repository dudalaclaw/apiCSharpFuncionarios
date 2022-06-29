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
    }
}
