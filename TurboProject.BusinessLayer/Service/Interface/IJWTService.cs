using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboProject.DataLayer.Entity;

namespace TurboProject.BusinessLayer.Service.Interface
{
    public interface IJWTService
    {
        string GenerateToken(User user, List<string> roles);
    }
}
