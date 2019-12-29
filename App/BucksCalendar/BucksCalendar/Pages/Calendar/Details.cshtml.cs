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
    public class DetailsModel : PageModel
    {
        private readonly BucksCalendar.Data.DatabaseContext _context;

        public DetailsModel(BucksCalendar.Data.DatabaseContext context)
        {
            _context = context;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(e => e.User).Include(e => e.Category).FirstOrDefaultAsync(m => m.EventID == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
