using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WebAppCoreMVC.Entities;

namespace WebAppCoreMVC.Models
{
    public class UserViewModel
    {
        public string SelectedRole { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
