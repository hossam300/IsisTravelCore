﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace IsisTravelCore.Data.Migrations
{
    public partial class AlterRequestOfferTableAddStatusCoulmn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "RequestOffers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "RequestOffers");
        }
    }
}
