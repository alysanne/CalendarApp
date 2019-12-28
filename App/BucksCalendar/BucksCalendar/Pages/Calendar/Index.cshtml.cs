using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BucksCalendar.Data;
using BucksCalendar.Models;

namespace BucksCalendar.Pages.Calendar
{
    public class IndexModel : PageModel
    {
        private readonly BucksCalendar.Data.DatabaseContext _context;

        public IndexModel(BucksCalendar.Data.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();
        }
    }
}
