using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
    public class In : IMethod
    {
        public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
        {
            MethodInfo method = Constant.Value.GetType().GetMethod("Contains", new Type[1]
            {
                    Constant.Type.GenericTypeArguments[0].UnderlyingSystemType
            });
            return Expression.Call(Constant, method, Property);
        }
    }
}
