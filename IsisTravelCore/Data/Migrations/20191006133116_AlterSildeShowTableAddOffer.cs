using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AlterSildeShowTableAddOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "SildeShows");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "SildeShows",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SildeShows_TourId",
                table: "SildeShows",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_SildeShows_Tours_TourId",
                table: "SildeShows",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SildeShows_Tours_TourId",
                table: "SildeShows");

            migrationBuilder.DropIndex(
                name: "IX_SildeShows_TourId",
                table: "SildeShows");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "SildeShows");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SildeShows",
                nullable: true);
        }
    }
}
