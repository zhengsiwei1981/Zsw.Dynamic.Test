using System.Linq.Expressions;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class LessThan : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			return Expression.LessThan(PropertyConverter.Convert(Property), Constant);
		}
	}
}
