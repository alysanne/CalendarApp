using System;
using BucksCalendar.Models;
using BucksCalendar.Pages.Calendar;
using BucksCalendar.Utilities;
using Xunit;

namespace Tests
{
    public class EventHelpersTests
    {
        [Fact]
        public void AddNewNotification()
        {
            var testEvent = new Event();
            var testInput = new EditModel.EventInput()
            {
                NotifyBySMS = true,
                NotifyByEmail = false,
                ScheduledFor = new DateTime(2019, 1, 1)
            };
            
            EditEventHelpers.HandleNotifications(testEvent, testInput);
            
            Assert.Equal(testInput.NotifyByEmail, testEvent.Notification.NotifyByEmail);
            Assert.Equal(testInput.NotifyBySMS, testEvent.Notification.NotifyBySMS);
            Assert.Equal(testInput.ScheduledFor, testEvent.Notification.ScheduledFor);
        }
        
        [Fact]
        public void EditExistingNotification()
        {
            var testEvent = new Event()
            {
                Notification = new Notification()
                {
                    NotifyBySMS = false,
                    NotifyByEmail = true,
                    ScheduledFor = new DateTime(2020, 02, 03)
                }
            };
            var testInput = new EditModel.EventInput()
            {
                NotifyBySMS = true,
                NotifyByEmail = false,
                ScheduledFor = new DateTime(2019, 1, 1)
            };
            
            EditEventHelpers.HandleNotifications(testEvent, testInput);
            
            Assert.False(testEvent.Notification.NotifyByEmail);
            Assert.True(testEvent.Notification.NotifyBySMS);
            Assert.Equal(new DateTime(2019, 1, 1), testEvent.Notification.ScheduledFor);
        }
    }
}