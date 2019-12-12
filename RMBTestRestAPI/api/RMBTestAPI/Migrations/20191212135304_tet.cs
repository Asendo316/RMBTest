using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RMBTestRestAPI.Migrations
{
    public partial class tet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountData",
                columns: table => new
                {
                    guid = table.Column<Guid>(nullable: false),
                    AccountNumber = table.Column<int>(nullable: false),
                    LedgerBalance = table.Column<double>(nullable: false),
                    CurrentBalance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountData", x => x.guid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountData");
        }
    }
}
