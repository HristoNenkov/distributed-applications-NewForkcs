using System.Collections.Generic;

namespace Bakery.Website.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }

        public List<RoleCheckbox> Roles { get; set; } = new();
    }

    public class RoleCheckbox
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
