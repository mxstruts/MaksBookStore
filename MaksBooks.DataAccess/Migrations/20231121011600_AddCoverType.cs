using Microsoft.EntityFrameworkCore.Migrations;

namespace MaksBooks.DataAccess.Migrations
{
    public partial class AddCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CoverTypes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CoverTypes",
                newName: "ID");
        }
    }
}
