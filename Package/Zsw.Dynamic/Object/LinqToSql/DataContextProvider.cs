using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.Object.LinqToSql.Query;
using Zsw.Dynamic.Query.Interface;
using Zsw.Dynamic.Result;

namespace Zsw.Dynamic.Object.LinqToSql
{
    public class DataContextProvider : IDynamicProvider
    {
        public DataContextProvider()
        {
            if (this.DataContext == null)
            {
                this.Create();
            }
        }
        System.Data.Linq.DataContext DataContext
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Create()
        {
            this.DataContext = (System.Data.Linq.DataContext)Activator.CreateInstance(Global.Configration.GetCurrentDatabase());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ToObject()
        {
            return this.DataContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ITable> GetTables()
        {
            var tableProperties = this.DataContext.GetType().GetProperties().Where(p => p.PropertyType.Name == "Table`1").ToList();
            var tables = new List<ITable>();
            tableProperties.ForEach(info =>
            {
                tables.Add(new Table(info, this.DataContext));
            });
            return tables;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITable GetTable(string name)
        {
            var tablePropertyInfo = this.DataContext.GetType().GetProperty(name);
            return new Table(tablePropertyInfo, this.DataContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IToList CreateListWrapper()
        {
            return new List();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IGet CreateGetWrapper()
        {
            return new Get();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ITransaction Transaction()
        {
            var trans = new Transcation()
            {
                TransactionInstance = this.DataContext.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable)
            };
            this.DataContext.Transaction = trans.TransactionInstance;
            return trans;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ConnectionOpen()
        {
            this.DataContext.Connection.Open();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ConnectionClose()
        {
            this.DataContext.Connection.Close();
            
        }
        /// <summary>
        /// 
        /// </summary>
        public void Submit()
        {
            this.DataContext.SubmitChanges();
        }
    }
}
