using Microsoft.EntityFrameworkCore.Migrations;

namespace DavidSerb.DataModel.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depots",
                columns: table => new
                {
                    DepotId = table.Column<string>(nullable: false),
                    DepotName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depots", x => x.DepotId);
                });

            migrationBuilder.CreateTable(
                name: "DrugTypes",
                columns: table => new
                {
                    DrugTypeId = table.Column<string>(nullable: false),
                    DrugTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugTypes", x => x.DrugTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    DepotId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                    table.ForeignKey(
                        name: "FK_Countries_Depots_DepotId",
                        column: x => x.DepotId,
                        principalTable: "Depots",
                        principalColumn: "DepotId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<string>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                    table.ForeignKey(
                        name: "FK_Sites_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrugUnits",
                columns: table => new
                {
                    DrugUnitId = table.Column<string>(nullable: false),
                    PickNumber = table.Column<int>(nullable: false),
                    DepotId = table.Column<string>(nullable: true),
                    DrugTypeId = table.Column<string>(nullable: true),
                    SiteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugUnits", x => x.DrugUnitId);
                    table.ForeignKey(
                        name: "FK_DrugUnits_Depots_DepotId",
                        column: x => x.DepotId,
                        principalTable: "Depots",
                        principalColumn: "DepotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugUnits_DrugTypes_DrugTypeId",
                        column: x => x.DrugTypeId,
                        principalTable: "DrugTypes",
                        principalColumn: "DrugTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugUnits_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryName",
                table: "Countries",
                column: "CountryName",
                unique: true,
                filter: "[CountryName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_DepotId",
                table: "Countries",
                column: "DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugUnits_DepotId",
                table: "DrugUnits",
                column: "DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugUnits_DrugTypeId",
                table: "DrugUnits",
                column: "DrugTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugUnits_SiteId",
                table: "DrugUnits",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_CountryCode",
                table: "Sites",
                column: "CountryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugUnits");

            migrationBuilder.DropTable(
                name: "DrugTypes");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Depots");
        }
    }
}
