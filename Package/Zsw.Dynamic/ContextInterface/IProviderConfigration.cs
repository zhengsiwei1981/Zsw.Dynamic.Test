using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.PipeLine;
using Zsw.Dynamic.PipeLine.Command;
using Zsw.Dynamic.PipeLine.Query;

namespace Zsw.Dynamic.ContextInterface
{
    public interface IProviderConfigration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_prefile"></param>
        /// <returns></returns>
        Type ChangeDatabase(object _prefile);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type GetCurrentDatabase();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDynamicProvider AssignDatabaseProvider();
        /// <summary>
        /// 
        /// </summary>
        IExpressionGenerate AssignExpressionGenerater();
        /// <summary>
        /// 
        /// </summary>
        IOrderExpressionGenerate AssignOrderExpressionGenerater();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IPipeLineCompent AssignQueryPipeLineCompent();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICommandPipeLineCompent AssignCommandPipeLineCompent();
    }
}
