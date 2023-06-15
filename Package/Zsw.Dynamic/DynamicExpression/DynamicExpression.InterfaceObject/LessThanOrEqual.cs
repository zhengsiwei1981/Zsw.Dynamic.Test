using System.Linq.Expressions;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class LessThanOrEqual : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			return Expression.LessThanOrEqual(PropertyConverter.Convert(Property), Constant);
		}
	}
}
