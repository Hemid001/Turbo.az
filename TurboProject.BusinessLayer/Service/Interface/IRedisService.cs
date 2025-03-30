using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IRedisService
    {
        Task SetUserAsync(string key, object value, int expirationMinutes);
        Task<T?> GetUserAsync<T>(string key);
        Task DeleteUserAsync(string key);

    }
}
