using System;
using System.ComponentModel.DataAnnotations;

namespace APIUsuarios.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = null!;

        [Required]
        public DateTime DataNascimento { get; set; }

        public string? Telefone { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataAtualizacao { get; set; }
    }
}
