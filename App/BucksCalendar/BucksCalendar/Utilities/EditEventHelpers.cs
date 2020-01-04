using BucksCalendar.Models;
using BucksCalendar.Pages.Calendar;

namespace BucksCalendar.Utilities
{
    public class EditEventHelpers
    {
        public static void HandleNotifications(Event eventData, EditModel.EventInput input)
        {
            if (eventData.Notification == null)
            {
                var newNotification = new Notification
                {
                    NotifyBySMS = input.NotifyBySMS,
                    NotifyByEmail = input.NotifyByEmail,
                    ScheduledFor = input.ScheduledFor
                };

                eventData.Notification = newNotification;
            }
            else
            {
                if (input.NotifyBySMS != eventData.Notification.NotifyBySMS)
                {
                    eventData.Notification.NotifyBySMS = input.NotifyBySMS;
                }
            
                if (input.NotifyByEmail != eventData.Notification.NotifyByEmail)
                {
                    eventData.Notification.NotifyByEmail = input.NotifyByEmail;
                }
            
                if (input.ScheduledFor != eventData.Notification.ScheduledFor)
                {
                    eventData.Notification.ScheduledFor = input.ScheduledFor;
                }
            }
        }

        public static void HandleDates(Event eventData, EditModel.EventInput input)
        {
            var startDateTime = input.StartDate.Date.Add(input.StartTime.TimeOfDay);
            if (startDateTime != eventData.StartDateTime)
            {
                eventData.StartDateTime = startDateTime;
            }
            
            var endDateTime = input.EndDate.Date.Add(input.EndTime.TimeOfDay);
            if (endDateTime != eventData.EndDateTime)
            {
                eventData.EndDateTime = endDateTime;
            }
        }
    }
}