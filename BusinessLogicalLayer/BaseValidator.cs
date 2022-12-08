using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    public abstract class BaseValidator<T>
    {
        //TODO -> Refatorar o fonte do BLL e mover FluentValidation para cá!
        protected abstract void Normatize(T item);
        protected abstract void ReNormatize(T item);
    }
}
