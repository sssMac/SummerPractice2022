using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData
{
    public abstract class BoardCommand
    {
        public byte Code { get; protected set; }
        public BoardCommand()
        {
            Code = 0x00;
        }
    }
}
