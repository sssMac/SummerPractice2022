using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData.EngineData
{
    public class EngineControl
    {
        public byte EngineNumber { get; set; }
        public byte Direct { get; set; }
        public byte Speed { get; set; }
        public UInt16 WorkTimeMs { get; set; }
    }
}
