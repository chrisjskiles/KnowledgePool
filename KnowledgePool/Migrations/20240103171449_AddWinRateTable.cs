using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePool.Migrations
{
    /// <inheritdoc />
    public partial class AddWinRateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WinRates",
                columns: table => new
                {
                    Uuid = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Set = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Rarity = table.Column<string>(type: "TEXT", nullable: true),
                    OhWr = table.Column<decimal>(type: "TEXT", nullable: true),
                    GdWr = table.Column<decimal>(type: "TEXT", nullable: true),
                    GihWr = table.Column<decimal>(type: "TEXT", nullable: true),
                    Iwd = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinRates", x => x.Uuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WinRates");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "cards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
