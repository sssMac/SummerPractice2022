using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData.EngineData
{
    public class EngineCommand : BoardCommand
    {
        public EngineCommand()
        {
            Code = 0x51;
        }
        public byte AnswerIsRequired { get => 0x01; }

        public List<EngineControl> Engines { get; set; }
    }
}
