using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Method
{
    public interface IUpdate
    {
        IOperationResult Invoke(OperationContext operationContext);
    }
}
