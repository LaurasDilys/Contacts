using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UnacceptedSharesAsJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnacceptedShares_AspNetUsers_UserId",
                table: "UnacceptedShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnacceptedShares",
                table: "UnacceptedShares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UnacceptedShares");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UnacceptedShares",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "UnacceptedShares",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnacceptedShares",
                table: "UnacceptedShares",
                columns: new[] { "ContactId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UnacceptedShares_AspNetUsers_UserId",
                table: "UnacceptedShares",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnacceptedShares_Contacts_ContactId",
                table: "UnacceptedShares",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnacceptedShares_AspNetUsers_UserId",
                table: "UnacceptedShares");

            migrationBuilder.DropForeignKey(
                name: "FK_UnacceptedShares_Contacts_ContactId",
                table: "UnacceptedShares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnacceptedShares",
                table: "UnacceptedShares");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UnacceptedShares",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "UnacceptedShares",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UnacceptedShares",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnacceptedShares",
                table: "UnacceptedShares",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnacceptedShares_AspNetUsers_UserId",
                table: "UnacceptedShares",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
