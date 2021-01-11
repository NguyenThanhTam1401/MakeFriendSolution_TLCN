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
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    WeightRate = table.Column<double>(nullable: false, defaultValue: 1.0),
                    IsCalculated = table.Column<bool>(nullable: false, defaultValue: true),
                    Delete = table.Column<bool>(nullable: false, defaultValue: true),
                    IsDisplay = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimilariryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 1, 10, 18, 31, 11, 24, DateTimeKind.Local).AddTicks(134))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilariryFeatures", x => x.Id);
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
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 1, 10, 18, 31, 10, 998, DateTimeKind.Local).AddTicks(1911)),
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
                name: "FeatureDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    Weight = table.Column<int>(nullable: false, defaultValue: 1),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeatureDetails_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlockUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    IsLock = table.Column<bool>(nullable: false),
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
                name: "SimilarityScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    Score = table.Column<double>(nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarityScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarityScores_Users_FromUserId",
                        column: x => x.FromUserId,
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    NumberOflikes = table.Column<int>(nullable: false, defaultValue: 0)
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
                name: "UserFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    FeatureDetailId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Enable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFeatures_FeatureDetails_FeatureDetailId",
                        column: x => x.FeatureDetailId,
                        principalTable: "FeatureDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFeatures_Users_UserId",
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
                table: "Features",
                columns: new[] { "Id", "IsCalculated", "IsDisplay", "Name", "WeightRate" },
                values: new object[] { 4, true, true, "Điều quan trọng nhất", 1.0 });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsCalculated", "IsDisplay", "Name" },
                values: new object[] { 19, true, false, "Nhóm tuổi" });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsCalculated", "IsDisplay", "Name", "WeightRate" },
                values: new object[,]
                {
                    { 3, true, true, "Phong cách sống", 1.0 },
                    { 2, true, true, "Học vấn", 1.0 },
                    { 1, true, true, "Dáng người", 1.0 },
                    { 7, true, true, "Nhạc ưa thích", 1.0 },
                    { 8, true, true, "Bầu không khí ưa thích", 1.0 },
                    { 9, true, true, "Đi mua sắm", 1.0 },
                    { 5, true, true, "Tôn giáo", 1.0 },
                    { 10, true, true, "Đi du lịch", 1.0 },
                    { 12, true, true, "Nấu ăn", 1.0 },
                    { 13, true, true, "Công nghệ", 1.0 },
                    { 14, true, true, "Thú cưng", 1.0 },
                    { 15, true, true, "Chơi thể thao", 1.0 },
                    { 16, true, true, "Hút thuốc", 1.0 },
                    { 17, true, true, "Uống rượu bia", 1.0 }
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsCalculated", "IsDisplay", "Name" },
                values: new object[] { 18, true, false, "Giới tính" });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IsCalculated", "IsDisplay", "Name", "WeightRate" },
                values: new object[,]
                {
                    { 11, true, true, "Chơi game", 1.0 },
                    { 6, true, true, "Thể loại phim ưa thích", 1.0 }
                });

            migrationBuilder.InsertData(
                table: "SimilariryFeatures",
                columns: new[] { "Id", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 1, 10, 18, 31, 11, 31, DateTimeKind.Local).AddTicks(8622) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AtmosphereLike", "AvatarPath", "Body", "Character", "Cook", "CreatedAt", "Dob", "DrinkBeer", "Education", "Email", "FavoriteMovie", "FindPeople", "FullName", "Game", "Gender", "Height", "IsInfoUpdated", "Job", "LifeStyle", "LikePet", "LikeTechnology", "Location", "Marriage", "MostValuable", "PassWord", "PhoneNumber", "PlaySport", "Religion", "Role", "Shopping", "Smoking", "Status", "Summary", "Target", "Title", "Travel", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c969"), 3, "han.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "han@gmail.com", 11, 1, "Gia Hân", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Han", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c968"), 3, "diem.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "diem@gmail.com", 11, 1, "Kiều Diễm", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Diem", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c966"), 3, "nhung.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung1@gmail.com", 11, 1, "Gia Nhung", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "GiaNhung", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c964"), 3, "duc.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "duc@gmail.com", 11, 0, "Trí Đức", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Duc", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c963"), 3, "son.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "son@gmail.com", 11, 0, "Phan Sơn", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Son", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c962"), 3, "dat.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dat@gmail.com", 11, 0, "Hồ Quốc Đạt", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Dat", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c961"), 3, "dinh.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "dinh@gmail.com", 11, 0, "Lê Kim Đỉnh", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Dinh", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c965"), 3, "tien.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "tien@gmail.com", 11, 0, "Lê Minh Tiến", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "tien", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96f"), 3, "hieu.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "hieu@gmail.com", 11, 0, "Võ Minh Hiếu", 0, 1, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "hieu", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96e"), 0, "vuong.jpg", 3, 3, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, "vuong@gmail.com", 7, 0, "Nguyên Vương", 0, 1, 170, true, 7, 1, 0, 0, 38, 3, 2, "1111", "0396925225", 0, 2, 0, 0, 1, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", 3, "Thông tin của tôi", 0, "vuong", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c967"), 3, "nhung2.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "nhung@gmail.com", 11, 1, "Nguyễn Huyền Nhung", 0, 0, 170, true, 7, 6, 0, 0, 37, 1, 3, "1111", "0369875463", 0, 0, 1, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "nhung2", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c977"), 3, "mai.jpg", 2, 1, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, "mai@gmail.com", 11, 1, "Xuân Maiii", 0, 0, 170, true, 7, 3, 0, 0, 37, 1, 2, "1111", "0396925225", 0, 0, 0, 0, 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Mai", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96d"), 4, "tam.jpg", 2, 2, 0, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "tam@gmail.com", 9, 0, "Nguyễn Thành Tâm", 0, 1, 170, true, 7, 6, 0, 0, 37, 2, 5, "admin", "0396925225", 0, 1, 0, 0, 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", 4, "Thông tin của tôi", 0, "Admin", 65 }
                });

            migrationBuilder.InsertData(
                table: "FeatureDetails",
                columns: new[] { "Id", "Content", "FeatureId", "Weight" },
                values: new object[,]
                {
                    { 1, "Nhỏ nhắn", 1, 1 },
                    { 62, "Thỉnh thoảng", 12, 3 },
                    { 61, "Ít nấu ăn", 12, 2 },
                    { 60, "Không nấu ăn", 12, 1 },
                    { 59, "Nghiện game", 11, 4 },
                    { 58, "Thường xuyên", 11, 3 },
                    { 57, "Thỉnh thoảng", 11, 2 },
                    { 56, "Không chơi game", 11, 1 },
                    { 54, "Thường xuyên", 10, 3 },
                    { 63, "Thường xuyên", 12, 4 },
                    { 53, "Thỉnh thoảng", 10, 2 },
                    { 51, "Thường xuyên", 9, 3 },
                    { 50, "Thỉnh thoảng", 9, 2 },
                    { 49, "Ít khi đi", 9, 1 },
                    { 48, "Náo nhiệt", 8, 5 },
                    { 47, "Vui tươi", 8, 4 },
                    { 46, "Bình yên", 8, 3 },
                    { 45, "Êm đềm", 8, 2 },
                    { 44, "Tĩnh lặng", 8, 1 },
                    { 52, "Ít khi đi", 10, 1 },
                    { 43, "Nhạc Bolero", 7, 5 },
                    { 64, "Bình thường", 13, 1 },
                    { 66, "Tính đồ công nghệ", 13, 3 },
                    { 84, "Từ_31_Đến_40", 19, 4 },
                    { 83, "Từ_25_Đến_30", 19, 3 },
                    { 82, "Từ_18_Đến_25", 19, 2 },
                    { 81, "Dưới_18_Tuổi", 19, 1 },
                    { 80, "Cùng giới", 18, -1 },
                    { 79, "Khác giới", 18, 1 },
                    { 78, "Uống nhiều", 17, 3 },
                    { 77, "Uống xã giao", 17, 2 },
                    { 65, "Chỉ theo dõi", 13, 2 },
                    { 76, "Không uống", 17, 1 },
                    { 74, "Hút xã giao", 16, 2 },
                    { 73, "Không hút thuốc", 16, 1 },
                    { 72, "Thường xuyên", 15, 3 },
                    { 71, "Thỉnh thoảng", 15, 2 },
                    { 70, "Ít khi chơi", 15, 1 },
                    { 69, "Thích thú cưng", 14, 3 },
                    { 68, "Nuôi cho vui", 14, 2 },
                    { 67, "Không thích", 14, 1 },
                    { 75, "Hút nhiều", 16, 3 },
                    { 85, "Từ_41_Đến_50", 19, 5 },
                    { 42, "Rap - Hip hop", 7, 4 },
                    { 40, "Pop", 7, 2 },
                    { 19, "Tự do", 3, 7 },
                    { 18, "Tình cảm", 3, 6 },
                    { 17, "Năng động", 3, 5 },
                    { 16, "Lành mạnh", 3, 4 },
                    { 15, "Lạc quan", 3, 3 },
                    { 14, "Giản dị", 3, 2 },
                    { 13, "An nhàn", 3, 1 },
                    { 12, "Trên cao học", 2, 6 },
                    { 20, "Sức khỏe", 4, 1 },
                    { 11, "Cao học", 2, 5 },
                    { 9, "Cao đẳng", 2, 3 },
                    { 8, "Trung cấp", 2, 2 },
                    { 7, "Phổ thông", 2, 1 },
                    { 6, "Vạm vỡ", 1, 6 },
                    { 5, "Cao lớn", 1, 5 },
                    { 4, "Mũm mĩm", 1, 4 },
                    { 3, "Cân đối", 1, 3 },
                    { 2, "Mảnh mai", 1, 2 },
                    { 10, "Đại học", 2, 4 },
                    { 41, "Dance", 7, 3 },
                    { 21, "Thời gian", 4, 2 },
                    { 23, "Bạn đời", 4, 4 },
                    { 39, "Nhạc trẻ", 7, 1 },
                    { 38, "Hoạt hình", 6, 9 },
                    { 37, "Lãng mạn", 6, 8 },
                    { 36, "Kinh dị", 6, 7 },
                    { 35, "Hài hước", 6, 6 },
                    { 34, "Cổ trang", 6, 5 },
                    { 33, "Chiến tranh", 6, 4 },
                    { 32, "Chiến tranh", 6, 3 },
                    { 22, "Bạn bè", 4, 3 },
                    { 31, "Khoa học viễn tưởng", 6, 2 },
                    { 101, "Đạo khác", 5, 5 },
                    { 100, "Tin Lành", 5, 4 },
                    { 29, "Phật giáo", 5, 3 },
                    { 28, "Thiên Chúa giáo", 5, 2 },
                    { 27, "Không có đạo", 5, 1 },
                    { 26, "Niềm vui mỗi ngày", 4, 7 },
                    { 25, "Sự nghiệp", 4, 6 },
                    { 24, "Gia đình", 4, 5 },
                    { 30, "Hành động", 6, 1 },
                    { 86, "Trên_50", 19, 6 }
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
                name: "IX_FeatureDetails_FeatureId",
                table: "FeatureDetails",
                column: "FeatureId");

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
                name: "IX_SimilarityScores_FromUserId",
                table: "SimilarityScores",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ThumbnailImages_UserId",
                table: "ThumbnailImages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_FeatureDetailId",
                table: "UserFeatures",
                column: "FeatureDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_FeatureId",
                table: "UserFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeatures_UserId",
                table: "UserFeatures",
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
                name: "SimilariryFeatures");

            migrationBuilder.DropTable(
                name: "SimilarityScores");

            migrationBuilder.DropTable(
                name: "UserFeatures");

            migrationBuilder.DropTable(
                name: "ThumbnailImages");

            migrationBuilder.DropTable(
                name: "FeatureDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
