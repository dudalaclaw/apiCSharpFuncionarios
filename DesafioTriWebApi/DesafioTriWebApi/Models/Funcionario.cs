using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.AspNetCore.Http;


namespace DesafioTriWebApi.Models
{
    public class Funcionario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo nome não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo sobrenome é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo sobrenome não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo cargo é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo cargo não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O campo telefone comercial é obrigatório")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "O campo telefone comercial não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string TelefoneComercial { get; set; }
        [Required(ErrorMessage = "O campo telefone residencial é obrigatório")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "O campo telefone residencial não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string TelefoneResidencial { get; set; }
        [Required(ErrorMessage = "O campo telefone celular é obrigatório")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "O campo telefone celular não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string TelefoneCelular { get; set; }
        [Required(ErrorMessage = "O campo endereço é obrigatório")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "O campo endereço não pode ultrapassar 256 caracteres")]
        public string Endereco { get; set; } 
        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo cidade não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo estado é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo estado não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Estado { get; set; }
        [Required(ErrorMessage = "O campo código postal é obrigatório")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "O campo estado não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string CodigoPostal { get; set; }
        [Required(ErrorMessage = "O campo pais é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo pais não pode ultrapassar 50 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Pais { get; set; }
        public string Website { get; set; } 
        [Required(ErrorMessage = "O campo observação é obrigatório")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "O campo observação não pode ultrapassar 256 caracteres", ErrorMessageResourceType = typeof(string))]
        public string Observacao { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "O campo FotoArquivo é obrigatório")]
        public IFormFile FotoArquivo { get; set; }
        [Required(ErrorMessage = "O campo Ativo é obrigatório")]
        public bool Ativo { get; set; }

        IEnumerable<Salario> Salarios { get; set; }
    }
}
