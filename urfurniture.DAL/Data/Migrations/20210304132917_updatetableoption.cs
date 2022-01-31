using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace urfurniture.DAL.Data.Migrations
{
    public partial class updatetableoption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblProductOptions_TblOptions_OptionRefId",
                table: "TblProductOptions");

            migrationBuilder.DropTable(
                name: "TblDeliverableLocations");

            migrationBuilder.DropTable(
                name: "TblOptions");

            migrationBuilder.DropTable(
                name: "TblOptionGroups");

            migrationBuilder.DropIndex(
                name: "IX_TblProductOptions_OptionRefId",
                table: "TblProductOptions");

            migrationBuilder.DropColumn(
                name: "OptionRefId",
                table: "TblProductOptions");

            migrationBuilder.AddColumn<string>(
                name: "OptionName",
                table: "TblProductOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductOptionId",
                table: "TblOrderItemDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionName",
                table: "TblProductOptions");

            migrationBuilder.DropColumn(
                name: "ProductOptionId",
                table: "TblOrderItemDetails");

            migrationBuilder.AddColumn<int>(
                name: "OptionRefId",
                table: "TblProductOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TblDeliverableLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDeliverableLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblOptionGroups",
                columns: table => new
                {
                    OptionGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OptionGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOptionGroups", x => x.OptionGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TblOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OptionGroupRefId = table.Column<int>(type: "int", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_TblOptions_TblOptionGroups_OptionGroupRefId",
                        column: x => x.OptionGroupRefId,
                        principalTable: "TblOptionGroups",
                        principalColumn: "OptionGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblProductOptions_OptionRefId",
                table: "TblProductOptions",
                column: "OptionRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOptions_OptionGroupRefId",
                table: "TblOptions",
                column: "OptionGroupRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblProductOptions_TblOptions_OptionRefId",
                table: "TblProductOptions",
                column: "OptionRefId",
                principalTable: "TblOptions",
                principalColumn: "OptionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
