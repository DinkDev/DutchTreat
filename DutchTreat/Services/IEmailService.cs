namespace DutchTreat.Services
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}