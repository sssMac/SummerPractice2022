using JSONData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData.CammeraData
{
    public class CameraMoveCommand : BoardCommand
    {
        public CameraMoveCommand()
        {
            Code = 0x71;
        }
        public byte CameraNumber { get; set; }
        public byte AnswerIsRequired { get => 0x01; }
        public List<CameraMoveControl> CameraMoveDir { get; set; }
    }
}
