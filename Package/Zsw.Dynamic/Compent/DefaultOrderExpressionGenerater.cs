using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;

namespace Zsw.Dynamic
{
    public class DefaultOrderExpressionGenerater : IOrderExpressionGenerate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IOrderedQueryable Create(IQueryable queryable, QueryContext context)
        {
            var dynamicOrderWithGeneric = typeof(DynamicOrder<>).MakeGenericType(context.Table.EntityType != null ? context.Table.EntityType : context.Table.GetType());
            var instance = Activator.CreateInstance(dynamicOrderWithGeneric);
            instance.GetType().GetMethod("Load").Invoke(instance, new object[] { queryable });

            OrderbyDictionary keys = new OrderbyDictionary();
            context.requestParamer.Orders.ForEach(o =>
            {
                keys.Add(new OrderByArgument(o.Name), (OrderByType)o.OrderType);
            });
            var byMethod = instance.GetType().GetMethods().Where(m => m.Name == "By" && m.GetParameters().Count() > 0).FirstOrDefault();
            byMethod.Invoke(instance, new object[] { keys });

            return (IOrderedQueryable)instance.GetType().GetMethod("Generate").Invoke(instance, null);
        }
    }
}
