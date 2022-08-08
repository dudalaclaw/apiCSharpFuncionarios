using DesafioTriWebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public interface IProdutosRepository : IRepository<Funcionario>
    {
        IEnumerable<Produtos> GetProdutos();
    }
}