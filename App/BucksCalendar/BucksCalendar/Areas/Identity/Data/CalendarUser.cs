using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BucksCalendar.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CalendarUser class
    public class CalendarUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public byte[] Image { get; set; }
    }
}
