using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BucksCalendar.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    AllDayEvent = table.Column<bool>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
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
                    ScheduledFor = table.Column<DateTime>(nullable: true)
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
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d52e2e23-d9d8-44a9-b3fe-c67b110e3998", 0, "e4232d05-6df2-459a-beb3-8b3d15a05faf", "theadmin@admin.com", true, null, true, null, "Admin", "THEADMIN@ADMIN.COM", "THEADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEFrrxyboyIB1RkasF2XjHD9Oa0Z/Jo3tVqIH74oePSKZVJasx8dgKjGlRL+qwkRr2g==", null, false, "Admin", "AQAAAAEAACcQAAAAEFrrxyboyIB1RkasF2XjHD9Oa0Z/Jo3tVqIH74oePSKZVJasx8dgKjGlRL+qwkRr2g==", false, "theadmin@admin.com" });

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

            migrationBuilder.InsertData(
                table: "Event",
                columns: new[] { "EventID", "AllDayEvent", "CategoryID", "Description", "EndDateTime", "Location", "StartDateTime", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, false, 1, "In this lecture we will review the tutorial for Razor Pages with Entity Framework and will learn about database migrations.", new DateTime(2019, 12, 6, 18, 0, 0, 0, DateTimeKind.Unspecified), "Uxbridge Campus - CS Lab", new DateTime(2019, 12, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Web Applications", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 2, false, 1, "A chance to work on our projects and ask any questions before the holidays.", new DateTime(2019, 12, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), "Uxbridge Campus - CS Lab", new DateTime(2019, 12, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), "Web Applications", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 3, false, 2, "Please ensure you review the assessment guide before you submit the document. Submit all logbooks as a single Word file.", new DateTime(2019, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), "CW1-B (Logbook)", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 4, true, 3, null, new DateTime(2019, 12, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Self-study day", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 5, false, 3, null, new DateTime(2019, 12, 6, 18, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 6, 13, 0, 0, 0, DateTimeKind.Unspecified), "Natalia WFH", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 6, true, 4, "Going home for Christmas!", new DateTime(2019, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Natalia on AL", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 7, false, 4, null, new DateTime(2019, 12, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 12, 16, 30, 0, 0, DateTimeKind.Unspecified), "Wafa on AL", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" },
                    { 8, true, 5, "Merry Christmas!", new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2019, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christmas Bank Holiday", "d52e2e23-d9d8-44a9-b3fe-c67b110e3998" }
                });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "NotificationID", "EventID", "NotifyByEmail", "NotifyBySMS", "ScheduledFor" },
                values: new object[,]
                {
                    { 1, 1, true, true, new DateTime(2019, 12, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, true, true, new DateTime(2019, 12, 12, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, true, false, new DateTime(2019, 12, 13, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 8, false, true, new DateTime(2019, 12, 24, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "NotificationLog");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "UserPreference");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
