namespace OnlineMarket.Utilities.Interfaces
{
    public interface ISendEmailService
    {
        void Send(string to, string link);
    }
}