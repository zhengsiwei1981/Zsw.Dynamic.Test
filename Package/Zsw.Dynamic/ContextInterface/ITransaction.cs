using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zsw.Dynamic.ContextInterface
{
    public interface ITransaction
    {
        void Commit();
        void Rallback();
    }
}
