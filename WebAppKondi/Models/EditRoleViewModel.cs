using System;
using System.Collections.Generic;

namespace WebAppCoreMVC.Models
{
    public class EditRoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Toutes les permissions disponibles
        public List<PermissionCheckboxItem> AvailablePermissions { get; set; } = new();

        // Permissions actuellement associées (caché dans les checkboxes cochées)
        public List<int> SelectedPermissionIds { get; set; } = new();
    }
}
