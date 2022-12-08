using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Validators.ClienteValidator
{
    //insert into clientes values ('Daniel Stickel','90090009000','dani.gostosao@gmail.com','47949494994',1)
    //update clientes set nome = 'Daniel Stickel Bernart' where id = 1
    //delete from clientes where id = 1
    //select * from clientes where nome like '%Daniel%'
    internal class UpdateClienteValidator : ClienteValidator
    {
        public UpdateClienteValidator()
        {
            base.ValidateID();
            base.ValidateNome();
            base.ValidateEmail();
            base.ValidateTelefone();
        }
    }
}
