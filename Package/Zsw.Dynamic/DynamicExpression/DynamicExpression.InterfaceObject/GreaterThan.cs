using System.Linq.Expressions;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class GreaterThan : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			return Expression.GreaterThan(PropertyConverter.Convert(Property), Constant);
		}
	}
}
