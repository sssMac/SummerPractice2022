using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP.Interfaces
{
    public interface ICOMport
    {
        void COMconnect();
        void COMdisconnect();
        public void COMwrite(byte[] data, int offset, int count);
    }
}
