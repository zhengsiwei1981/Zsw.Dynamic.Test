using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Query.Interface
{
    public interface IToList
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        object ToList(QueryContext queryContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        int Count(QueryContext queryContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        /// <returns></returns>
        IQueryResult ToQueryResult(QueryContext queryContext);
    }
}
