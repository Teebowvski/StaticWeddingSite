
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OusMarieWedding.ViewModels
{
    public class UserViewModel
    {

        public  IEnumerable<IdentityUser> Users { get; set; }
    }
}
