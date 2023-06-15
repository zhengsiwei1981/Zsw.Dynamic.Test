using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Method;

namespace Zsw.Dynamic.Object.LinqToSql.Method
{
    internal class Where : IWhere
    {
        /// <summary>
        /// 
        /// </summary>
        private MethodInfo Info
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        private LambdaExpression LambdaExpression
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        private object Instance
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public Where(object instance, MethodInfo info, LambdaExpression lambdaExpression)
        {
            this.Info = info;
            this.LambdaExpression = lambdaExpression;
            this.Instance = instance;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsGeneric
        {
            get
            {
                return this.Info.IsGenericMethod;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable Invoke()
        {
            return (IQueryable)this.Info.Invoke(null, new object[] { this.Instance, this.LambdaExpression });
        }
    }
}
