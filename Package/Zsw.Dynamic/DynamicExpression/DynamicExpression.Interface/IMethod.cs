using System.Linq.Expressions;

namespace Zsw.Dynamic.Interface
{
	public interface IMethod
	{
		Expression Build<E>(MemberExpression Property, ConstantExpression Constant) where E : class, new();
	}
}
