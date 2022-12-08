using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ResponseFactory
    {
        public static Response CreateSuccessResponse()
        {
            return new Response()
            {
                Message = "Operação realizada com sucesso!",
                HasSuccess = true
            };
        }

        public static Response CreateFailureResponse(Exception ex)
        {
            return new Response()
            {
                Message = "Erro no banco de dados, contate o administrador.",
                HasSuccess = false,
                Exception = ex
            };
        }

        public static SingleResponse<T> CreateSingleResponseFailure<T>(string message)
        {
            return new SingleResponse<T>()
            {
                Message = message,
                HasSuccess = false,
                Exception = null
            };
        }

        public static SingleResponse<T> CreateSingleResponseSucess<T>(T item)
        {
            return new SingleResponse<T>()
            {
                Message = "Operação realizada com sucesso!",
                HasSuccess = true,
                Exception = null,
                Item = item
            };
        }

        public static SingleResponse<T> CreateSingleResponseFailure<T>(Exception ex)
        {
            return new SingleResponse<T>()
            {
                Message = "Erro no banco de dados, contate o administrador",
                HasSuccess = false,
                Exception = ex,
            };
        }

        public static SingleResponse<T> CreateNotFoundResponseFailure<T>()
        {
            return new SingleResponse<T>()
            {
                Message = "Dado não encontrado.",
                HasSuccess = false,
            };
        }

    }
}
