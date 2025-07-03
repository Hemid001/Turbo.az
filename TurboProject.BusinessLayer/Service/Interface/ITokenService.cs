using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.BusinessLayer.Model.DTO.Response.Account;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface ITokenService
    {
        Task<LoginResponseDto> GenerateTokensAsync(
           User user,
           IList<string> roles,
           string ipAddress);

        Task<LoginResponseDto> RefreshAsync(
            string refreshToken,
            string ipAddress);
    }
}
