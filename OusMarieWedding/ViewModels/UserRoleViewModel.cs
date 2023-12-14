using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace OusMarieWedding.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSelected { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<IdentityUser> user { get; set; }
    }
}
