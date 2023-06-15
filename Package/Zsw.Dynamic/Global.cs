using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zsw.Dynamic.Compent;
using Zsw.Dynamic.ContextInterface;
using Zsw.Dynamic.PipeLine.Command;
using Zsw.Dynamic.PipeLine.Query;

namespace Zsw.Dynamic
{
    public class Global
    {
        internal static IExpressionGenerate expressionGenerate = null;
        internal static IOrderExpressionGenerate orderExpressionGenerate = null;
        internal static IPipeLineCompent PipeLineCompent = null;
        internal static IProviderConfigration Configration = null;
        internal static List<EntityInitConfig> entityInitConfigs = new List<EntityInitConfig>();
        public static AssemblyNameSpaceLoader AssemblyNameList = new AssemblyNameSpaceLoader();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configration"></param>
        public static void UseConfigration(IProviderConfigration configration)
        {
            Configration = configration;
            expressionGenerate = Configration.AssignExpressionGenerater();
            orderExpressionGenerate = Configration.AssignOrderExpressionGenerater();
            PipeLineCompent = Configration.AssignQueryPipeLineCompent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_prefile"></param>
        public static void ChangeDatabase(object _prefile)
        {
            Configration.ChangeDatabase(_prefile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeLineCompent"></param>
        public static void ChangePipeLineCompent(IPipeLineCompent _pipeLineCompent)
        {
            PipeLineCompent = _pipeLineCompent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void AttachEntityInitConfig(EntityInitConfig config)
        {
            entityInitConfigs.Add(config);
        }
     
    }
}
