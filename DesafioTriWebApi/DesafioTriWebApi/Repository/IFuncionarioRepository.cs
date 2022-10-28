using DesafioTriWebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        IEnumerable<Venda> GetUltimasVendasFuncionario();

        IEnumerable<Produtos> GetProdutos();

        string InserirFuncionario(Funcionario funcionario);

        string DeleteFuncionario(int id);

        string UpdateFuncionario(Funcionario funcionario);

        string PatchFuncionarioAtivo(int id, bool ativo);

        void AtualizarImagem(FotoFuncionario fotoFuncionario);
    }

}
