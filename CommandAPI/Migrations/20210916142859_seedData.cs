using Microsoft.EntityFrameworkCore.Migrations;

namespace CommandAPI.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "HowTo", "Line", "Platform" },
                values: new object[] { 169, "", "", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: 169);
        }
    }
}
