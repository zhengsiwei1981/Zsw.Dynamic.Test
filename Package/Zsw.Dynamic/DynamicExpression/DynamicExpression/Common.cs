using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Zsw.Dynamic
{
	internal class Common<E> where E : class, new()
	{
		private ParameterExpression Parameter = null;

		private string Name
		{
			get;
			set;
		}

		private object Value
		{
			get;
			set;
		}

		internal Common()
		{
		}

		internal Common(string _name, object _value)
		{
			Set(_name, _value);
		}

		public void Set(string _name, object _value)
		{
			Name = _name;
			Value = _value;
		}

		internal ParameterExpression GetParameter()
		{
			if (Parameter == null)
			{
				Parameter = Expression.Parameter(typeof(E), "obj");
			}
			return Parameter;
		}

		internal MemberExpression GetProperty()
		{
			PropertyInfo property = typeof(E).GetProperty(Name);
			if (property == null)
			{
				throw new System.Exception("不存在的属性");
			}
			return Expression.Property(GetParameter(), property);
		}

		internal ConstantExpression GetConstant()
		{
			return Expression.Constant(Value, Value.GetType());
		}
	}
}
