using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeFriendSolution.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    AuthorizeCount = table.Column<int>(nullable: false, defaultValue: 0),
                    UnauthorizeCount = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Role = table.Column<int>(nullable: false),
                    TypeAccount = table.Column<int>(nullable: false, defaultValue: 0),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    Location = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    NumberOfFiends = table.Column<int>(nullable: false, defaultValue: 0),
                    NumberOfLikes = table.Column<int>(nullable: false, defaultValue: 0),
                    NumberOfImages = table.Column<int>(nullable: false, defaultValue: 0),
                    IsInfoUpdated = table.Column<bool>(nullable: false, defaultValue: false),
                    PasswordForgottenCode = table.Column<string>(nullable: true, defaultValue: ""),
                    PasswordForgottenPeriod = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    NumberOfPasswordConfirmations = table.Column<int>(nullable: false, defaultValue: 0),
                    Title = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    FindPeople = table.Column<int>(nullable: false),
                    Marriage = table.Column<int>(nullable: false),
                    Target = table.Column<int>(nullable: false),
                    Education = table.Column<int>(nullable: false),
                    Body = table.Column<int>(nullable: false),
                    Character = table.Column<int>(nullable: false),
                    LifeStyle = table.Column<int>(nullable: false),
                    MostValuable = table.Column<int>(nullable: false),
                    Job = table.Column<int>(nullable: false),
                    Religion = table.Column<int>(nullable: false),
                    FavoriteMovie = table.Column<int>(nullable: false),
                    AtmosphereLike = table.Column<int>(nullable: false),
                    Shopping = table.Column<int>(nullable: false),
                    Travel = table.Column<int>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    Cook = table.Column<int>(nullable: false),
                    LikeTechnology = table.Column<int>(nullable: false),
                    LikePet = table.Column<int>(nullable: false),
                    PlaySport = table.Column<int>(nullable: false),
                    Smoking = table.Column<int>(nullable: false),
                    DrinkBeer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlockUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlockUsers_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlockUsers_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Follows_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
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
                    SenderId = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeImages_ThumbnailImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ThumbnailImages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LikeImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "Cook", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Game", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "LikePet", "LikeTechnology", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "PlaySport", "Religion", "Role", "Shopping", "Smoking", "Status", "Summary", "Target", "Title", "Travel", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96d"), 4, "tam.jpg", 2, 2, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "tam@gmail.com", 9, 0, "Nguyễn Thành Tâm", 0, 1, 170, true, 7, 6, 0, 0, 37, 2, 5, "admin", "0396925225", 0, 1, 0, 0, 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Admin", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c967"), 3, "nhung2.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung@gmail.com", 11, 1, "Nguyễn Huyền Nhung", 0, 0, 170, true, 7, 6, 0, 0, 37, 1, 3, "1111", "0369875463", 0, 0, 1, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "nhung2", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96e"), 0, "vuong.jpg", 3, 3, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, "vuong@gmail.com", 7, 0, "Nguyên Vương", 0, 1, 170, true, 7, 1, 0, 0, 38, 3, 2, "1111", "0396925225", 0, 2, 0, 0, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", 0, "vuong", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96f"), 3, "hieu.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu@gmail.com", 11, 0, "Võ Minh Hiếu", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "hieu", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c965"), 3, "tien.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien@gmail.com", 11, 0, "Lê Minh Tiến", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "tien", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c961"), 3, "dinh.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh@gmail.com", 11, 0, "Lê Kim Đỉnh", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Dinh", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c962"), 3, "dat.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat@gmail.com", 11, 0, "Hồ Quốc Đạt", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Dat", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c963"), 3, "son.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son@gmail.com", 11, 0, "Phan Sơn", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Son", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c964"), 3, "duc.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc@gmail.com", 11, 0, "Trí Đức", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Duc", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c966"), 3, "nhung.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung1@gmail.com", 11, 1, "Gia Nhung", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "GiaNhung", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c968"), 3, "diem.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem@gmail.com", 11, 1, "Kiều Diễm", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Diem", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c969"), 3, "han.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han@gmail.com", 11, 1, "Gia Hân", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Han", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c977"), 3, "mai.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai@gmail.com", 11, 1, "Xuân Maiii", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Mai", 65 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockUsers_FromUserId",
                table: "BlockUsers",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockUsers_ToUserId",
                table: "BlockUsers",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FromUserId",
                table: "Favorites",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ToUserId",
                table: "Favorites",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FromUserId",
                table: "Follows",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_ToUserId",
                table: "Follows",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HaveMessages_ReceiverId",
                table: "HaveMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_HaveMessages_SenderId",
                table: "HaveMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeImages_ImageId",
                table: "LikeImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeImages_UserId",
                table: "LikeImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailImages_UserId",
                table: "ThumbnailImages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "BlockUsers");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "HaveMessages");

            migrationBuilder.DropTable(
                name: "LikeImages");

            migrationBuilder.DropTable(
                name: "ThumbnailImages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
