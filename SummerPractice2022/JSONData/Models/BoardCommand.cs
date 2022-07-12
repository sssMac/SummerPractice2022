using JSONData.CammeraData;
using JSONData.EngineData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONData.Models
{
    public abstract class BoardCommand
    {
        public byte Code { get; protected set; }
        public byte AnswerIsRequired { get { return 0x01; } }
        public BoardCommand()
        {
            Code = 0x00;
        }
        public List<byte> commandOperands = new List<byte>();
        public void SetCommandOperands(BoardCommand command)
        {
            if (command is EngineCommand)
            {
                SetCommandOperands((EngineCommand)command);
                return;
            }
            if (command is CameraMoveCommand)
            {
                SetCommandOperands((CameraMoveCommand)command);
                return;
            }
        }
        public void SetCommandOperands(EngineCommand command)
        {
            foreach (var ec in command.Engines)
            {
                commandOperands.Add(ec.EngineNumber);
                commandOperands.Add(ec.Direct);
                commandOperands.Add(ec.Speed);
                commandOperands.AddRange(BitConverter.GetBytes(ec.WorkTimeMs));
            }
        }
        public void SetCommandOperands(CameraMoveCommand command)
        {
            commandOperands.Add(command.CameraNumber);
            foreach (var ec in command.CameraMoveDir)
            {
                commandOperands.Add(ec.Axis);
                commandOperands.Add(ec.MovDeg);

            }
        }
    }
}
