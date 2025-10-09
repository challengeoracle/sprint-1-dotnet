using System.ComponentModel.DataAnnotations;

namespace Medix.Models
{
    public enum StatusUnidade
    {
        Ativa,
        Inativa,
        Suspensa,
        EmTeste
    }

    public class UnidadeMedica
    {
        public int Id { get; set; } // Chave primária gerada automaticamente

        [Required(ErrorMessage = "O nome da unidade é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(18)]
        public string CNPJ { get; set; }

        public string? Endereco { get; set; }

        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O e-mail do administrador é obrigatório.")]
        [EmailAddress]
        public string EmailAdmin { get; set; }

        public StatusUnidade Status { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}