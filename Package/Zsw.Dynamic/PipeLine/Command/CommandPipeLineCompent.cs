using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Object;
using Zsw.Dynamic.Paramers;

namespace Zsw.Dynamic.PipeLine.Command
{
    public class CommandPipeLineCompent : ICommandPipeLineCompent
    {
        private ITransaction Transaction
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnBeginTransaction(OperationContext operationContext)
        {
            this.Transaction = operationContext.Provder.Transaction();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnClose(OperationContext operationContext)
        {
            operationContext.Provder.ConnectionClose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnCommit(OperationContext operationContext)
        {
            this.Transaction.Commit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnEntityInit(OperationContext operationContext)
        {
            if (operationContext.OperationType == OperationType.Insert)
            {
                operationContext.Entity = operationContext.Provder.ToObject().CreateEntity(operationContext.EntityName);

                Global.entityInitConfigs.Where(config => config.IsGlobal).ToList().
                    ForEach(config =>
                {
                    config.AssignValue(operationContext.Entity);
                });

                Global.entityInitConfigs.Where(config => config.Match(operationContext.Entity.GetType())).
                    ToList().ForEach(config =>
                {
                    config.AssignValue(operationContext.Entity);
                });

                operationContext.Entity.ObjectFill(operationContext.OperationParamers.obj);
            }
            else if (operationContext.OperationType == OperationType.Update || operationContext.OperationType == OperationType.Delete)
            {
                var queryContext = new QueryContext()
                {
                    Provder = operationContext.Provder,
                    QueryType = QueryType.Get,
                    Table = operationContext.Table,
                    EntityName = operationContext.EntityName,
                    requestParamer = new PageRequestParamer()
                    {
                        QueryParams = new List<QueryParamer>()
                        {
                            new QueryParamer()
                            {
                                Method = 1,
                                Name = operationContext.Table.GetKey(),
                                Value = operationContext.OperationParamers.Id
                            }
                        },
                        Orders = new List<OrderParamer>()
                    }
                };
                queryContext.Queryable = ExpressionFactory.Create(queryContext);
                var result = operationContext.Provder.CreateGetWrapper().ToGet(queryContext);
                if (result.Data != null)
                {
                    operationContext.Entity = result.Data;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnExecuted(OperationContext operationContext)
        {
            operationContext.OperationParamers.Executed.InterfaceExectue<IExecuted>(exec => exec.Execute(operationContext));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnExecuting(OperationContext operationContext)
        {
            operationContext.OperationParamers.Executed.InterfaceExectue<IExecuting>(exec => exec.Execute(operationContext));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnOpen(OperationContext operationContext)
        {
            operationContext.Provder.ConnectionOpen();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public virtual void OnRollback(OperationContext operationContext)
        {
            this.Transaction.Rallback();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationContext"></param>
        public void OnValidation(OperationContext operationContext)
        {
            operationContext.OperationParamers.Validation.InterfaceExectue<IValidation>(validation => validation.Validate(operationContext));
        }
    }
}
