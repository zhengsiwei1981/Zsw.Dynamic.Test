using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Method;
using Zsw.Dynamic.Object.LinqToSql.Method.Result;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.EntityFramework.Method
{
    public class Delete : IDelete
    {
        /// <summary>
        /// 
        /// </summary>
        internal MethodInfo MethodInfo
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal object TableInstance
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGeneric => this.MethodInfo.IsGenericMethod;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_tableInstance"></param>
        /// <param name=""></param>
        public Delete(object _tableInstance, MethodInfo _methodInfo)
        {
            this.TableInstance = _tableInstance;
            this.MethodInfo = _methodInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        /// <returns></returns>
        public IOperationResult Invoke(OperationContext operationContext)
        {
            var result = this.MethodInfo.Invoke(this.TableInstance, new object[] { operationContext.Entity });
            return new OperationResult() { Result = result };
        }
    }
}
