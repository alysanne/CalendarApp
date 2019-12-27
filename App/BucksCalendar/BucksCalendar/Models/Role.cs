namespace BucksCalendar.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public bool CanAddLecture { get; set; }
        public bool CanAddDeadline { get; set; }
        public bool CanAddWFH { get; set; }
        public bool CanAddAnnualLeave { get; set; }
        public bool CanAddBankHolidays { get; set; }
    }
}