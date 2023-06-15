using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic.PipeLine.Query
{
    public interface IPipeLineCompent
    {
        /// <summary>
        /// 在参数验证时
        /// </summary>
        /// <param name="context"></param>
        void OnValidation(QueryContext context);
        /// <summary>
        /// 在参数预处理时
        /// </summary>
        /// <param name="context"></param>
        void OnParamersProcess(QueryContext context);
        /// <summary>
        /// 在创建表达式时
        /// </summary>
        /// <param name="context"></param>
        void OnCreateExpression(QueryContext context);
        /// <summary>
        /// 在查询结果时
        /// </summary>
        /// <param name="context"></param>
        void OnQueryResult(QueryContext context);
        /// <summary>
        /// 在查询结果后
        /// </summary>
        /// <param name="context"></param>
        void OnFill(QueryContext context);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Execute(QueryContext context);
    }
}
