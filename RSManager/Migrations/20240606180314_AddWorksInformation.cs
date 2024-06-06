using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSManager.Migrations
{
    /// <inheritdoc />
    public partial class AddWorksInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorksInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Client = table.Column<string>(type: "TEXT", nullable: false),
                    Car = table.Column<string>(type: "TEXT", nullable: false),
                    Worker = table.Column<string>(type: "TEXT", nullable: false),
                    SuOfWork = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksInformation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorksInformation");
        }
    }
}
