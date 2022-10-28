using Microsoft.AspNetCore.Http;
namespace DesafioTriWebApi.Models
{
    public class FotoFuncionario
    {
        public int IdFuncionario { get; set; }
        public IFormFile File { get; set; }
    }
}
