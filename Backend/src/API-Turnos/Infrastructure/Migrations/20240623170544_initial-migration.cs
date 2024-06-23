using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    LicenseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    MedicalCenterId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medics_MedicalCenters_MedicalCenterId",
                        column: x => x.MedicalCenterId,
                        principalTable: "MedicalCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(20)", maxLength: 8, nullable: false),
                    MedicalCenterId = table.Column<int>(type: "INTEGER", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_MedicalCenters_MedicalCenterId",
                        column: x => x.MedicalCenterId,
                        principalTable: "MedicalCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailableAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MedicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableAppointments_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicSpecialties",
                columns: table => new
                {
                    MedicId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialtiesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicSpecialties", x => new { x.MedicId, x.SpecialtiesId });
                    table.ForeignKey(
                        name: "FK_MedicSpecialties_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicSpecialties_Specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicWorkSchedules",
                columns: table => new
                {
                    MedicId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkSchedulesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicWorkSchedules", x => new { x.MedicId, x.WorkSchedulesId });
                    table.ForeignKey(
                        name: "FK_MedicWorkSchedules_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicWorkSchedules_WorkSchedules_WorkSchedulesId",
                        column: x => x.WorkSchedulesId,
                        principalTable: "WorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MedicId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicalCenterId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCancelled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_MedicalCenters_MedicalCenterId",
                        column: x => x.MedicalCenterId,
                        principalTable: "MedicalCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Medics_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MedicalCenters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "General Hospital" },
                    { 2, "City Clinic" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cardiology" },
                    { 2, "Neurology" },
                    { 3, "Pediatrics" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "Doe", "John", "123", "Patient" },
                    { 2, "emily.johnson@example.com", "Johnson", "Emily", "123", "Patient" },
                    { 3, "george.peterson@example.com", "Peterson", "George", "123", "Patient" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { 4, "sysadmin1@example.com", "Admin1", "admin123", "SysAdmin" },
                    { 5, "sysadmin2@example.com", "Admin2", "admin123", "SysAdmin" }
                });

            migrationBuilder.InsertData(
                table: "WorkSchedules",
                columns: new[] { "Id", "Day", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 2, 2, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 3, 3, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 4, 4, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) },
                    { 5, 5, new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Medics",
                columns: new[] { "Id", "LastName", "LicenseNumber", "MedicalCenterId", "Name" },
                values: new object[,]
                {
                    { 1, "Brown", "123456", 1, "Michael" },
                    { 2, "Smith", "654321", 1, "Jane" },
                    { 3, "Jackson", "321123", 2, "Peter" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "MedicalCenterId", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { 6, "admin1@example.com", 1, "Admin 1", "password1", "AdminMC" },
                    { 7, "admin2@example.com", 2, "Admin 2", "password2", "AdminMC" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDateTime", "IsCancelled", "MedicId", "MedicalCenterId", "PatientId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 1, 1 },
                    { 2, new DateTime(2023, 6, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 2, 2 },
                    { 3, new DateTime(2023, 6, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "AvailableAppointments",
                columns: new[] { "Id", "DateTime", "MedicId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2023, 6, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2023, 6, 23, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2023, 6, 21, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2023, 6, 22, 15, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, new DateTime(2023, 6, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, new DateTime(2023, 6, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 8, new DateTime(2023, 6, 22, 17, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, new DateTime(2023, 6, 23, 18, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "MedicSpecialties",
                columns: new[] { "MedicId", "SpecialtiesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "MedicWorkSchedules",
                columns: new[] { "MedicId", "WorkSchedulesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 5 },
                    { 3, 1 },
                    { 3, 3 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicalCenterId",
                table: "Appointments",
                column: "MedicalCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MedicId",
                table: "Appointments",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AvailableAppointments_MedicId",
                table: "AvailableAppointments",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Medics_MedicalCenterId",
                table: "Medics",
                column: "MedicalCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicSpecialties_SpecialtiesId",
                table: "MedicSpecialties",
                column: "SpecialtiesId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicWorkSchedules_WorkSchedulesId",
                table: "MedicWorkSchedules",
                column: "WorkSchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MedicalCenterId",
                table: "Users",
                column: "MedicalCenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "AvailableAppointments");

            migrationBuilder.DropTable(
                name: "MedicSpecialties");

            migrationBuilder.DropTable(
                name: "MedicWorkSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropTable(
                name: "Medics");

            migrationBuilder.DropTable(
                name: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "MedicalCenters");
        }
    }
}
