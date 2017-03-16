namespace OnlineMarket.Utilities.Interfaces
{
    public interface IEncryptionService
    {
        string CreateSalt();

        string EncryptPassword(string password, string salt);
    }
}
