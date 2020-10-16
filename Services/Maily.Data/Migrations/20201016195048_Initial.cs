using Microsoft.EntityFrameworkCore.Migrations;

namespace Maily.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(maxLength: 64, nullable: true),
                    Username = table.Column<string>(maxLength: 128, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 512, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 512, nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailGroupMails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailId = table.Column<int>(nullable: false),
                    MailGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailGroupMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailGroupMails_MailGroups_MailGroupId",
                        column: x => x.MailGroupId,
                        principalTable: "MailGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MailGroupMails_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailGroupMails_MailGroupId",
                table: "MailGroupMails",
                column: "MailGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MailGroupMails_MailId",
                table: "MailGroupMails",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_MailGroups_UserId",
                table: "MailGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mails_UserId",
                table: "Mails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true,
                filter: "[Token] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailGroupMails");

            migrationBuilder.DropTable(
                name: "MailGroups");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
