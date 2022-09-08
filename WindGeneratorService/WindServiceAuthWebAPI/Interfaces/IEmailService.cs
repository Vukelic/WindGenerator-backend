using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceAuthWebAPI.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string message, string mailFrom, string displayName, string emailApiKey);
    }
}
