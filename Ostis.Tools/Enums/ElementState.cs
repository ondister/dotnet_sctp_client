using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostis.Sctp.Tools
{
    public enum ElementState:byte
    {
        UnKnown=0,
        Synchronized=1,
        New=2,
        Edited=3,
        Deleted=4,
    }
}
