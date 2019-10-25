using System.Collections;
using System.Collections.Generic;

namespace BucksCalendar.Models
{
    public class User
    {
        public int ID { get; set; }
        public string RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}