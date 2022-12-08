using System;
using System.Collections.Generic;

namespace Metadata
{
    public class Cliente 
    {
        //Permitir que o EF instancie os objetos (como ao criar um construtor, o nosso
        //padrão deixaria de existir, o EntityFramework não conseguiria criar uma instância
        //padrão de Cliente sem esta sobrecarga!!
        //protected Cliente() { }

        //Criar seu construtor de acordo com as regras do seu négocio
        //public Cliente(int id, string nome)
        //{
        //    this.ID = id;
        //    this.Nome = nome;
        //}
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}