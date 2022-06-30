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

        
        [HttpGet("Get")]
        public IActionResult Get(int id)
        {
            var result = _funcionario.Get(id);
            return new JsonResult(result);
        }
    }
}
