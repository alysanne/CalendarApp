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

        public async Task OnGetAsync(int? year, int? month)
        {
            var calendarYear = year ?? DateTime.Today.Year;
            var calendarMonth = month ?? DateTime.Today.Month;

            /* Dates for query results delimitation */
            var firstDayMonth = new DateTime(calendarYear, calendarMonth, 1);
            var lastDayMonth = new DateTime(calendarYear, calendarMonth, DateTime.DaysInMonth(calendarYear, calendarMonth), 23, 59, 59);
            
            var query = from ev in _context.Events
                        where 
                            ((ev.StartDateTime.Month == calendarMonth && ev.StartDateTime.Year == calendarYear) || 
                             (ev.EndDateTime.Month == calendarMonth && ev.EndDateTime.Year == calendarYear) ||
                             (ev.StartDateTime < firstDayMonth && ev.EndDateTime > lastDayMonth ))
                        select ev;
            
            Event = await query
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.Notification)
                .ToListAsync();
        }
    }
}
