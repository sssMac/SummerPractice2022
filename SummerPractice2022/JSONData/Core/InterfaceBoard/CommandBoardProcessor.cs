using JSONData.CammeraData;
using JSONData.EngineData;
using JSONData.Interfaces;
using Newtonsoft.Json.Linq;

namespace JSONData.Core.CommandConverters
{
   public class CommandBoardProcessor
    {
        string _command;
        IBoardCommandConverter _commandConverter;
        
        public CommandBoardProcessor(string commandJson)
        {
            _command = commandJson;
            _commandConverter = GetCommandConverter();
        }
        public byte[] GetCommandBinPackage()
        {
            return _commandConverter.GetCommandBinPackage(_command);
        }
        public string GetAnswerJson(byte[] answerBin)
        {
            return _commandConverter.GetAnswerJson(answerBin);
        }
        private IBoardCommandConverter GetCommandConverter()
        {
          
            byte commandCode = Convert.ToByte(JObject.Parse(_command)["Code"]);

            switch (commandCode)
            {
                case 0x51:
                    return new BaseCommandConverter<EngineCommand>();
                //case 0x62:
                //    return;
                //case 0x65:
                //    return;
                case 0x71: 
                    return new BaseCommandConverter<CameraMoveCommand>();

            }
            return null;
        }


         
    }
}
