using System.Drawing;

namespace DesafioTriWebApi.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cargo { get; set; }
        public string TelefoneComercial { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneCelular { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Website { get; set; }
        public string Observacao { get; set; }
        public decimal Salario { get; set; }
        public Image Foto { get; set; }
    }
}
