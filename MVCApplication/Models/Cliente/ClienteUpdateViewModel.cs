using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApplication.Models.Cliente
{
    public class ClienteUpdateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(70, MinimumLength = 4, ErrorMessage = "Nome deve conter entre 4 e 70 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF deve ser informado.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Telefone deve ser informado.")]
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Data de Nascimento deve ser informada.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
