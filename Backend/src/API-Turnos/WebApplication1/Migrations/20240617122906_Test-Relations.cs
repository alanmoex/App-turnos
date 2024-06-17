using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class TestRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Medics_MedicId",
                table: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_Specialties_MedicId",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Specialties");

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

            migrationBuilder.CreateIndex(
                name: "IX_MedicSpecialties_SpecialtiesId",
                table: "MedicSpecialties",
                column: "SpecialtiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicSpecialties");

            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "Specialties",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialties_MedicId",
                table: "Specialties",
                column: "MedicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Medics_MedicId",
                table: "Specialties",
                column: "MedicId",
                principalTable: "Medics",
                principalColumn: "Id");
        }
    }
}
