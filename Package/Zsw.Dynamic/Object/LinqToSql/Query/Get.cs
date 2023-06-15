using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Object.LinqToSql.Query.Result;
using Zsw.Dynamic.Query.Interface;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.LinqToSql.Query
{
    public class Get : IGet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public IQueryResult ToGet(QueryContext queryContext)
        {
            var methodInfo = typeof(Queryable).GetMethods().Where(m => m.Name == "FirstOrDefault" && m.GetParameters().Count() == 1).FirstOrDefault().
                MakeGenericMethod(queryContext.Table.EntityType);
            var obj = methodInfo.Invoke(null, new object[] { queryContext.Queryable });

            return new QueryResult() { Data = obj };
        }
    }
}
