using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSManager.Migrations
{
    /// <inheritdoc />
    public partial class AddWorksInformationEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorksInformationEdited",
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
                    table.PrimaryKey("PK_WorksInformationEdited", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorksInformationEdited");
        }
    }
}
