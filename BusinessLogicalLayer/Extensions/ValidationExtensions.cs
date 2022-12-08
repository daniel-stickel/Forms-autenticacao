using Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    static class ValidationExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (!result.IsValid)
            {
                Response r = new Response();
                r.HasSuccess = false;
                StringBuilder sb = new StringBuilder();
                foreach (ValidationFailure item in result.Errors)
                {
                    sb.AppendLine(item.ErrorMessage);
                }
                r.Message = sb.ToString();
                return r;
            }
            return new Response()
            {
                HasSuccess = true,
                Message = "Cliente validado com sucesso."
            };
        }
    }
}
