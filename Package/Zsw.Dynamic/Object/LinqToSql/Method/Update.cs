using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Method;
using Zsw.Dynamic.Object.LinqToSql.Method.Result;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.LinqToSql.Method
{
    public class Update : IUpdate
    {
        public IOperationResult Invoke(OperationContext operationContext)
        {
            return new OperationResult() { Result = 1 };
        }
    }
}
