using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BucksCalendar.Areas.Identity.Data;
using BucksCalendar.Areas.Identity.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BucksCalendar.Data;
using BucksCalendar.Models;
using Microsoft.AspNetCore.Identity;

namespace BucksCalendar.Pages.Calendar
{
    public class CreateModel : PageModel
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<CalendarUser> _userManager;

        public CreateModel(DatabaseContext context, UserManager<CalendarUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [BindProperty]
        public EventInput Input { get; set; }
        
        public class EventInput
        {
            [Required(ErrorMessage = "This field is required.")]
            [DataType(DataType.Text)]
            [Display(Name = "Category")]
            public int CategoryID { get; set; }
            
            [Required(ErrorMessage = "This field is required.")]
            [DataType(DataType.Text)]
            public string Title { get; set; }
            
            [DataType(DataType.Text)]
            public string Description { get; set; }
            
            [DataType(DataType.Text)]
            public string Location { get; set; }
            
            [Display(Name = "All day event")]
            public bool AllDayEvent { get; set; }
            
            [Required(ErrorMessage = "This field is required.")]
            [DataType(DataType.DateTime)]
            [Display(Name = "Start date")]
            public DateTime StartDate { get; set; }
            
            [Required(ErrorMessage = "Required.")]
            [DataType(DataType.Time)]
            [Display(Name = "Time")]
            public DateTime StartTime { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [DataType(DataType.DateTime)]
            [Display(Name = "End date")]
            public DateTime EndDate { get; set; }
            
            [Required(ErrorMessage = "Required.")]
            [DataType(DataType.Time)]
            [Display(Name = "Time")]
            public DateTime EndTime { get; set; }
            
            [Display(Name = "Notify by SMS")]
            public bool NotifyBySMS { get; set; }
            
            [Display(Name = "Notify by Email")]
            public bool NotifyByEmail { get; set; }
            
            [DataType(DataType.DateTime)]
            [Display(Name = "Scheduled for")]
            public DateTime? ScheduledFor { get; set; }
        }

        public IActionResult OnGet()
        {
        ViewData["UserID"] = new SelectList(_context.Set<CalendarUser>(), "Id", "Id");
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var calendarEvent = new Event { 
                UserID = _userManager.GetUserId(User),
                CategoryID = Input.CategoryID,
                AllDayEvent = Input.AllDayEvent,
                StartDateTime = Input.StartDate.Date.Add(Input.StartTime.TimeOfDay), 
                EndDateTime = Input.EndDate.Date.Add(Input.EndTime.TimeOfDay),
                Title = Input.Title,
                Description = Input.Description,
                Location = Input.Location
            };

            _context.Events.Add(calendarEvent);
            
            if (Input.NotifyByEmail || Input.NotifyBySMS)
            {
                var notification = new Notification
                {
                    Event = calendarEvent,
                    NotifyBySMS = Input.NotifyBySMS,
                    NotifyByEmail = Input.NotifyByEmail,
                    ScheduledFor = Input.ScheduledFor
                };

                _context.Notifications.Add(notification);
            }
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
