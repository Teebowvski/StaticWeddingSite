using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace OusMarieWedding.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
