using Microsoft.EntityFrameworkCore.Migrations;

namespace DavidSerb.DataModel.Migrations
{
    public partial class RenameColumnWeightFromDrugType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WeightInPounds",
                table: "DrugTypes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightInPounds",
                table: "DrugTypes");
        }
    }
}
