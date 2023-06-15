using System;
using System.Linq.Expressions;
using System.Reflection;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class Like : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			MethodInfo method = typeof(string).GetMethod("Contains", new Type[1]
			{
				typeof(string)
			});
			return Expression.Call(Property, method, Constant);
		}
	}
}
