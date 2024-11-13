using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFam.Infrastructure.Migrations.WriteDb
{
    /// <inheritdoc />
    public partial class ChangePetHealthInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "health_info_age",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "health_info_age",
                table: "pets");
        }
    }
}
