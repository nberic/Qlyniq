using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Qlyniq.Migrations
{
    public partial class CreatedEmployeesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    OfficeId = table.Column<int>(nullable: false),
                    SocialSecurityNumber = table.Column<string>(maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    IsMedicalWorker = table.Column<short>(nullable: false),
                    MedicalTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
