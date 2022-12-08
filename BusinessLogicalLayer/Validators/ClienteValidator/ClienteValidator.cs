using FluentValidation;
using Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicalLayer.Extensions;

namespace BusinessLogicalLayer.Validators.ClienteValidator
{
    internal class ClienteValidator : AbstractValidator<Cliente>
    {
        public void ValidateID()
        {
            RuleFor(c => c.ID).GreaterThan(0).WithMessage("ID não informado.");
        }

        public void ValidateNome()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome deve ser informado.")
                                .Length(3, 70).WithMessage("Nome deve conter entre 3 e 70 caracteres");
        }

        public void ValidateCPF()
        {
            RuleFor(c => c.CPF).NotEmpty().WithMessage("CPF deve ser informado.")
                              .Length(11).WithMessage("CPF deve conter 11 caracteres.")
                              .Must(c => c.IsValidCPF()).WithMessage("CPF inválido.");
        }

        public void ValidateTelefone()
        {
            RuleFor(c => c.Telefone).NotEmpty().WithMessage("Telefone deve ser informado")
                                   .Length(9, 15).WithMessage("Telefone deve conter entre 9 e 15 caracteres");

        }

        public void ValidateEmail()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email deve ser informado")
                .Length(10, 100).WithMessage("Email deve conter entre 10 e 100 caracteres.")
                .EmailAddress().WithMessage("Email inválido");
        }

        public void ValidateDataNascimento()
        {
            RuleFor(c => c.DataNascimento).GreaterThan(DateTime.Now.AddYears(-110)).WithMessage("Data inválida");
        }
    }
}
