namespace gymAPI.Comunes.Classes.Helpers
{
    public interface ICifradoHelper
    {
        public string EncryptString(string plainText);
        public string DecryptString(string cipherText);
    }
}