using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Paramers;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic
{
    public class DynamicProvider
    {
        /// <summary>
        /// 
        /// </summary>
        internal PageRequestParamer PageRequestParamer
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
                return this.PageRequestParamer != null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal QueryContext QueryContext
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        internal IQueryable Queryable
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageRequestParamer">查询参数</param>
        public DynamicProvider(PageRequestParamer pageRequestParamer)
        {
            this.PageRequestParamer = pageRequestParamer;
            this.QueryInitialization();
        }
        /// <summary>
        /// 不传入查询参数，但必须从请求中获取格式正确的查询参数
        /// </summary>
        public DynamicProvider()
        {
            this.QueryInitialization();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        public DynamicProvider(IQueryable queryable, int pageNumber, int pageIndex)
        {
            this.Queryable = queryable;
            this.PageRequestParamer = new PageRequestParamer() { EntityName = this.Queryable.ElementType.Name, EachPageNumber = pageNumber, Index = pageIndex };
            this.QueryInitialization();

            this.QueryContext.Queryable = this.Queryable;
        }
        /// <summary>
        /// 
        /// </summary>
        private void QueryInitialization()
        {
            if (!this.IsParamerTrans)
            {
                if (System.Web.HttpContext.Current != null)
                {
                    //从请求中获取查询参数
                    using (var sr = new StreamReader(System.Web.HttpContext.Current.Request.InputStream))
                    {
                        var json = sr.ReadToEnd();
                        this.PageRequestParamer = JsonConvert.DeserializeObject<PageRequestParamer>(json);
                        if (this.PageRequestParamer == null)
                        {
                            throw new Exception("Http请求的参数不正确！");
                        }
                    }
                }
            }
            this.QueryContext = ContextFactory.Create(this.PageRequestParamer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryResult ToQuery()
        {
            Global.PipeLineCompent.Execute(this.QueryContext);
            return this.QueryContext.Result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryResult ToGet()
        {
            this.QueryContext.QueryType = QueryType.Get;
            Global.PipeLineCompent.Execute(this.QueryContext);
            return this.QueryContext.Result;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="operationParamers"></param>
        ///// <returns></returns>
        //public static DynamicCommand CreateCommand(OperationParamers operationParamers)
        //{
        //    return new DynamicCommand(operationParamers);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public static DynamicCommand CreateCommand()
        //{
        //    return new DynamicCommand();
        //}
    }
}
