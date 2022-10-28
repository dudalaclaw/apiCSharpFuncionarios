using DesafioTriWebApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace DesafioTriWebApi.Repository
{
    public interface ISalarioRepository : IRepository<Salario>
    {

        IEnumerable<Salario> GetSalarioByFuncionario(int idFuncionario);
        public string InserirSalario(Salario salario);
    
    }
}
