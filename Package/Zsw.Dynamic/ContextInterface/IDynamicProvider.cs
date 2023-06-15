using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Query.Interface;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.ContextInterface
{
    public interface IDynamicProvider
    {
        /// <summary>
        /// 获取表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ITable GetTable(string name);
        /// <summary>
        ///获取表列表 
        /// </summary>
        /// <returns></returns>
        List<ITable> GetTables();
        /// <summary>
        /// 获取原始对象
        /// </summary>
        /// <returns></returns>
        object ToObject();
        /// <summary>
        /// 创建数据库上下文实例
        /// </summary>
        void Create();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IToList CreateListWrapper();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IGet CreateGetWrapper();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ITransaction Transaction();
        /// <summary>
        /// 
        /// </summary>
        void ConnectionOpen();
        /// <summary>
        /// 
        /// </summary>
        void ConnectionClose();
        /// <summary>
        /// 
        /// </summary>
        void Submit();
    }
}
