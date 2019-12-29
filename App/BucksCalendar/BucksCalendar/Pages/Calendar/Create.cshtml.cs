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
            
            [Display(Name = "All day event")]
            public bool AllDayEvent { get; set; }
            
            [Required(ErrorMessage = "This field is required.")]
            [DataType(DataType.DateTime)]
            [Display(Name = "Start date")]
            public DateTime StartDateTime { get; set; }

            [DataType(DataType.DateTime)]
            [Display(Name = "End date")]
            public DateTime? EndDateTime { get; set; }
            
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
                StartDateTime = Input.StartDateTime, 
                EndDateTime = Input.EndDateTime, 
                Title = Input.Title,
                Description = Input.Description
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
