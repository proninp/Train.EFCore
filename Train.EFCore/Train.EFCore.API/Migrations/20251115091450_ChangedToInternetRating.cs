using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Train.EFCore.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToInternetRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m
            );

            migrationBuilder.RenameColumn(
                name: "ImdbRating",
                table: "Pictures",
                newName: "InternetRating"
            );

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "InternetRating",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InternetRating",
                table: "Pictures",
                newName: "ImdbRating"
            );
            
            migrationBuilder.AlterColumn<int>(
                name: "ImdbRating",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImdbRating",
                value: 0);
        }
    }
}
