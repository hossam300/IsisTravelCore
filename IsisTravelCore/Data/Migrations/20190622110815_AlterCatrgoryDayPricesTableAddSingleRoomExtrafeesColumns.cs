using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AlterCatrgoryDayPricesTableAddSingleRoomExtrafeesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SingleRoomExtrafees",
                table: "CatrgoryDayPrices",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SingleRoomExtrafees",
                table: "CatrgoryDayPrices");
        }
    }
}
