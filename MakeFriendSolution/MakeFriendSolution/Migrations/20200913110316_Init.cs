using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeFriendSolution.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PassWord = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Role = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    Location = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    FindPeople = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    IAm = table.Column<int>(nullable: false),
                    Marriage = table.Column<int>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    Education = table.Column<int>(nullable: false),
                    Body = table.Column<int>(nullable: false),
                    Character = table.Column<int>(nullable: false),
                    LifeStyle = table.Column<int>(nullable: false),
                    MostValuable = table.Column<int>(nullable: false),
                    Job = table.Column<int>(nullable: false),
                    Religion = table.Column<int>(nullable: false),
                    Smoking = table.Column<int>(nullable: false),
                    DrinkBeer = table.Column<int>(nullable: false),
                    Children = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HaveMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(maxLength: 5000, nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    SentAt = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    ReceiverId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaveMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HaveMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HaveMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThumbnailImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false, defaultValue: "Image title"),
                    ImagePath = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThumbnailImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThumbnailImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "Body", "Character", "Children", "ConcurrencyStamp", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "EmailConfirmed", "FindPeople", "FullName", "Gender", "Height", "IAm", "Job", "LifeStyle", "Location", "LockoutEnabled", "LockoutEnd", "Marriage", "MostValuable", "NormalizedEmail", "NormalizedUserName", "PassWord", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Religion", "Role", "SecurityStamp", "Smoking", "Status", "Summary", "Target", "Title", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "ec826af8-0310-48cf-8a14-da11bdb1c96d", 0, "Tam.jpg", 0, 1, 0, "a02acc64-bac2-4963-9dae-6435b498d714", new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tam@gmail.com", false, "Tìm người yêu", "Nguyễn Thành Tâm", 0, 170, 0, 7, 8, 37, false, null, 3, 23, null, null, "admin", null, "0396925225", false, 0, 0, "f2c38aaa-4b90-443b-ac2a-b24a6466e2b5", 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 5, "Thông tin của tôi", false, "Admin", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "Body", "Character", "Children", "ConcurrencyStamp", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "EmailConfirmed", "FindPeople", "FullName", "Gender", "Height", "IAm", "Job", "LifeStyle", "Location", "LockoutEnabled", "LockoutEnd", "Marriage", "MostValuable", "NormalizedEmail", "NormalizedUserName", "PassWord", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Religion", "Role", "SecurityStamp", "Smoking", "Status", "Summary", "Target", "Title", "TwoFactorEnabled", "UserName", "Weight" },
                values: new object[] { "ec826af8-0310-48cf-8a14-da11bdb1c96e", 0, "vuong.jpg", 3, 3, 2, "9825592a-1d76-484a-809b-db6e7b6061e7", new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong@gmail.com", false, "Tìm người thương", "Nguyên Vương", 0, 170, 0, 7, 1, 38, false, null, 1, 1, null, null, "1111", null, "0396925225", false, 2, 0, "40367dba-52f5-463e-8d31-25b179bde7f4", 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 0, "Thông tin của tôi", false, "vuong", 65 });

            migrationBuilder.CreateIndex(
                name: "IX_HaveMessages_ReceiverId",
                table: "HaveMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_HaveMessages_SenderId",
                table: "HaveMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailImages_UserId",
                table: "ThumbnailImages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HaveMessages");

            migrationBuilder.DropTable(
                name: "ThumbnailImages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
