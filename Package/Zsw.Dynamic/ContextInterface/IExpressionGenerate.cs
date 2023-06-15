using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Paramers;

namespace Zsw.Dynamic.ContextInterface
{
    public interface IExpressionGenerate
    {
        LambdaExpression Create(QueryContext context);
    }
}
