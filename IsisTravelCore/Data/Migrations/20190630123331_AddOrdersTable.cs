using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AddOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    ItemName1 = table.Column<string>(nullable: true),
                    Quantity1 = table.Column<int>(nullable: true),
                    Price1 = table.Column<decimal>(nullable: true),
                    Discount1 = table.Column<decimal>(nullable: true),
                    ItemName2 = table.Column<string>(nullable: true),
                    Quantity2 = table.Column<int>(nullable: true),
                    Price2 = table.Column<decimal>(nullable: true),
                    Discount2 = table.Column<decimal>(nullable: true),
                    ItemName3 = table.Column<string>(nullable: true),
                    Quantity3 = table.Column<int>(nullable: true),
                    Price3 = table.Column<decimal>(nullable: true),
                    Discount3 = table.Column<decimal>(nullable: true),
                    ItemName4 = table.Column<string>(nullable: true),
                    Quantity4 = table.Column<int>(nullable: true),
                    Price4 = table.Column<decimal>(nullable: true),
                    Discount4 = table.Column<decimal>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatorId",
                table: "Orders",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
