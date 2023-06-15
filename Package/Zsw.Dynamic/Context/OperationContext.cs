using System.ComponentModel;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Paramers;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationContext : BasicContext
    {
        /// <summary>
        /// 
        /// </summary>
        public OperationParamers OperationParamers
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public OperationType OperationType
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public ITable Table
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public object Entity
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public IOperationResult Result
        {
            get;set;
        }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Insert = 0,
        /// <summary>
        /// 更新
        /// </summary>
        [Description("更新")]
        Update = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2
    }
}
