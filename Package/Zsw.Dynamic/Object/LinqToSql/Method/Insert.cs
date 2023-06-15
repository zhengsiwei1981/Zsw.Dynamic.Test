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

namespace Zsw.Dynamic.Object.LinqToSql.Method
{
    public class Insert : IInsert
    {
        /// <summary>
        /// 
        /// </summary>
        public object TableInstance
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public MethodInfo MethodInfo
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableInstance"></param>
        /// <param name="methodInfo"></param>
        public Insert(object tableInstance, MethodInfo methodInfo)
        {
            this.TableInstance = tableInstance;
            this.MethodInfo = methodInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGeneric => this.MethodInfo.IsGenericMethod;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOperationResult Invoke(OperationContext operationContext)
        {
            var result = this.MethodInfo.Invoke(this.TableInstance, new object[] { operationContext.Entity });
            return new OperationResult() { Result = result };
        }
    }
}
