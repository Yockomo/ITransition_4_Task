using System;

namespace Itransition.Models
{
    public class People
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastSeenOnSite { get; set; }
        public string Status { get; set; }
    }
}
