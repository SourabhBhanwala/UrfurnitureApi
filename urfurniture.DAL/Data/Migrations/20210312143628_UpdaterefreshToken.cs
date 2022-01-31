﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace urfurniture.DAL.Data.Migrations
{
    public partial class UpdaterefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TblRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TblRefreshTokens");
        }
    }
}
