using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Object;

namespace Zsw.Dynamic.PipeLine.Query
{
    public class PipeLineCompent : IPipeLineCompent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public virtual void OnValidation(QueryContext context)
        {
            if (string.IsNullOrEmpty(context.requestParamer.EntityName))
            {
                throw new Exception("对象名不能为空！");
            }
            if (context.requestParamer.Index < 0)
            {
                throw new Exception("页索引不能小于0");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnParamersProcess(QueryContext context)
        {
            if (context.requestParamer.QueryParams == null)
            {
                return;
            }
            context.requestParamer.QueryParams = context.requestParamer.QueryParams.Where(q => q.Value != null).ToList();
            context.requestParamer.QueryParams.ForEach(q =>
            {
                if (q.Value.GetType() == typeof(JArray))
                {
                    var array = q.Value as JArray;
                    Type type = null;
                    if (array.Count == 0)
                    {
                        q.Value = null;
                        return;
                    }
                    else
                    {
                        switch (array[0].Type)
                        {
                            case JTokenType.Integer:
                                type = typeof(int);
                                break;
                            case JTokenType.String:
                                type = typeof(string);
                                break;
                        }
                    }

                    var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                    var callmethod = typeof(Extensions).GetMethod("Value", new Type[] { typeof(IEnumerable<JToken>) }, null);
                    var addmethod = list.GetType().GetMethod("Add");

                    for (int i = 0; i < array.Count; i++)
                    {
                        var token = array[i];
                        var value = callmethod.MakeGenericMethod(type).Invoke(null, new object[] { token });
                        addmethod.Invoke(list, new object[] { value });
                    }

                    q.Value = list;
                }
                if (q.Value.GetType() == typeof(long))
                {
                    q.Value = Convert.ToInt32(q.Value);
                }
                DateTime dt;
                if (DateTime.TryParse(q.Value.ToString(), out dt))
                {
                    q.Value = dt;
                }
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        public virtual void OnCreateExpression(QueryContext context)
        {
            if (context.Queryable == null)
            {
                context.Queryable = ExpressionFactory.Create(context);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnQueryResult(QueryContext context)
        {
            if (context.QueryType == QueryType.List)
            {
                context.Result = context.Provder.CreateListWrapper().ToQueryResult(context);
            }
            else
            {
                context.Result = context.Provder.CreateGetWrapper().ToGet(context);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnFill(QueryContext context)
        {
            context.requestParamer.FillMethod.InterfaceExectue<IFill>(fill => fill.Fill(context));
        }
        /// <summary>
        /// 
        /// </summary>
        public void Execute(QueryContext context)
        {
            this.OnValidation(context);
            this.OnParamersProcess(context);
            this.OnCreateExpression(context);
            this.OnQueryResult(context);
            this.OnFill(context);
        }
    }
}
