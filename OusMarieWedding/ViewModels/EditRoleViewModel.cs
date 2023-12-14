
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OusMarieWedding.ViewModels
{
    public class EditRoleViewModel
    {


        
            public EditRoleViewModel()
            {
                Users = new List<string>();
            }

            public string Id { get; set; }

            [Required(ErrorMessage = "Role Name is required")]
            public string RoleName { get; set; }

            public List<string> Users { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
