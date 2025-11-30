using System;
using FluentValidation;
using APIUsuarios.Application.DTOs;

namespace APIUsuarios.Application.Validators
{
    public class UsuarioCreateDtoValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateDtoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().Length(3,100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(6);
            RuleFor(x => x.DataNascimento).NotEmpty()
                .Must(BeAtLeast18).WithMessage("Usuário deve ter pelo menos 18 anos");
            RuleFor(x => x.Telefone).Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$").When(x => !string.IsNullOrEmpty(x.Telefone));
        }

        private bool BeAtLeast18(DateTime data)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - data.Year;
            if (data > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
