using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.ContextInterface
{
    public interface IColumn
    {
        string Name
        {
            get;set;
        }
        Type Type
        {
            get;set;
        }
    }
}
