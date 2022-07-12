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
        public void SetCommandOperands(BoardCommand command, List<byte> commandOperands)
        {
            if (command is EngineCommand)
            {
                SetCommandOperands((EngineCommand)command, commandOperands);
                return;
            }
            if (command is CameraMoveCommand)
            {
                SetCommandOperands((CameraMoveCommand)command, commandOperands);
                return;
            }
        }
        public void SetCommandOperands(EngineCommand command, List<byte> commandOperands)
        {
            foreach (var ec in command.Engines)
            {
                commandOperands.Add(ec.EngineNumber);
                commandOperands.Add(ec.Direct);
                commandOperands.Add(ec.Speed);
                commandOperands.AddRange(BitConverter.GetBytes(ec.WorkTimeMs));
            }
        }
        public void SetCommandOperands(CameraMoveCommand command, List<byte> commandOperands)
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
