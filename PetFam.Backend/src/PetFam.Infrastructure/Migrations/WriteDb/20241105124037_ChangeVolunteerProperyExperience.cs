using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFam.Infrastructure.Migrations.WriteDb
{
    /// <inheritdoc />
    public partial class ChangeVolunteerProperyExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ages_of_expirience",
                table: "volunteers",
                newName: "ages_of_experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ages_of_experience",
                table: "volunteers",
                newName: "ages_of_expirience");
        }
    }
}
