using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Method;
using Zsw.Dynamic.Object.LinqToSql.Method;
using Zsw.Dynamic.Object.LinqToSql.Query;

namespace Zsw.Dynamic.Object.LinqToSql
{
    public class Table : ContextInterface.ITable
    {
        internal PropertyInfo TableInfo
        {
            get;
            private set;
        }
        internal object ContextInstance
        {
            get;
            private set;
        }
        private object TableInstance
        {
            get; set;
        }
        public Table(PropertyInfo info, object instance)
        {
            this.TableInfo = info;
            this.ContextInstance = instance;

            this.TableInstance = this.TableInfo.GetValue(instance);
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsView
        {
            get
            {
                return this.TableInfo.Name.Contains("vw");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Type Type
        {
            get
            {
                return this.TableInfo.PropertyType;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Type EntityType
        {
            get
            {
                return this.TableInstance.GetType().GetGenericArguments()[0];
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //public IColumn GetColumn(string name)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public List<IColumn> GetColumns()
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            var key = this.EntityType.GetProperties().Where(p => p.GetCustomAttribute<System.Data.Linq.Mapping.ColumnAttribute>().IsPrimaryKey == true).FirstOrDefault();
            if (key != null)
            {
                return key.Name;
            }
            key = this.EntityType.GetProperty("Id");
            if (key != null)
            {
                return key.Name;
            }
            var keyName = this.TableInfo.Name + "_Id";
            key = this.EntityType.GetProperty(keyName);
            if (key != null)
            {
                return key.Name;
            }
            throw new Exception("未找到对象的主键！");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaExpression"></param>
        /// <returns></returns>
        public IWhere GetWhere(LambdaExpression lambdaExpression)
        {
            var methodInfo = typeof(Queryable).GetMethods().Where(m => m.Name == "Where"
           && m.GetParameters()[1].ToString() == "System.Linq.Expressions.Expression`1[System.Func`2[TSource,System.Boolean]] predicate").FirstOrDefault();
            var genericMethodInfo = methodInfo.MakeGenericMethod(this.EntityType);

            return new Where(this.TableInstance, genericMethodInfo, lambdaExpression);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IInsert GetInsert()
        {
            return new Insert(this.TableInstance, this.TableInstance.GetType().GetMethod("InsertOnSubmit"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDelete GetDelete()
        {
            return new Delete(this.TableInstance,this.TableInstance.GetType().GetMethod("DeleteOnSubmit"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IUpdate GetUpdate()
        {
            return new Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            return this.TableInstance;
        }
    }
}
