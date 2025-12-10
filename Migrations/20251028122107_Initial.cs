using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GasField.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ukpgs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ukpgs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoofGvk = table.Column<double>(type: "float", nullable: false),
                    BottomGvk = table.Column<double>(type: "float", nullable: false),
                    RoofPerforation = table.Column<double>(type: "float", nullable: false),
                    BottomPerforation = table.Column<double>(type: "float", nullable: false),
                    Extraction = table.Column<double>(type: "float", nullable: false),
                    UkpgId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wells_Ukpgs_UkpgId",
                        column: x => x.UkpgId,
                        principalTable: "Ukpgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "CreatedAt", "FullName", "Login", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Администратор системы", "Operator", "$2a$11$3ufDM9/ALD4/aoVANHk7FuFfFlJPcw8LfzQRlX7e8KQVIR3XfWoYi", "Admin" },
                    { 2, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Инженер по добыче", "Engineer", "$2a$11$3ufDM9/ALD4/aoVANHk7FuFfFlJPcw8LfzQRlX7e8KQVIR3XfWoYi", "Engineer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_Login",
                table: "People",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wells_UkpgId",
                table: "Wells",
                column: "UkpgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Wells");

            migrationBuilder.DropTable(
                name: "Ukpgs");
        }
    }
}
