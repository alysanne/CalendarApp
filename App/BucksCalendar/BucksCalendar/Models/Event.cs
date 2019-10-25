using System;

namespace BucksCalendar.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string CategoryID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Notify { get; set; }
    }
}