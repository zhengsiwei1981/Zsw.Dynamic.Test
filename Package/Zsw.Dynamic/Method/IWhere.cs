using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.ContextInterface;

namespace Zsw.Dynamic.Method
{
    public interface IWhere
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsGeneric { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable Invoke();
    }
}
