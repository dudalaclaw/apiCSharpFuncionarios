using DesafioTriWebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        IEnumerable<Venda> GetUltimasVendasFuncionario();
        IEnumerable<Produtos> GetProdutos();
    }
}
