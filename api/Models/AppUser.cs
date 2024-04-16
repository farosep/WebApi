using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{

    public class AppUser : IdentityUser
    {
        // под капотом у айдентити юзера есть логин пароль

        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

        public List<PLUserModel> PLUserModel { get; set; } = new List<PLUserModel>();
    }
}