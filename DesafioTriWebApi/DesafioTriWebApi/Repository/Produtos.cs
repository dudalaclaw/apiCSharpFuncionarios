using System.Drawing;

namespace DesafioTriWebApi.Repository
{
    public class Produtos
    {
        public string NomeFornecedor { get; set; }
        public string ContatoFornecedor { get; set; }
        public string EmailFornecedor { get; set; }
        public int IdProduto { get; set; }
        public string CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal Custo { get; set; }
        public decimal PrecoVenda { get; set; }
        public string UnidadeVenda { get; set; }
        public int Status { get; set; }
        public string Categoria { get; set; }
        public string FotoProduto { get; set; }
    }
}
