using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.InterfaceObject;
using Zsw.Dynamic.Paramers;

namespace Zsw.Dynamic
{
    public class DefaultExpressonGenerater : IExpressionGenerate
    {
        public LambdaExpression Create(QueryContext context)
        {
            var lambadWithGeneric = typeof(Lambad<>).MakeGenericType(context.Table.EntityType != null ? context.Table.EntityType : context.Table.Type);
            var instance = Activator.CreateInstance(lambadWithGeneric);

            this.CreateAndExpression(instance, context.requestParamer.QueryParams.Where(q => q.IsOr == false).ToList(), context.Table.GetKey());
            this.CreateOrExpression(instance, context.requestParamer.QueryParams.Where(q => q.IsOr == true).ToList(), context);

            return (LambdaExpression)instance.GetType().GetMethod("Create").Invoke(instance, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        private void CreateAndExpression(object instance, List<QueryParamer> andParamers, string key)
        {
            var andMethod = instance.GetType().GetMethod("WithAnd", new Type[] { typeof(QueryItem) });
            if (andParamers.Count > 0)
            {
                andParamers.ForEach(item =>
                {
                    andMethod.Invoke(instance,
                        new object[] { new QueryItem(item.Name, item.Value,
                        MethodFactory.CreateMethodEntity(item.Method))
                        });
                });
            }
            else
            {
                andMethod.Invoke(instance, new object[] { new QueryItem(key, -1, new NotEqual()) });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="andParamers"></param>
        private void CreateOrExpression(object andinstance, List<QueryParamer> orParamers, QueryContext context)
        {
            if (orParamers.Count > 0)
            {
                var lambadWithGeneric = typeof(Lambad<>).MakeGenericType(context.Table.EntityType != null ? context.Table.EntityType : context.Table.Type);
                var instance = Activator.CreateInstance(lambadWithGeneric);

                var orMethod = instance.GetType().GetMethod("WithOr", new Type[] { typeof(QueryItem) });
                orParamers.ForEach(p =>
                {
                    var queryItem = new QueryItem(p.Name, p.Value, MethodFactory.CreateMethodEntity(p.Method));
                    orMethod.Invoke(instance, new object[] { queryItem });
                });
                var expr = instance.GetType().GetField("Expr").GetValue(instance);
                var visitMethod = andinstance.GetType().GetMethods().Where(m => m.Name == "WithAnd" && m.GetParameters()[0].ParameterType == typeof(Expression)).FirstOrDefault();
                visitMethod.Invoke(andinstance, new object[] { expr });
            }
        }
    }
}
