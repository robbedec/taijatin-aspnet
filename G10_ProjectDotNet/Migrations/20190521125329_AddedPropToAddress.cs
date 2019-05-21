using Microsoft.EntityFrameworkCore.Migrations;

namespace G10_ProjectDotNet.Migrations
{
    public partial class AddedPropToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Bus",
            //    table: "Address",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bus",
                table: "Address");
        }
    }
}
