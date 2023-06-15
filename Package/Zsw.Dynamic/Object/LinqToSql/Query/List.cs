using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Object.LinqToSql.Query.Result;
using Zsw.Dynamic.Query.Interface;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.LinqToSql.Query
{
    public class List : IToList
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public int Count(QueryContext queryContext)
        {
            var countResult = this.CreateGenericMethodVersion(typeof(Queryable),
                m => m.Name == "Count" && m.GetParameters().Count() == 1, queryContext.Table.EntityType).Invoke(null, new object[] { queryContext.Queryable });
            return (int)countResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public object ToList(QueryContext queryContext)
        {
            if (queryContext.requestParamer.EachPageNumber >= 0)
            {
                var skipTarget = this.CreateGenericMethodVersion(typeof(Enumerable),
       m => m.Name == "Skip", queryContext.Table.EntityType).Invoke(null, new object[] { queryContext.Queryable, queryContext.requestParamer.Index * queryContext.requestParamer.EachPageNumber });

                var takeTarget = this.CreateGenericMethodVersion(typeof(Enumerable),
                    m => m.Name == "Take", queryContext.Table.EntityType).Invoke(null, new object[] { skipTarget, queryContext.requestParamer.EachPageNumber });

                return this.CreateGenericMethodVersion(typeof(Enumerable),
                    m => m.Name == "ToList", queryContext.Table.EntityType).Invoke(null, new object[] { takeTarget });
            }
            else
            {
                return this.CreateGenericMethodVersion(typeof(Enumerable),
                 m => m.Name == "ToList", queryContext.Table.EntityType).Invoke(null, new object[] { queryContext.Queryable });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        public IQueryResult ToQueryResult(QueryContext queryContext)
        {
            return new QueryResult()
            {
                Data = this.ToList(queryContext),
                EachPageNumber = queryContext.requestParamer.EachPageNumber,
                Index = queryContext.requestParamer.Index,
                TotalRecord = this.Count(queryContext),
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private MethodInfo CreateGenericMethodVersion(Type baseClassType, Func<MethodInfo, bool> predicate, Type genericType)
        {
            return baseClassType.GetMethods().Where(predicate).FirstOrDefault().MakeGenericMethod(genericType);
        }
    }
}
