using System;

namespace BucksCalendar.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int EventID { get; set; }
        public bool NotifyBySMS { get; set; }
        public bool NotifyByEmail { get; set; }
        public DateTime ScheduledFor { get; set; }
    }
}