using System.Linq.Expressions;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic.InterfaceObject
{
	public class NotEqual : IMethod
	{
		public Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new()
		{
			return Expression.NotEqual(PropertyConverter.Convert(Property), Constant);
		}
	}
}
