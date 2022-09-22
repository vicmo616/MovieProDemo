using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieProDemo.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "Movie",
                newName: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movie",
                newName: "MovieID");
        }
    }
}
