using Common;
using Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    //public interface IEntityCRUD<T> where T: Entity, new()
    //{
    //    Response Insert(T t);

    //}
    public interface IClienteService
    {
        Task<Response> Insert(Cliente cliente);
        Task<Response> Update(Cliente cliente);
        Task<Response> Delete(int id);
        Task<Response> Deactivate(int id);
        Task<SingleResponse<Cliente>> GetById(int id);
        Task<SingleResponse<Cliente>> GetByCPF(string cpf);
        //No mundo real, este dado é paginado!! O fonte pode ser visto na classe ClienteBLL!!
        Task<DataResponse<Cliente>> GetClientes();
    }
}
