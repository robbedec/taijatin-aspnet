using Microsoft.EntityFrameworkCore.Migrations;

namespace G10_ProjectDotNet.Migrations
{
    public partial class AddedPropToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "IsNoMember",
            //    table: "Users",
            //    nullable: false,
            //    defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNoMember",
                table: "Users");
        }
    }
}
