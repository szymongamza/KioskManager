using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KioskManager.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kiosk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PCId = table.Column<string>(type: "TEXT", nullable: false),
                    ActualIPAddress = table.Column<string>(type: "TEXT", nullable: true),
                    isOnline = table.Column<bool>(type: "INTEGER", nullable: false),
                    Registered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastOnline = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SettingHostName = table.Column<string>(type: "TEXT", nullable: false),
                    SettingHomePage = table.Column<string>(type: "TEXT", nullable: false),
                    SettingKioskConfig = table.Column<string>(type: "TEXT", nullable: false),
                    SettingScheduledAction = table.Column<string>(type: "TEXT", nullable: false),
                    SettingRefreshPage = table.Column<string>(type: "TEXT", nullable: false),
                    SettingRootPassword = table.Column<string>(type: "TEXT", nullable: false),
                    SettingRtcWake = table.Column<string>(type: "TEXT", nullable: false),
                    SettingScreenSettings = table.Column<string>(type: "TEXT", nullable: false),
                    SettingTimeZone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiosk", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kiosk");
        }
    }
}
