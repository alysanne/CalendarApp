using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BucksCalendar.Migrations
{
    public partial class CalendarDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    CanAddLecture = table.Column<bool>(nullable: false),
                    CanAddDeadline = table.Column<bool>(nullable: false),
                    CanAddWFH = table.Column<bool>(nullable: false),
                    CanAddAnnualLeave = table.Column<bool>(nullable: false),
                    CanAddBankHolidays = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "UserPreference",
                columns: table => new
                {
                    UserPreferenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    ConsentSMS = table.Column<bool>(nullable: false),
                    ConsentEmail = table.Column<bool>(nullable: false),
                    LectureNotificationsSMS = table.Column<bool>(nullable: false),
                    DeadlineNotificationsSMS = table.Column<bool>(nullable: false),
                    WFHNotificationsSMS = table.Column<bool>(nullable: false),
                    AnnualLeaveNotificationsSMS = table.Column<bool>(nullable: false),
                    BankHolidayNotificationsSMS = table.Column<bool>(nullable: false),
                    LectureNotificationsEmail = table.Column<bool>(nullable: false),
                    DeadlineNotificationsEmail = table.Column<bool>(nullable: false),
                    WFHNotificationsEmail = table.Column<bool>(nullable: false),
                    AnnualLeaveNotificationsEmail = table.Column<bool>(nullable: false),
                    BankHolidayNotificationsEmail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreference", x => x.UserPreferenceID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NotifyUsers = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Event_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventID = table.Column<int>(nullable: false),
                    NotifyBySMS = table.Column<bool>(nullable: false),
                    NotifyByEmail = table.Column<bool>(nullable: false),
                    ScheduledFor = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notification_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLog",
                columns: table => new
                {
                    NotificationLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationID = table.Column<int>(nullable: false),
                    NotificationType = table.Column<string>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: false),
                    EventID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.NotificationLogID);
                    table.ForeignKey(
                        name: "FK_NotificationLog_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLog_Notification_NotificationID",
                        column: x => x.NotificationID,
                        principalTable: "Notification",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryID", "Type" },
                values: new object[,]
                {
                    { 1, 0 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 4 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleID", "CanAddAnnualLeave", "CanAddBankHolidays", "CanAddDeadline", "CanAddLecture", "CanAddWFH", "Type" },
                values: new object[,]
                {
                    { 1, true, true, true, true, true, 0 },
                    { 2, true, true, true, true, true, 1 },
                    { 3, true, false, false, false, true, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_CategoryID",
                table: "Event",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserID",
                table: "Event",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EventID",
                table: "Notification",
                column: "EventID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_EventID",
                table: "NotificationLog",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_NotificationID",
                table: "NotificationLog",
                column: "NotificationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationLog");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserPreference");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
