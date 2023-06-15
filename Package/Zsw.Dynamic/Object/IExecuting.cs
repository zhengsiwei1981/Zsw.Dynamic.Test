using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic.Object
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExecuting
    {
        void Execute(OperationContext context);
    }
}
