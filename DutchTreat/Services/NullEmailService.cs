using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class NullEmailService : IEmailService
    {
        private readonly ILogger _logger;

        public NullEmailService(ILogger<NullEmailService> logger)
        {
            _logger = logger;
        }

        public void SendEmail(string to, string subject, string body)
        {
            // Log the message
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}
