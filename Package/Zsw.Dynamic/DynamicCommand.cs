using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Object.LinqToSql.Method.Result;
using Zsw.Dynamic.Paramers;
using Zsw.Dynamic.PipeLine.Command;
using Zsw.Dynamic.PipeLine.Query;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic
{
    public class DynamicCommand
    {
        /// <summary>
        /// 
        /// </summary>
        internal OperationParamers OperationParamers
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal OperationContext OperationContext
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal bool IsParamerTrans
        {
            get
            {
                return this.OperationParamers != null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DynamicCommand()
        {
            this.ContextInitialization();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryContext"></param>
        //internal DynamicCommand(QueryContext queryContext)
        //{
        //    this.OperationContext = new OperationContext()
        //    {
        //        EntityName = queryContext.EntityName,
        //        Provder = queryContext.Provder,
        //        Table = queryContext.Table
        //    };
        //    this.ContextInitialization();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramers"></param>
        public DynamicCommand(OperationParamers paramers)
        {
            this.OperationParamers = paramers;
            this.ContextInitialization();
        }
        /// <summary>
        /// 
        /// </summary>
        private void ContextInitialization()
        {
            if (!this.IsParamerTrans)
            {
                if (System.Web.HttpContext.Current != null)
                {
                    //从请求中获取查询参数
                    using (var sr = new StreamReader(System.Web.HttpContext.Current.Request.InputStream))
                    {
                        var json = sr.ReadToEnd();
                        this.OperationParamers = JsonConvert.DeserializeObject<OperationParamers>(json);
                        if (this.OperationParamers == null)
                        {
                            throw new Exception("Http请求的参数不正确！");
                        }
                    }
                }
            }
            if (this.OperationContext == null)
            {
                this.OperationContext = ContextFactory.Create(this.OperationParamers);
            }
            else
            {
                this.OperationContext.OperationParamers = this.OperationParamers;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOperationResult Insert()
        {
            this.OperationContext.OperationType = OperationType.Insert;
            this.PipeLineEventExecute(() =>
            {
                this.OperationContext.Table.GetInsert().Invoke(this.OperationContext);
                this.OperationContext.Provder.Submit();
                this.OperationContext.Result = new OperationResult() { Result = 1 };
            });
            return this.OperationContext.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOperationResult Update()
        {
            this.OperationContext.OperationType = OperationType.Update;
            this.PipeLineEventExecute(() =>
            {
                this.OperationContext.Entity.ObjectFill(this.OperationContext.OperationParamers.obj);
                this.OperationContext.Table.GetUpdate().Invoke(this.OperationContext);
                this.OperationContext.Provder.Submit();
                this.OperationContext.Result = new OperationResult() { Result = 1 };
            });
            return this.OperationContext.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOperationResult Delete()
        {
            this.OperationContext.OperationType = OperationType.Delete;
            this.PipeLineEventExecute(() =>
            {
                this.OperationContext.Table.GetDelete().Invoke(this.OperationContext);
                this.OperationContext.Provder.Submit();
                this.OperationContext.Result = new OperationResult() { Result = 1 };
            });
            return this.OperationContext.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        private void PipeLineEventExecute(Action action)
        {
            ICommandPipeLineCompent commandPipelineCompent = Global.Configration.AssignCommandPipeLineCompent();
            try
            {
                commandPipelineCompent.OnEntityInit(this.OperationContext);
                commandPipelineCompent.OnOpen(this.OperationContext);
                commandPipelineCompent.OnValidation(this.OperationContext);
                commandPipelineCompent.OnBeginTransaction(this.OperationContext);
                commandPipelineCompent.OnExecuting(this.OperationContext);

                action();

                commandPipelineCompent.OnExecuted(this.OperationContext);
                commandPipelineCompent.OnCommit(this.OperationContext);
            }
            catch (Exception ex)
            {
                commandPipelineCompent.OnRollback(this.OperationContext);
                throw ex;
            }
            finally
            {
                commandPipelineCompent.OnClose(this.OperationContext);
            }
        }
    }
}
