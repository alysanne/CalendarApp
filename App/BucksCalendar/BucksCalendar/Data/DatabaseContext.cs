using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucksCalendar.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using BucksCalendar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BucksCalendar.Data
{
    public class DatabaseContext : IdentityDbContext<CalendarUser>
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<UserPreference>().ToTable("UserPreference");
            modelBuilder.Entity<NotificationLog>().ToTable("NotificationLog");
            modelBuilder.Entity<Notification>().ToTable("Notification");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Event>().ToTable("Event");
            
            // Add predefined values for categories
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 1, Type = BucksCalendar.Models.Categories.Lecture});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 2, Type = BucksCalendar.Models.Categories.Deadline});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 3, Type = BucksCalendar.Models.Categories.WFH});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 4, Type = BucksCalendar.Models.Categories.AnnualLeave});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 5, Type = BucksCalendar.Models.Categories.BankHoliday});
            
            // Add predefined values for roles
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleID = 1, Type = BucksCalendar.Models.Roles.Admin, CanAddLecture = true, CanAddDeadline = true,
                CanAddWFH = true, CanAddAnnualLeave = true, CanAddBankHolidays = true
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleID = 2, Type = BucksCalendar.Models.Roles.Teacher, CanAddLecture = true, CanAddDeadline = true,
                CanAddWFH = true, CanAddAnnualLeave = true, CanAddBankHolidays = true
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                RoleID = 3, Type = BucksCalendar.Models.Roles.Student, CanAddLecture = false, CanAddDeadline = false,
                CanAddWFH = true, CanAddAnnualLeave = true, CanAddBankHolidays = false
            });
            
            // Seed Admin user
            var userGuid = Guid.NewGuid().ToString();
            modelBuilder.Entity<CalendarUser>().HasData(new CalendarUser
            {
                Id = userGuid,
                UserName = "theadmin@admin.com",
                NormalizedUserName = "THEADMIN@ADMIN.COM",
                Email = "theadmin@admin.com",
                NormalizedEmail = "THEADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEFrrxyboyIB1RkasF2XjHD9Oa0Z/Jo3tVqIH74oePSKZVJasx8dgKjGlRL+qwkRr2g==",
                SecurityStamp = "AQAAAAEAACcQAAAAEFrrxyboyIB1RkasF2XjHD9Oa0Z/Jo3tVqIH74oePSKZVJasx8dgKjGlRL+qwkRr2g==",
                ConcurrencyStamp = "e4232d05-6df2-459a-beb3-8b3d15a05faf",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                Name = "Admin",
                Role = "Admin"
            });
            
            // Seed test events
            var events = new Event[]
            {
                new Event{
                    EventID=1, UserID = userGuid, CategoryID=1, AllDayEvent=false, StartDateTime=DateTime.Parse("2019-12-06T10:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-06T18:00:00"), Title="Web Applications", 
                    Description="In this lecture we will review the tutorial for Razor Pages with Entity Framework and will learn about database migrations.",
                    Location="Uxbridge Campus - CS Lab"
                },
                new Event
                {
                    EventID=2, UserID = userGuid, CategoryID=1, AllDayEvent=false, StartDateTime=DateTime.Parse("2019-12-13T09:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-13T17:00:00"), Title="Web Applications",
                    Description="A chance to work on our projects and ask any questions before the holidays.",
                    Location="Uxbridge Campus - CS Lab"
                },
                new Event
                {
                    EventID=3, UserID = userGuid, CategoryID=2, AllDayEvent=false, StartDateTime=DateTime.Parse("2019-12-16T14:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-16T14:00:00"), Title="CW1-B (Logbook)",
                    Description="Please ensure you review the assessment guide before you submit the document. Submit all logbooks as a single Word file."
                },
                new Event
                {
                    EventID=4, UserID = userGuid, CategoryID=3, AllDayEvent=true, StartDateTime=DateTime.Parse("2019-12-20T00:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-20T10:00:00"), Title="Self-study day"
                },
                new Event
                {
                    EventID=5, UserID = userGuid, CategoryID=3, AllDayEvent=false, StartDateTime=DateTime.Parse("2019-12-06T13:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-06T18:00:00"), Title="Natalia WFH"
                },
                new Event
                {
                    EventID=6, UserID = userGuid, CategoryID=4, AllDayEvent=true, StartDateTime=DateTime.Parse("2019-12-24T00:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-29T00:00:00"), Title="Natalia on AL",
                    Description="Going home for Christmas!"
                },
                new Event
                {
                    EventID=7, UserID = userGuid, CategoryID=4, AllDayEvent=false, StartDateTime=DateTime.Parse("2019-12-12T16:30:00"),
                    EndDateTime=DateTime.Parse("2019-12-13T13:00:00"), Title="Wafa on AL"
                },
                new Event
                {
                    EventID=8, UserID = userGuid, CategoryID=5, AllDayEvent=true, StartDateTime=DateTime.Parse("2019-12-25T00:00:00"),
                    EndDateTime=DateTime.Parse("2019-12-25T00:00:00"), Title="Christmas Bank Holiday",
                    Description="Merry Christmas!"
                }
            };
            foreach (Event e in events)
            {
                modelBuilder.Entity<Event>().HasData(e);
            }
            
            // Seed test notifications
            var notifications = new Notification[]
            {
                new Notification
                {
                    NotificationID = 1, EventID = 1, NotifyBySMS = true, NotifyByEmail = true,
                    ScheduledFor = DateTime.Parse("2019-12-05T10:00:00")
                },
                new Notification
                {
                    NotificationID = 2, EventID = 2, NotifyBySMS = true, NotifyByEmail = true,
                    ScheduledFor = DateTime.Parse("2019-12-12T09:00:00")
                },
                new Notification
                {
                    NotificationID = 3, EventID = 3, NotifyBySMS = false, NotifyByEmail = true,
                    ScheduledFor = DateTime.Parse("2019-12-13T14:00:00")
                },
                new Notification
                {
                    NotificationID = 4, EventID = 8, NotifyBySMS = true, NotifyByEmail = false,
                    ScheduledFor = DateTime.Parse("2019-12-24T10:00:00")
                }
            };
            foreach (Notification n in notifications)
            {
                modelBuilder.Entity<Notification>().HasData(n);
            }
        }
    }
}
