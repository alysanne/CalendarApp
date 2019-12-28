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
        public DbSet<Category> Category { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<NotificationLog> NotificationLog { get; set; }
        public DbSet<UserPreference> UserPreference { get; set; }

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
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 1, Type = Categories.Lecture});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 2, Type = Categories.Deadline});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 3, Type = Categories.WFH});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 4, Type = Categories.AnnualLeave});
            modelBuilder.Entity<Category>().HasData(new Category {CategoryID = 5, Type = Categories.BankHoliday});
            
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
        }
    }
}
