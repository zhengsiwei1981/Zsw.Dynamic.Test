using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic.ContextInterface
{
    public interface IOrderExpressionGenerate
    {
        IOrderedQueryable Create(IQueryable queryable, QueryContext context);
    }
}
