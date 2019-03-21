using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoutBook.Data.Migrations
{
    public partial class _2_Shout_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Vote",
                table: "Shout",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Shout",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<int>(
                name: "Reject",
                table: "Shout",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Vote",
                table: "Shout",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Shout",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Reject",
                table: "Shout",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
