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
                    { new Guid("494f726a-36c3-49d7-b0b9-ebfa41bc73dc"), 3, "duc.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc2@gmail.com", 11, 0, "Trí Đức2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Duc", 65 },
                    { new Guid("ffbae934-b3a7-4897-8099-f64dcee55d95"), 3, "nhung.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung2@gmail.com", 11, 1, "Gia Nhung2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("a142e55c-fd1f-4b9e-9dd7-fff4177a3136"), 3, "diem.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem2@gmail.com", 11, 1, "Kiều Diễm2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Diem", 65 },
                    { new Guid("99657a02-5101-44f8-b96e-15871441f20e"), 3, "han.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han2@gmail.com", 11, 1, "Gia Hân2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Han", 65 },
                    { new Guid("be2a936b-c612-4877-a6cd-f3291ded0235"), 3, "mai.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai2@gmail.com", 11, 1, "Xuân Maiii2", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Mai", 65 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Role", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[] { new Guid("f5fc2cd9-d6b0-4694-88ec-822dce666481"), 3, "nhung2.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung3@gmail.com", 11, 1, "Nguyễn Huyền Nhung3", 0, 170, true, 7, 5, 37, 1, 3, "1111", "0369875463", 0, 1, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "nhung2", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("2a7d6946-4da4-44a3-8d6a-ec06368a00b5"), 0, "vuong.jpg", 3, 3, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong3@gmail.com", 7, 0, "Nguyên Vương3", 1, 170, true, 7, 1, 38, 2, 2, "1111", "0396925225", 2, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", "vuong", 65 },
                    { new Guid("70599229-5f70-43ff-939a-771c71977314"), 3, "hieu.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu3@gmail.com", 11, 0, "Võ Minh Hiếu3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "hieu", 65 },
                    { new Guid("b58df0cd-e276-41fe-9990-2a0290d74fb2"), 3, "tien.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien3@gmail.com", 11, 0, "Lê Minh Tiến3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "tien", 65 },
                    { new Guid("0b28d639-40e8-40a1-856f-95b87d796d80"), 3, "dinh.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh3@gmail.com", 11, 0, "Lê Kim Đỉnh3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("508c0a22-5c4a-49fc-abb3-27989f0dacf9"), 3, "dat.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat3@gmail.com", 11, 0, "Hồ Quốc Đạt3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dat", 65 },
                    { new Guid("b6b1aeb7-4d79-40cc-9f89-ca5a11b10d01"), 3, "son.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son3@gmail.com", 11, 0, "Phan Sơn3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Son", 65 },
                    { new Guid("e761d884-1b1d-4d46-b759-1ccd3fc05a02"), 3, "duc.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc3@gmail.com", 11, 0, "Trí Đức3", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Duc", 65 },
                    { new Guid("277cc9e0-85d3-43aa-8fed-924fcd616632"), 3, "nhung.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung3@gmail.com", 11, 1, "Gia Nhung3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("be3a8b41-735e-417f-a2d6-8a1ce492cab0"), 3, "diem.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem3@gmail.com", 11, 1, "Kiều Diễm3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Diem", 65 },
                    { new Guid("d2527d31-99b5-4810-b12c-ba0f080de179"), 3, "son.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son2@gmail.com", 11, 0, "Phan Sơn2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Son", 65 },
                    { new Guid("b96b2030-11f3-48ab-bca0-e96c78a8a3e6"), 3, "han.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han3@gmail.com", 11, 1, "Gia Hân3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Han", 65 },
                    { new Guid("569abcf7-cb03-4d54-b006-e239eb30f900"), 3, "dat.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat2@gmail.com", 11, 0, "Hồ Quốc Đạt2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dat", 65 },
                    { new Guid("38f986e0-2549-4c27-ac66-55916422af97"), 3, "tien.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien@gmail.com", 11, 0, "Lê Minh Tiến2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "tien", 65 }
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
                values: new object[] { new Guid("55a1618f-abd8-4972-ae02-b9cf1ab2e6d9"), 3, "nhung2.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung1@gmail.com", 11, 1, "Nguyễn Huyền Nhung2", 0, 170, true, 7, 5, 37, 1, 3, "1111", "0369875463", 0, 1, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "nhung2", 65 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "Religion", "Smoking", "Status", "Summary", "Target", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("8f8c15dd-9831-491e-bc5d-2786a510baa4"), 0, "vuong.jpg", 3, 3, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, "vuong2@gmail.com", 7, 0, "Nguyên Vương2", 1, 170, true, 7, 1, 38, 2, 2, "1111", "0396925225", 2, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", "vuong", 65 },
                    { new Guid("55816cde-5a3a-4769-a5c2-80cf5f3cee47"), 3, "hieu.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu2@gmail.com", 11, 0, "Võ Minh Hiếu2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "hieu", 65 },
                    { new Guid("d7b489b4-368b-46da-9359-52b00fe16867"), 3, "dinh.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh2@gmail.com", 11, 0, "Lê Kim Đỉnh2", 1, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("dba9f471-d67e-4e01-b9d8-b5facfdd782d"), 3, "mai.jpg", 2, 1, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai3@gmail.com", 11, 1, "Xuân Maiii3", 0, 170, true, 7, 3, 37, 1, 2, "1111", "0396925225", 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", "Mai", 65 }
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
