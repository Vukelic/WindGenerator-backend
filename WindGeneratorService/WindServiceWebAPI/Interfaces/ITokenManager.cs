using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WindServiceWebAPI.Interfaces
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        Task<bool> DeactivateCurrentAsync();
        Task<bool> IsActiveAsync(string token);
        Task<bool> DeactivateAsync(string token);
        string GetCurrentToken();
    }
}
