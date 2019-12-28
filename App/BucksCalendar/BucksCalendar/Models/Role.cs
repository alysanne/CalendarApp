namespace BucksCalendar.Models
{
    public enum Roles
    {
        Admin,
        Teacher,
        Student
    }
    public class Role
    {
        public int RoleID { get; set; }
        public Roles Type { get; set; }
        public bool CanAddLecture { get; set; }
        public bool CanAddDeadline { get; set; }
        public bool CanAddWFH { get; set; }
        public bool CanAddAnnualLeave { get; set; }
        public bool CanAddBankHolidays { get; set; }
    }
}