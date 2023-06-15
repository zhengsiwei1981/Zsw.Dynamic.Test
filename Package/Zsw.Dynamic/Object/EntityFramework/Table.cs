using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Method;
using Zsw.Dynamic.Object.EntityFramework.Method;

namespace Zsw.Dynamic.Object.EntityFramework
{
    public class Table : ContextInterface.ITable
    {
        /// <summary>
        /// 
        /// </summary>
        internal PropertyInfo TableInfo
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal object ContextInstance
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        private object TableInstance
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="instance"></param>
        public Table(PropertyInfo info, object instance)
        {
            this.TableInfo = info;
            this.ContextInstance = instance;

            this.TableInstance = this.TableInfo.GetValue(instance);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsView
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDelete GetDelete()
        {
            return new Delete(this.TableInstance, this.TableInstance.GetType().GetMethod("Remove"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IInsert GetInsert()
        {
            return new Insert(this.TableInstance, this.TableInstance.GetType().GetMethod("Add"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            var keyName = "";
            var keyObj = this.GetType().GetMethod("TGetKey").MakeGenericMethod(this.EntityType).Invoke(this, null);
            PropertyInfo key = null;

            if (keyObj != null)
            {
                keyName = keyObj.ToString();
                key = this.EntityType.GetProperty(keyName);
                if (key != null)
                {
                    return key.Name;
                }
            }            
            key = this.EntityType.GetProperty("Id");
            if (key != null)
            {
                return key.Name;
            }
            keyName = this.TableInfo.Name + "_Id";
            key = this.EntityType.GetProperty(keyName);
            if (key != null)
            {
                return key.Name;
            }
            keyName = this.TableInfo.Name + "Id";
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
        /// <returns></returns>
        public string TGetKey<TEntity>() where TEntity : class
        {
            var set = ((IObjectContextAdapter)this.ContextInstance).ObjectContext.CreateObjectSet<TEntity>();
            if (set.EntitySet.ElementType.KeyMembers.Count > 0)
            {
                return set.EntitySet.ElementType.KeyMembers[0].Name;
            }
            else
            {
                return null;
            }
        }
        public IUpdate GetUpdate()
        {
            return new Update();
        }

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
        public object ToObject()
        {
            return this.TableInstance;
        }
    }
}
