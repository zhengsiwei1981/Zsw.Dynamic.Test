using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Method;

namespace Zsw.Dynamic.ContextInterface
{
    public interface ITable
    {
        /// <summary>
        /// 是否是视图
        /// </summary>
        bool IsView
        {
            get;
        }
        /// <summary>
        /// 表的实际类型
        /// </summary>
        Type Type
        {
            get;
        }
        /// <summary>
        /// 如果是泛型对象表的话，此属性则代表泛型类型
        /// </summary>
        Type EntityType
        {
            get;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IWhere GetWhere(LambdaExpression lambdaExpression);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IInsert GetInsert();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDelete GetDelete();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IUpdate GetUpdate();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object ToObject();
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //List<IColumn> GetColumns();
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //IColumn GetColumn(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetKey();
    }
}
