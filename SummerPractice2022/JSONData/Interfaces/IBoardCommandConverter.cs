namespace JSONData.Interfaces
{
   public interface IBoardCommandConverter 
    {
        public byte[] GetCommandBinPackage(string jsonString);

        string GetAnswerJson(byte[] answerBin);
    }
}
