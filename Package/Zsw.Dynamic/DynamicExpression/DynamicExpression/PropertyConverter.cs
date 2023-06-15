using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Zsw.Dynamic
{
	public class PropertyConverter
	{
		public static Expression Convert(MemberExpression Property)
		{
			if ((Property.Member as PropertyInfo).PropertyType.IsGenericType)
			{
				Type type = (Property.Member as PropertyInfo).PropertyType.GetGenericArguments()[0];
				return Expression.Convert(Property, type);
			}
			return Property;
		}
	}
}
