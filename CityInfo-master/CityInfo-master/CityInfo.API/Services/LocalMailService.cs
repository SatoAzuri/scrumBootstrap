using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly ILogger<LocalMailService> _logger;
        public LocalMailService(ILogger<LocalMailService> logger)
        {
            _logger = logger;

        }
       

        private string _mailTo = "admin@mycompany.com";
        private string _mailFrom = "noreply@mycompany.com";
        

        public void Send(string to, string subject, string body)
        {
            _logger.LogInformation($"To: {to} Subject {subject} Body: {body}");
            //send mail output to the bebug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {body}");
        }
    }
}
