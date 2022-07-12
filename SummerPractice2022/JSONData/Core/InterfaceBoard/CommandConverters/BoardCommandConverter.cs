using JSONData;
using JSONData.Models;

namespace JSONData.Core.CommandConverters
{
   public abstract class BoardCommandConverter
    {
        
        public byte[] GetBinPackage(TransferPackage transferPackage)
        {
            List<byte> package = new List<byte>()
            {
                transferPackage.IdDevice,
                transferPackage.IdBoard,
                transferPackage.Command,
                transferPackage.AnswerIsRequired
            };
            package.AddRange(BitConverter.GetBytes(transferPackage.CommandLength));
            package.AddRange(transferPackage.CommandOperands);
            package.AddRange(new byte[transferPackage.FullPackageLength - package.Count]);
            package.Add(GetCheckSum(package));

            return package.ToArray();
        }

        public  byte GetCheckSum(List<byte> dataArray)
        {
            byte check = 0x00;
            foreach (var b in dataArray)
                check ^= b;
            return check;
        }

        public DeviceCredentials GetDeviceCredentials()
        {
          //TODO  Чтение данных их конфига
          return  new DeviceCredentials()
            {
                IdBoard = 0xd,
                IdDevice = 0x01
            };

        }


    }
}
