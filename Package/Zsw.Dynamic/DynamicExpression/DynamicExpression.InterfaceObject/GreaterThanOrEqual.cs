using System.Linq.Expressions;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class GreaterThanOrEqual : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			return Expression.GreaterThanOrEqual(PropertyConverter.Convert(Property), Constant);
		}
	}
}
