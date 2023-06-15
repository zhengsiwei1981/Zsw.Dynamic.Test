using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;

namespace Zsw.Dynamic.PipeLine.Command
{
    public interface ICommandPipeLineCompent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnValidation(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnEntityInit(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnOpen(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnBeginTransaction(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnExecuting(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnExecuted(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnCommit(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnClose(OperationContext operationContext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        void OnRollback(OperationContext operationContext);
    }
}
