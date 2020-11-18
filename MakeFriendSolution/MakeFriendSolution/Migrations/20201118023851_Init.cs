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
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Role = table.Column<int>(nullable: false, defaultValue: 1),
                    TypeAccount = table.Column<int>(nullable: false, defaultValue: 0),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    Location = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
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
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96d"), 3, "Tam.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tam@gmail.com", 11, 0, "Nguyễn Thành Tâm", 1, 170, true, 7, 3, 37, 1, 2, "admin", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Admin", 65 },
                    { new Guid("7c6e6a3d-9cd0-4e83-b138-3d8fef6c6f02"), 3, "duc.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc2@gmail.com", 11, 0, "Trí Đức2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Duc", 65 },
                    { new Guid("901b87c1-0e36-43a3-9559-b12fbbc44cbc"), 3, "nhung.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung2@gmail.com", 11, 1, "Gia Nhung2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("925b370a-5338-4c3a-bb16-b97b9b2eaada"), 3, "diem.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem2@gmail.com", 11, 1, "Kiều Diễm2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Diem", 65 },
                    { new Guid("e53fec7e-cee7-4dff-8265-48a6515f7e8f"), 3, "han.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han2@gmail.com", 11, 1, "Gia Hân2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Han", 65 },
                    { new Guid("bc1fd1eb-5e55-4d3c-acb6-f9782ca00f02"), 3, "mai.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai2@gmail.com", 11, 1, "Xuân Maiii2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Mai", 65 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Role", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[] { new Guid("7beeb367-652a-4e48-a80e-db478cc58437"), 3, "nhung2.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung3@gmail.com", 11, 1, "Nguyễn Huyền Nhung3", 0, 170, true, 7, 5, 37, 1, 3, "1111", "0369875463", 0, 1, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "nhung2", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("945754b6-f87c-4598-a4dc-d16745c842f3"), 0, "vuong.jpg", 3, 3, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong3@gmail.com", 7, 0, "Nguyên Vương3", 1, 170, true, 7, 1, 38, 2, 2, "1111", "0396925225", 2, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", "vuong", 65 },
                    { new Guid("e75921bd-64aa-41f8-9b45-6cf19cbde837"), 3, "hieu.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu3@gmail.com", 11, 0, "Võ Minh Hiếu3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "hieu", 65 },
                    { new Guid("8d819f3f-5328-423c-9bc5-605537988cca"), 3, "tien.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien3@gmail.com", 11, 0, "Lê Minh Tiến3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "tien", 65 },
                    { new Guid("cbfdc6cd-eb5d-4259-8b06-a80be93a71b8"), 3, "dinh.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh3@gmail.com", 11, 0, "Lê Kim Đỉnh3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("af82e1bc-3530-4fd6-b019-b57d5868187f"), 3, "dat.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat3@gmail.com", 11, 0, "Hồ Quốc Đạt3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dat", 65 },
                    { new Guid("61c84792-d409-458e-ae76-14ec50815406"), 3, "son.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son3@gmail.com", 11, 0, "Phan Sơn3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Son", 65 },
                    { new Guid("5d8ec15f-f28e-4e6f-9e2a-d9ef61168d13"), 3, "duc.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc3@gmail.com", 11, 0, "Trí Đức3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Duc", 65 },
                    { new Guid("c631f5d9-c498-48dd-b829-3588c0595a3a"), 3, "nhung.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung3@gmail.com", 11, 1, "Gia Nhung3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("6a667043-d0ac-4dae-a43b-04105dbe0e1d"), 3, "diem.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem3@gmail.com", 11, 1, "Kiều Diễm3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Diem", 65 },
                    { new Guid("034100f4-7934-406d-a48a-ada906f78e58"), 3, "son.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son2@gmail.com", 11, 0, "Phan Sơn2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Son", 65 },
                    { new Guid("f068afad-224f-4849-b545-2eee4e00147b"), 3, "han.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han3@gmail.com", 11, 1, "Gia Hân3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Han", 65 },
                    { new Guid("61fa945b-db68-4a4a-a2d5-b5fa96671d1b"), 3, "dat.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat2@gmail.com", 11, 0, "Hồ Quốc Đạt2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dat", 65 },
                    { new Guid("ee84d4d7-5c5f-45e6-ad63-1aaa12d73398"), 3, "tien.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien@gmail.com", 11, 0, "Lê Minh Tiến2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "tien", 65 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Role", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[] { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c967"), 3, "nhung2.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung@gmail.com", 11, 1, "Nguyễn Huyền Nhung", 0, 170, true, 7, 5, 37, 1, 3, "1111", "0369875463", 0, 1, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "nhung2", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96e"), 0, "vuong.jpg", 3, 3, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong@gmail.com", 7, 0, "Nguyên Vương", 1, 170, true, 7, 1, 38, 2, 2, "1111", "0396925225", 2, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", "vuong", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96f"), 3, "hieu.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu@gmail.com", 11, 0, "Võ Minh Hiếu", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "hieu", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c965"), 3, "tien.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien@gmail.com", 11, 0, "Lê Minh Tiến", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "tien", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c961"), 3, "dinh.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh@gmail.com", 11, 0, "Lê Kim Đỉnh", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c962"), 3, "dat.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat@gmail.com", 11, 0, "Hồ Quốc Đạt", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dat", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c963"), 3, "son.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son@gmail.com", 11, 0, "Phan Sơn", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Son", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c964"), 3, "duc.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc@gmail.com", 11, 0, "Trí Đức", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Duc", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c966"), 3, "nhung.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung1@gmail.com", 11, 1, "Gia Nhung", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c968"), 3, "diem.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem@gmail.com", 11, 1, "Kiều Diễm", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Diem", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c969"), 3, "han.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han@gmail.com", 11, 1, "Gia Hân", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Han", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c977"), 3, "mai.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai@gmail.com", 11, 1, "Xuân Maiii", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Mai", 65 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Role", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[] { new Guid("8ddab316-8252-4528-b099-ac1029c6ec49"), 3, "nhung2.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung1@gmail.com", 11, 1, "Nguyễn Huyền Nhung2", 0, 170, true, 7, 5, 37, 1, 3, "1111", "0369875463", 0, 1, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "nhung2", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("a921ec3b-7bd4-4ee7-aeb4-22198e9b144e"), 0, "vuong.jpg", 3, 3, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong2@gmail.com", 7, 0, "Nguyên Vương2", 1, 170, true, 7, 1, 38, 2, 2, "1111", "0396925225", 2, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", "vuong", 65 },
                    { new Guid("eb6adc87-2430-4a57-90a6-f8ba8a7f24d3"), 3, "hieu.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu2@gmail.com", 11, 0, "Võ Minh Hiếu2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "hieu", 65 },
                    { new Guid("de22d9d2-4dea-4d4d-af56-05791bb4d999"), 3, "dinh.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh2@gmail.com", 11, 0, "Lê Kim Đỉnh2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("e87370c4-1069-4a29-a9aa-03c61c8a6e31"), 3, "mai.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai3@gmail.com", 11, 1, "Xuân Maiii3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Mai", 65 }
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
