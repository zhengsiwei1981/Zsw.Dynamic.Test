using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Zsw.Dynamic
{
	public class DynamicOrder<T> where T : class
	{
		private OrderbyDictionary _orderByArguments;

		public OrderbyDictionary OrderByArguments
		{
			get
			{
				if (_orderByArguments == null)
				{
					_orderByArguments = new OrderbyDictionary();
				}
				return _orderByArguments;
			}
			private set
			{
				_orderByArguments = value;
			}
		}

		public Expression Expression
		{
			get;
			private set;
		}

		public IQueryable<T> Source
		{
			get;
			private set;
		}

		public DynamicOrder<T> Load(IQueryable<T> _source)
		{
			Source = _source;
			Expression = _source.Expression;
			return this;
		}

		public DynamicOrder<T> By()
		{
			ConstractorMember();
			return this;
		}

		public DynamicOrder<T> By(OrderbyDictionary _orderByArguments)
		{
			OrderByArguments = _orderByArguments;
			ConstractorMember();
			return this;
		}

		public IOrderedQueryable<T> Generate()
		{
			int index = OrderByArguments.Count - 1;
			ToQueryable(index);
			Expression = Source.Expression;
			return Source as IOrderedQueryable<T>;
		}

		private void ToQueryable(int index)
		{
			if (index != -1)
			{
				KeyValuePair<OrderByArgument, OrderByType> keyValuePair = OrderByArguments.ElementAt(index);
				FieldInfo field = typeof(OrderByType).GetField(keyValuePair.Value.ToString());
				if (!field.IsDefined(typeof(OrderByTypeDescriptorAttribute), inherit: false))
				{
					throw new System.Exception("未指定排序方法的映射");
				}
				OrderByTypeDescriptorAttribute orderByTypeDescriptorAttribute = (OrderByTypeDescriptorAttribute)field.GetCustomAttributes(typeof(OrderByTypeDescriptorAttribute), inherit: false).ElementAt(0);
				string methodMap = orderByTypeDescriptorAttribute.MethodMap;
				PropertyInfo property = typeof(T).GetProperty(keyValuePair.Key.PropertyName);
				Source = Source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), methodMap, new Type[2]
				{
					typeof(T),
					property.PropertyType
				}, Source.Expression, Expression.Quote(keyValuePair.Key.MemberExpression)));
				ToQueryable(--index);
			}
		}

		private void ConstractorMember()
		{
			OrderByArguments.ToList().ForEach(delegate(KeyValuePair<OrderByArgument, OrderByType> kv)
			{
				kv.Key.MemberExpression = DynamicCreateMember(kv.Key.PropertyName);
			});
		}

		public Expression CreateMemberExpression<TKey>(string pName)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "T");
			MemberExpression body = Expression.Property(parameterExpression, pName);
			return Expression.Lambda<Func<T, TKey>>(body, new ParameterExpression[1]
			{
				parameterExpression
			});
		}

		private Expression DynamicCreateMember(string pName)
		{
			PropertyInfo property = typeof(T).GetProperty(pName);
			MethodInfo methodInfo = GetType().GetMethod("CreateMemberExpression").MakeGenericMethod(property.PropertyType);
			return methodInfo.Invoke(this, new string[1]
			{
				pName
			}) as Expression;
		}
	}
}
