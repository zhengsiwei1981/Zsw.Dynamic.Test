using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Method
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInsert
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsGeneric { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IOperationResult Invoke(OperationContext operationContext);
    }
}
