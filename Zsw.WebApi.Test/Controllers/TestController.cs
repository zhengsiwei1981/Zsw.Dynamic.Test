using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zsw.Dynamic;
using Zsw.Dynamic.Compent;
using Zsw.Dynamic.Object.LinqToSql;
using Zsw.TestDB.EF;
using Zsw.TestDB.LinqToSQL;

namespace Zsw.WebApi.Test.Controllers
{
    public class TestController : ApiController
    {
        /// <summary>
        /// 以下设置应该配置在Global.asax中
        /// </summary>
        [HttpGet]
        public string UseLinqToSql()
        {
            //使用LinqToSql的配置
            Global.UseConfigration(new Zsw.Dynamic.Object.LinqToSql.ProviderConfigration());
            //应用LinqToSql数据库上下文对象
            Global.ChangeDatabase(typeof(DataClasses1DataContext));
            //这条语句用于配置实现AOP类型时将会对类型进行查找的程序集名称
            Global.AssemblyNameList.AddRange(new List<string>() { "Zsw.WebApi.Test" });

            //应用全局配置初始化实体类，任何为CreateDate的字段都会在插入时初始化为当前日期
            //可以指定具体类型进行初始化
            var entityInitConfig = new EntityInitConfig(true) {};
            entityInitConfig.AddField(new InitField() { Name = "CreateDate", ProviderValue = () => DateTime.Now });
            Global.AttachEntityInitConfig(entityInitConfig);

            return "成功";
        }
        /// <summary>
        /// 以下设置应该配置在Global.asax中
        /// </summary>
        [HttpGet]
        public string UseEntityFramework()
        {
            //使用LinqToSql的配置
            Global.UseConfigration(new Zsw.Dynamic.Object.EntityFramework.ProviderConfigration());
            //应用LinqToSql数据库上下文对象
            Global.ChangeDatabase(typeof(SchoolEntities));
            //这条语句用于配置实现AOP类型时将会对类型进行查找的程序集名称,如果不指定将会到入口程序集的根目录下寻找匹配的类型
            Global.AssemblyNameList.AddRange(new List<string>() { "Zsw.WebApi.Test" });

            //应用全局配置初始化实体类，任何为CreateDate的字段都会在插入时初始化为当前日期
            //可以指定具体类型进行初始化
            var entityInitConfig = new EntityInitConfig(true) { };
            entityInitConfig.AddField(new InitField() { Name = "CreateDate", ProviderValue = () => DateTime.Now });
            Global.AttachEntityInitConfig(entityInitConfig);

            return "成功";
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object ToList()
        {
            return new DynamicProvider().ToQuery();
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object ToGet()
        {
            return new DynamicProvider().ToGet();
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Insert()
        {
            return new DynamicCommand().Insert();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Update()
        {
            return new DynamicCommand().Update();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Delete()
        {
            return new DynamicCommand().Delete();
        }
    }
}
