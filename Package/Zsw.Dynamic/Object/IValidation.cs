using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic.Object
{
    public interface IValidation
    {
        void Validate(OperationContext operationContext);
    }
}
