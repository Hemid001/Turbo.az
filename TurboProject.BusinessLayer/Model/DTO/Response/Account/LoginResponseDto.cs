using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboProject.BusinessLayer.Model.DTO.Response.Account
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string RefreshToken { get; set; }
    }
}
