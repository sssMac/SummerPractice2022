using JSONData;
using JSONData.Models;

namespace JSONData.Core.CommandConverters
{
   public abstract class BoardCommandConverter
    {
        
        public byte[] GetBinPackage(TransferPackage transferPackage)
        {
            List<byte> package = new List<byte>();
            package.Add(transferPackage.IdDevice);
            package.Add(transferPackage.IdBoard);
            package.Add(transferPackage.Command);
            package.AddRange(BitConverter.GetBytes(transferPackage.CommandLength));
            package.AddRange(transferPackage.CommandOperands);
            package.Add(transferPackage.AnswerIsRequired);
            package.Add(GetCheckSum(package));
            package.AddRange(new byte[transferPackage.FullPackageLength - package.Count]);

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
