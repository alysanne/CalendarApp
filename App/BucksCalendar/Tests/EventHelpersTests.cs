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
        
        [Fact]
        public void EditDates_TimeSegment()
        {
            var testEvent = new Event()
            {
                StartDateTime = new DateTime(2020, 2, 3),
                EndDateTime = new DateTime(2020, 2, 4)
            };
            var testInput = new EditModel.EventInput()
            {
                StartDate = new DateTime(2020, 2, 3),
                EndDate = new DateTime(2020, 2, 4),
                StartTime = new DateTime(2020, 1, 1, 12, 30, 23),
                EndTime = new DateTime(2020, 1, 1, 11, 20, 32),
            };
            
            EditEventHelpers.HandleDates(testEvent, testInput);
            
            Assert.Equal(new DateTime(2020, 2, 3, 12, 30, 23), testEvent.StartDateTime);
            Assert.Equal(new DateTime(2020, 2, 4, 11, 20, 32), testEvent.EndDateTime);
        }
        
        [Fact]
        public void EditDates_DateSegment()
        {
            var testEvent = new Event()
            {
                StartDateTime = new DateTime(2020, 2, 3),
                EndDateTime = new DateTime(2020, 2, 4)
            };
            var testInput = new EditModel.EventInput()
            {
                StartDate = new DateTime(2021, 2, 1),
                EndDate = new DateTime(2021, 3, 2),
                StartTime = new DateTime(2020, 1, 1),
                EndTime = new DateTime(2020, 2, 2),
            };
            
            EditEventHelpers.HandleDates(testEvent, testInput);
            
            Assert.Equal(new DateTime(2021, 2, 1), testEvent.StartDateTime);
            Assert.Equal(new DateTime(2021, 3, 2), testEvent.EndDateTime);
        }
    }
}