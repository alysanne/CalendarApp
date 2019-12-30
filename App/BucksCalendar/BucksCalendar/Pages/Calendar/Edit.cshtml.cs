using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BucksCalendar.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BucksCalendar.Data;
using BucksCalendar.Models;

namespace BucksCalendar.Pages.Calendar
{
    public class EditModel : PageModel
    {
        private readonly BucksCalendar.Data.DatabaseContext _context;

        public EditModel(BucksCalendar.Data.DatabaseContext context)
        {
            _context = context;
        }
        
        public Event Event { get; set; }
        public Notification Notification { get; set; }

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
        
        private void LoadEvent()
        {
            Input = new EventInput
            {
                CategoryID = Event.CategoryID,
                AllDayEvent = Event.AllDayEvent,
                StartDateTime = Event.StartDateTime,
                EndDateTime = Event.EndDateTime,
                Title = Event.Title,
                Description = Event.Description
            };

            if (Event.Notification != null)
            {
                Input.NotifyByEmail = Event.Notification.NotifyByEmail;
                Input.NotifyBySMS = Event.Notification.NotifyBySMS;
                Input.ScheduledFor = Event.Notification.ScheduledFor;
            }
        }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.Notification)
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (Event == null)
            {
                return NotFound();
            }
            
            LoadEvent();

            ViewData["UserID"] = new SelectList(_context.Set<CalendarUser>(), "Id", "Id");
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Event = await _context.Events
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.Notification)
                .FirstOrDefaultAsync(m => m.EventID == id);
            
            if (Input.CategoryID != Event.CategoryID)
            {
                Event.CategoryID = Input.CategoryID;
            }
            
            if (Input.AllDayEvent != Event.AllDayEvent)
            {
                Event.AllDayEvent = Input.AllDayEvent;
            }
            
            if (Input.StartDateTime != Event.StartDateTime)
            {
                Event.StartDateTime = Input.StartDateTime;
            }
            
            if (Input.EndDateTime != Event.EndDateTime)
            {
                Event.EndDateTime = Input.EndDateTime;
            }
            
            if (Input.Title != Event.Title)
            {
                Event.Title = Input.Title;
            }
            
            if (Input.Description != Event.Description)
            {
                Event.Description = Input.Description;
            }

            if (Event.Notification == null)
            {
                var notification = new Notification
                {
                    NotifyBySMS = Input.NotifyBySMS,
                    NotifyByEmail = Input.NotifyByEmail,
                    ScheduledFor = Input.ScheduledFor
                };

                Event.Notification = notification;
            }
            else
            {
                if (Input.NotifyBySMS != Event.Notification.NotifyBySMS)
                {
                    Event.Notification.NotifyBySMS = Input.NotifyBySMS;
                }
            
                if (Input.NotifyByEmail != Event.Notification.NotifyByEmail)
                {
                    Event.Notification.NotifyByEmail = Input.NotifyByEmail;
                }
            
                if (Input.ScheduledFor != Event.Notification.ScheduledFor)
                {
                    Event.Notification.ScheduledFor = Input.ScheduledFor;
                }
            }

            _context.Attach(Event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(Event.EventID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
