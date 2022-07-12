using System;

namespace JSONData.Models
{
    public class TransferPackage
    {
        public byte IdBoard { get; set; }
        public byte IdDevice { get; set; }
        public byte Command { get; set; }
        public UInt16 CommandLength { get; set; }
        public byte AnswerIsRequired { get; set; }
        public byte[] CommandOperands { get; set; }
        public byte CheckSum { get; set; }
        public byte[] CommandTxPackage { get; set; }
        public UInt16 FullPackageLength { get; set; }
        public byte GetCheckSum { get; set; }
    }
}