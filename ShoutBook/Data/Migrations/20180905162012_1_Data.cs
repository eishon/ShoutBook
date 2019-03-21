using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoutBook.Data.Migrations
{
    public partial class _1_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shout_ShoutType_TypeID",
                table: "Shout");

            migrationBuilder.DropIndex(
                name: "IX_Shout_TypeID",
                table: "Shout");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Shout");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Shout",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Shout",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShoutReactionData",
                columns: table => new
                {
                    ShoutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    Reaction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoutReactionData", x => x.ShoutID);
                });

            migrationBuilder.CreateTable(
                name: "UserData",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserData", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoutReactionData");

            migrationBuilder.DropTable(
                name: "UserData");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Shout");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Shout");

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Shout",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shout_TypeID",
                table: "Shout",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shout_ShoutType_TypeID",
                table: "Shout",
                column: "TypeID",
                principalTable: "ShoutType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
