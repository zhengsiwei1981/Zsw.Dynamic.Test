using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic
{
    internal class ExpressionFactory
    {
        static internal IQueryable Create(QueryContext context)
        {
            return Global.orderExpressionGenerate.Create(context.Table.GetWhere(Global.expressionGenerate.Create(context)).Invoke(), context);
        }
    }
}
