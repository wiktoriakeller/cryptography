namespace CryptographyAPI.Services
{
    public interface IAlgorithmService<T>
    {
        public bool PrepareData(T data);
        public string Encipher(T data);
        public string Decipher(T data);
    }
}
