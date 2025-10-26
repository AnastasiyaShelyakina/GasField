using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasField.Migrations
{
    /// <inheritdoc />
    public partial class Ukpgandwells : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UkpgCode",
                table: "Wells",
                newName: "UkpgId");

            migrationBuilder.AlterColumn<int>(
                name: "UsernameId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wells_UkpgId",
                table: "Wells",
                column: "UkpgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wells_Ukpgs_UkpgId",
                table: "Wells",
                column: "UkpgId",
                principalTable: "Ukpgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wells_Ukpgs_UkpgId",
                table: "Wells");

            migrationBuilder.DropIndex(
                name: "IX_Wells_UkpgId",
                table: "Wells");

            migrationBuilder.RenameColumn(
                name: "UkpgId",
                table: "Wells",
                newName: "UkpgCode");

            migrationBuilder.AlterColumn<int>(
                name: "UsernameId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
