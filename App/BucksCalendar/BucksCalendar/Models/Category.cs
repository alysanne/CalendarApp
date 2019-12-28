namespace BucksCalendar.Models
{
    public enum Categories {
        Lecture,
        Deadline,
        WFH,
        AnnualLeave,
        BankHoliday
    }
    public class Category
    {
        public int CategoryID { get; set; }
        public Categories Type { get; set; }
    }
}