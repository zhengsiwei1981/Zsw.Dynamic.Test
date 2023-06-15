using System.Linq.Expressions;

namespace Zsw.Dynamic
{
	public class OrderByArgument
	{
		public string PropertyName
		{
			get;
			set;
		}

		internal Expression MemberExpression
		{
			get;
			set;
		}

		public OrderByArgument(string pName)
		{
			PropertyName = pName;
		}
	}
}
