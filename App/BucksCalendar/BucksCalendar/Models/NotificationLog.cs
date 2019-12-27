using System;

namespace BucksCalendar.Models
{
    public class NotificationLog
    {
        public int NotificationLogID { get; set; }
        public int NotificationID { get; set; }
        public string NotificationType { get; set; }
        public DateTime DateSent { get; set; }
        
        public Notification Notification { get; set; }
        public Event Event { get; set; }
    }
}