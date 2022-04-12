using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itransition.Areas.Identity.ApplicationIdentityUser
{
    [Table("Users", Schema = "data")]
    public class AppIdentityUser : IdentityUser
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastSeenOnSite { get; set; }
        public AppIdentityUser()
        {
            RegistrationDate = DateTime.Now;
            Status = "Unblocked";
        }
    }
}
