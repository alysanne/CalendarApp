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
        }
    }
}
