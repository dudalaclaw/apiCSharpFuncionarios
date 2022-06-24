using DesafioTriWebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTriWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController
    {
        FuncionarioRepository _funcionario;

        public FuncionarioController(FuncionarioRepository funcionario)
        {
            _funcionario = funcionario;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _funcionario.GetAll();
            return new JsonResult(result);
        }
    }
}
