using Microsoft.EntityFrameworkCore.Migrations;

namespace urfurniture.DAL.Data.Migrations
{
    public partial class removeproductsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "TblProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "TblProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
