namespace BucksCalendar.Models
{
    public class UserPreference
    {
        public int UserPreferenceID { get; set; }
        public string UserID { get; set; }
        public bool ConsentSMS { get; set; }
        public bool ConsentEmail { get; set; }
        public bool LectureNotificationsSMS { get; set; }
        public bool DeadlineNotificationsSMS { get; set; }
        public bool WFHNotificationsSMS { get; set; }
        public bool AnnualLeaveNotificationsSMS { get; set; }
        public bool BankHolidayNotificationsSMS { get; set; }
        public bool LectureNotificationsEmail { get; set; }
        public bool DeadlineNotificationsEmail { get; set; }
        public bool WFHNotificationsEmail { get; set; }
        public bool AnnualLeaveNotificationsEmail { get; set; }
        public bool BankHolidayNotificationsEmail { get; set; }
    }
}