using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Qlyniq.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "diagnosis",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    MedicalTerm = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnosis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "offices",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    OfficeNumber = table.Column<uint>(nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(10,0)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SocialSecurityNumber = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "enum('Male','Female')", nullable: false),
                    HealthCareProvider = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OfficeId = table.Column<uint>(nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(255)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(255)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "enum('Male','Female')", nullable: false),
                    IsMedicalWorker = table.Column<sbyte>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'"),
                    MedicalTitle = table.Column<string>(type: "varchar(50)", nullable: true),
                    IsDean = table.Column<sbyte>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'"),
                    DeanOfficeId = table.Column<uint>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_DeanOfficeId",
                        column: x => x.DeanOfficeId,
                        principalTable: "offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientFirstName = table.Column<string>(type: "varchar(255)", nullable: true),
                    PatientLastName = table.Column<string>(type: "varchar(255)", nullable: true),
                    PatientId = table.Column<uint>(nullable: true),
                    DoctorId = table.Column<uint>(nullable: false),
                    StartingTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    PatientId = table.Column<uint>(nullable: false),
                    CreatorId = table.Column<uint>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Note = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "labreports",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RecipientId = table.Column<uint>(nullable: false),
                    PatientId = table.Column<uint>(nullable: false),
                    AcceptedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    SampledTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Glucose = table.Column<float>(nullable: false),
                    Urea = table.Column<float>(nullable: false),
                    Creatine = table.Column<float>(nullable: false),
                    Cholesterol = table.Column<float>(nullable: false),
                    Helicobacter = table.Column<sbyte>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labreports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabResults_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabResults_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "examinations",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<uint>(nullable: false),
                    DoctorId = table.Column<uint>(nullable: false),
                    StartingTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    FileId = table.Column<uint>(nullable: false),
                    DiagnosisId = table.Column<uint>(nullable: true),
                    Therapy = table.Column<string>(type: "varchar(500)", nullable: false, defaultValueSql: "'Nihil'"),
                    IsEmergency = table.Column<sbyte>(type: "tinyint(1)", nullable: false, defaultValueSql: "'0'"),
                    LabReportId = table.Column<uint>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "diagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_FileId",
                        column: x => x.FileId,
                        principalTable: "files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_LabReportId",
                        column: x => x.LabReportId,
                        principalTable: "labreports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examinations_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "FK_Appointments_DoctorId",
                table: "appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "FK_Appointments_PatientId",
                table: "appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "FK_Employees_DeanOfficeId",
                table: "employees",
                column: "DeanOfficeId");

            migrationBuilder.CreateIndex(
                name: "FK_Employees_OfficeId",
                table: "employees",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "SocialSecurityNumber",
                table: "employees",
                column: "SocialSecurityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_Examinations_DiagnosisId",
                table: "examinations",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "FK_Examinations_DoctorId",
                table: "examinations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "FK_Examinations_FileId",
                table: "examinations",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "FK_Examinations_LabReportId",
                table: "examinations",
                column: "LabReportId");

            migrationBuilder.CreateIndex(
                name: "FK_Examinations_PatientId",
                table: "examinations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "FK_Files_CreatorId",
                table: "files",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "FK_Files_PatientId",
                table: "files",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "FK_LabResults_PatientId",
                table: "labreports",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "FK_LabResults_RecipientId",
                table: "labreports",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "SocialSecurityNumber",
                table: "patients",
                column: "SocialSecurityNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "examinations");

            migrationBuilder.DropTable(
                name: "diagnosis");

            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "labreports");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "offices");
        }
    }
}
