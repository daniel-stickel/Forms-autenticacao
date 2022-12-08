using BusinessLogicalLayer.Interfaces;
using BusinessLogicalLayer.Validators.ClienteValidator;
using Common;
using FluentValidation.Results;
using Metadata;
using System;
using System.Text;
using BusinessLogicalLayer.Extensions;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BusinessLogicalLayer
{
    public class ClienteBLL : BaseValidator<Cliente>, IClienteService
    {
        protected override void Normatize(Cliente item)
        {
            item.Nome = item.Nome.Normatize();
            item.Telefone = item.Telefone.RemoveMask();
            item.Email = item.Email.Normatize();
            item.CPF = item.CPF.RemoveMask();
        }

        protected override void ReNormatize(Cliente item)
        {
            item.CPF = item.CPF.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        }

        public async Task<Response> Insert(Cliente cliente)
        {
            this.Normatize(cliente);
            InsertClienteValidator validator = new InsertClienteValidator();
            ValidationResult result = validator.Validate(cliente);
            Response response = result.ToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }
            using (FarmaBruContext db = new FarmaBruContext())
            {
                db.Clientes.Add(cliente);
                try
                {
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        HasSuccess = true,
                        Message = "Cliente inserido com sucesso!"
                    };
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("UQ_CLIENTE_EMAIL"))
                    {
                        return new Response()
                        {
                            HasSuccess = false,
                            Message = "Email já cadastrado.",
                            Exception = ex
                        };
                    }
                    return new Response()
                    {
                        HasSuccess = false,
                        Message = "Erro no banco de dados, contate o administrador",
                        Exception = ex
                    };
                }
            }




            //ADO.NET 
            //SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\moc\Documents\FarmaBruDB.mdf;Integrated Security=True;Connect Timeout=30";

            //SqlCommand command = new SqlCommand();
            //command.Connection = conn;
            //command.CommandText = "INSERT INTO CLIENTES VALUES (@NOME,@CPF,@TELEFONE,@EMAIL,@DATANASCIMENTO)";
            //command.Parameters.AddWithValue("@NOME", cliente.Nome);
            //command.Parameters.AddWithValue("@CPF", cliente.CPF);
            //command.Parameters.AddWithValue("@TELEFONE", cliente.Telefone);
            //command.Parameters.AddWithValue("@EMAIL", cliente.Email);
            //command.Parameters.AddWithValue("@DATANASCIMENTO", cliente.DataNascimento);

            //conn.Open();
            //command.ExecuteNonQuery();
            //conn.Dispose();

        }

        public async Task<Response> Update(Cliente cliente)
        {
            this.Normatize(cliente);
            UpdateClienteValidator validator = new UpdateClienteValidator();
            ValidationResult result = validator.Validate(cliente);
            Response response = result.ToResponse();
            if (!response.HasSuccess)
            {
                return response;
            }
            try
            {
                using (FarmaBruContext db = new FarmaBruContext())
                {
                    //Melhor implementação!
                    db.Entry(cliente).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return ResponseFactory.CreateSuccessResponse();
                    //Quando o objeto cliente não está completo, necessitamos de uma roundtrip adicional!
                    //Cliente clienteBanco = await db.Clientes.FindAsync(cliente.ID);
                    //clienteBanco.Nome = cliente.Nome;
                    //clienteBanco.Email = cliente.Email;
                    //await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailureResponse(ex);
            }
        }

        public async Task<Response> Delete(int id)
        {
            using (FarmaBruContext db = new FarmaBruContext())
            {
                db.Entry(new Cliente() { ID = id }).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
                //Quando o objeto cliente não está completo, necessitamos de uma roundtrip adicional!
                //Cliente clienteBanco = await db.Clientes.FindAsync(cliente.ID);
                //clienteBanco.Nome = cliente.Nome;
                //clienteBanco.Email = cliente.Email;
                //await db.SaveChangesAsync();
            }
        }

        public Task<Response> Deactivate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SingleResponse<Cliente>> GetById(int id)
        {
            if (id <= 0)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>("ID inválido.");
            }
            try
            {
                using (FarmaBruContext db = new FarmaBruContext())
                {
                    //Se não for encontrado o ID do cliente, o retorno é null!
                    Cliente cliente = await db.Clientes.FindAsync(id);
                    if (cliente == null)
                    {
                        return ResponseFactory.CreateNotFoundResponseFailure<Cliente>();
                    }
                    this.ReNormatize(cliente);
                    return ResponseFactory.CreateSingleResponseSucess<Cliente>(cliente);
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateSingleResponseFailure<Cliente>(ex);
            }
        }

        public Task<SingleResponse<Cliente>> GetByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        //public async Task<DataResponse<Cliente>> GetClientes(int page, int size)
        //db.Clientes.Skip((page-1) * size).Take(size)
        public async Task<DataResponse<Cliente>> GetClientes()
        {
            try
            {
                using (FarmaBruContext db = new FarmaBruContext())
                {
                    List<Cliente> clientes = await db.Clientes.Where(c => c.Ativo).ToListAsync();
                    clientes.ForEach(c => this.ReNormatize(c));
                    //Action, Func, Expression
                    return new DataResponse<Cliente>()
                    {
                        HasSuccess = true,
                        Message = "Dados selecionados com sucesso.",
                        Data = clientes
                    };
                }
            }
            catch (Exception ex)
            {
                return new DataResponse<Cliente>()
                {
                    HasSuccess = true,
                    Message = "Erro no banco de dados, contate o administrador.",
                    Exception = ex
                };
            }
        }
    }
}
