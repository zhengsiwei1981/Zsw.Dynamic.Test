using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zsw.Dynamic.Context;
using Zsw.Dynamic.Object;

namespace Zsw.WebApi.Test.Aop
{
    public class StudentExecuted : IExecuted
    {
        public void Execute(OperationContext context)
        {
            context.Result.Result = "执行后的动作";
        }
    }
}