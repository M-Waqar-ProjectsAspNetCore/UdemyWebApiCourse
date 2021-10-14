using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiIdentityJwt.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2423e4aa-870d-4de6-8449-3de5c3d8b4c6", "45270bb3-4384-4767-ac0c-d06be2014f3b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "673c3e59-7dc4-4fa9-bb3b-c53f297b6bd6", "0502ce17-0bea-4360-9f52-4e7dcd5e1ddb", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2423e4aa-870d-4de6-8449-3de5c3d8b4c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "673c3e59-7dc4-4fa9-bb3b-c53f297b6bd6");
        }
    }
}
