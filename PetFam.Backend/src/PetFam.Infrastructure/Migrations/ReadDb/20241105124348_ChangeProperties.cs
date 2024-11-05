using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFam.Infrastructure.Migrations.ReadDb
{
    /// <inheritdoc />
    public partial class ChangeProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "volunteers",
                newName: "last_name");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patronymic",
                table: "volunteers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "patronymic",
                table: "volunteers");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "volunteers",
                newName: "full_name");
        }
    }
}
