using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Object.LinqToSql;
using Zsw.Dynamic.Paramers;

namespace Zsw.Dynamic
{
    internal class ContextFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageRequestParamer"></param>
        /// <returns></returns>
        static internal QueryContext Create(PageRequestParamer pageRequestParamer)
        {
            var context = new QueryContext()
            {
                Provder = Global.Configration.AssignDatabaseProvider(),
                EntityName = pageRequestParamer.EntityName,
                requestParamer = pageRequestParamer
            };
            context.Table = context.Provder.GetTable(context.EntityName);
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationParamers"></param>
        /// <returns></returns>
        static internal OperationContext Create(OperationParamers operationParamers)
        {
            var context = new OperationContext()
            {
                Provder = Global.Configration.AssignDatabaseProvider(),
                EntityName = operationParamers.EntityName,
                OperationParamers = operationParamers
            };
            context.Table = context.Provder.GetTable(context.EntityName);
            return context;
        }
    }
}
