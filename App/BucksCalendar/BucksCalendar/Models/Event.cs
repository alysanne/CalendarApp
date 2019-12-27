using System;
using System.Collections.Generic;
using BucksCalendar.Areas.Identity.Data;

namespace BucksCalendar.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool NotifyUsers { get; set; }
        
        public Category Category { get; set; }
        public CalendarUser User { get; set; }
        public Notification Notification { get; set; }
        public ICollection<NotificationLog> NotificationLogs { get; set; }
    }
}