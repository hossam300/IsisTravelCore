using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AddRequestOffersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestOffers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    AdultQuantity = table.Column<int>(nullable: false),
                    BabyQuantity = table.Column<int>(nullable: false),
                    ExtraRoomQuantity = table.Column<int>(nullable: false),
                    TotalAdult = table.Column<decimal>(nullable: false),
                    TotalBaby = table.Column<decimal>(nullable: false),
                    TotalExtraRoom = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    TourId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOffers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOffers_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestOffers_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_CategoryId",
                table: "RequestOffers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_CreatorId",
                table: "RequestOffers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_TourId",
                table: "RequestOffers",
                column: "TourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestOffers");
        }
    }
}
