using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BucksCalendar.Data;
using BucksCalendar.Models;
using BucksCalendar.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BucksCalendar.Pages.Calendar
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly BucksCalendar.Data.DatabaseContext _context;

        public IndexModel(BucksCalendar.Data.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get;set; }
        
        public string SelectedMonth { get; set; }
        public string SelectedYear { get; set; }

        public async Task OnGetAsync(int? year, int? month)
        {
            var calendarYear = DateHelpers.CalcNumericYear(year);
            var calendarMonth = DateHelpers.CalcNumericMonth(month);
            SelectedMonth = calendarMonth.ToString();
            SelectedYear = calendarYear.ToString();

            /* Dates for query results delimitation */
            var firstDayMonth = DateHelpers.FirstDayOfMonth(calendarYear, calendarMonth);
            var lastDayMonth = DateHelpers.LastDayOfMonth(calendarYear, calendarMonth);
            
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
