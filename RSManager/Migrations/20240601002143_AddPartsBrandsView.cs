using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSManager.Migrations
{
    /// <inheritdoc />
    public partial class AddPartsBrandsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autorization",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    UserPassword = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorization", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    HorsePower = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fio = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartsBrandsView",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartName = table.Column<string>(type: "TEXT", nullable: false),
                    PartCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PartSum = table.Column<int>(type: "INTEGER", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartsBrandsView", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fio = table.Column<string>(type: "TEXT", nullable: false),
                    Post = table.Column<string>(type: "TEXT", nullable: false),
                    Experience = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartName = table.Column<string>(type: "TEXT", nullable: false),
                    PartCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PartSum = table.Column<int>(type: "INTEGER", nullable: false),
                    SuitableCar = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Cars_SuitableCar",
                        column: x => x.SuitableCar,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Client = table.Column<int>(type: "INTEGER", nullable: false),
                    Car = table.Column<int>(type: "INTEGER", nullable: false),
                    Worker = table.Column<int>(type: "INTEGER", nullable: false),
                    SumOfWork = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Cars_Car",
                        column: x => x.Car,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Clients_Client",
                        column: x => x.Client,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Workers_Worker",
                        column: x => x.Worker,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Works_Parts",
                columns: table => new
                {
                    WorksId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountOfParts = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works_Parts", x => new { x.WorksId, x.PartId });
                    table.ForeignKey(
                        name: "FK_Works_Parts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Parts_Works_WorksId",
                        column: x => x.WorksId,
                        principalTable: "Works",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SuitableCar",
                table: "Parts",
                column: "SuitableCar");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Car",
                table: "Works",
                column: "Car");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Client",
                table: "Works",
                column: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Worker",
                table: "Works",
                column: "Worker");

            migrationBuilder.CreateIndex(
                name: "IX_Works_Parts_PartId",
                table: "Works_Parts",
                column: "PartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autorization");

            migrationBuilder.DropTable(
                name: "PartsBrandsView");

            migrationBuilder.DropTable(
                name: "Works_Parts");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
