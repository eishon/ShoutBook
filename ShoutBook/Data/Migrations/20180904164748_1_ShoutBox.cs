using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoutBook.Data.Migrations
{
    public partial class _1_ShoutBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoutType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoutType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shout",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeID = table.Column<int>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    ShoutBy = table.Column<string>(nullable: true),
                    Vote = table.Column<int>(nullable: false),
                    Reject = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Attach = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shout", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shout_ShoutType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "ShoutType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shout_TypeID",
                table: "Shout",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shout");

            migrationBuilder.DropTable(
                name: "ShoutType");
        }
    }
}
