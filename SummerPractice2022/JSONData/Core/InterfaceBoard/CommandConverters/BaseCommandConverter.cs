using JSONData;
using JSONData.Interfaces;
using JSONData.Models;
using Newtonsoft.Json;

namespace JSONData.Core.CommandConverters
{
	public class BaseCommandConverter<T> : BoardCommandConverter, IBoardCommandConverter where T : BoardCommand
    {
		public byte[] GetCommandBinPackage(string jsonString)
		{
            var command = JsonConvert.DeserializeObject<T>(jsonString);
            var commandOperands = GetEngineCommandOperands(command);
            var deviceCredentials = GetDeviceCredentials();
            TransferPackage package = new TransferPackage()
            {
                IdBoard = deviceCredentials.IdBoard,
                IdDevice = deviceCredentials.IdDevice,
                Command = 0x30,
                AnswerIsRequired = command.AnswerIsRequired,
                CommandOperands = commandOperands,
                CommandLength = (UInt16)(commandOperands.Length + 7),
                FullPackageLength = 64
            };

            return GetBinPackage(package);
        }
		public string GetAnswerJson(byte[] answerBin)
		{
			throw new NotImplementedException();
		}
        private byte[] GetEngineCommandOperands(BoardCommand command)
		{
            List<byte> commandOperands = new List<byte>();
            
            commandOperands.Add(command.Code);
            command.SetCommandOperands(command, commandOperands);
            return commandOperands.ToArray();
        }

    }
}
