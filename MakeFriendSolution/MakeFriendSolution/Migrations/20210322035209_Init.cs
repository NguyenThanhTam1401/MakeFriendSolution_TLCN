using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    WeightRate = table.Column<double>(nullable: false, defaultValue: 1.0),
                    IsCalculated = table.Column<bool>(nullable: false, defaultValue: true),
                    IsSearchFeature = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    AutoFilter = table.Column<bool>(nullable: false),
                    Drawings = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Hentai = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Neutral = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Porn = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Sexy = table.Column<double>(nullable: false, defaultValue: 0.0),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FromId = table.Column<Guid>(nullable: false),
                    ToId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimilariryFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 3, 22, 10, 52, 8, 633, DateTimeKind.Local).AddTicks(8083))
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
                    ConnectionId = table.Column<string>(nullable: true),
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
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 3, 22, 10, 52, 8, 609, DateTimeKind.Local).AddTicks(8185)),
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
                    Job = table.Column<int>(nullable: false),
                    FindPeople = table.Column<int>(nullable: false),
                    FindAgeGroup = table.Column<int>(nullable: false)
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false, defaultValue: "Image title"),
                    ImagePath = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    NumberOflikes = table.Column<int>(nullable: false, defaultValue: 0),
                    Status = table.Column<int>(nullable: false)
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
                name: "SearchFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    FeatureDetailId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchFeatures_FeatureDetails_FeatureDetailId",
                        column: x => x.FeatureDetailId,
                        principalTable: "FeatureDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SearchFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SearchFeatures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    FeatureDetailId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
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
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
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
                columns: new[] { "Id", "IsCalculated", "IsSearchFeature", "Name", "WeightRate" },
                values: new object[,]
                {
                    { 2, true, true, "Học vấn", 1.0 },
                    { 16, true, false, "Hút thuốc", 1.0 },
                    { 15, true, false, "Chơi thể thao", 1.0 },
                    { 14, true, false, "Thú cưng", 1.0 },
                    { 13, true, false, "Công nghệ", 1.0 },
                    { 12, true, false, "Nấu ăn", 1.0 },
                    { 11, true, false, "Chơi game", 1.0 },
                    { 10, true, false, "Đi du lịch", 1.0 },
                    { 9, true, false, "Đi mua sắm", 1.0 },
                    { 8, true, false, "Bầu không khí ưa thích", 1.0 },
                    { 7, true, false, "Nhạc ưa thích", 1.0 },
                    { 6, true, false, "Thể loại phim ưa thích", 1.0 },
                    { 5, true, false, "Tôn giáo", 1.0 },
                    { 1, true, true, "Dáng người", 1.0 },
                    { 17, true, false, "Uống rượu bia", 1.0 },
                    { 3, true, false, "Phong cách sống", 1.0 }
                });

            migrationBuilder.InsertData(
                table: "SimilariryFeatures",
                columns: new[] { "Id", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2021, 3, 22, 10, 52, 8, 643, DateTimeKind.Local).AddTicks(7005) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarPath", "ConnectionId", "CreatedAt", "Dob", "Email", "FindAgeGroup", "FindPeople", "FullName", "Gender", "Height", "IsInfoUpdated", "Job", "Location", "PassWord", "PhoneNumber", "Role", "Status", "Summary", "Title", "UserName", "Weight" },
                values: new object[,]
                {
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c969"), "han.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "han@gmail.com", 0, 1, "Gia Hân", 0, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Han", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c968"), "diem.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "diem@gmail.com", 0, 1, "Kiều Diễm", 0, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Diem", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c966"), "nhung.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "nhung1@gmail.com", 0, 1, "Gia Nhung", 0, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "GiaNhung", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c964"), "duc.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "duc@gmail.com", 0, 0, "Trí Đức", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Duc", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c963"), "son.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "son@gmail.com", 0, 0, "Phan Sơn", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Son", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c962"), "dat.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dat@gmail.com", 0, 0, "Hồ Quốc Đạt", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Dat", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c961"), "dinh.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "dinh@gmail.com", 0, 0, "Lê Kim Đỉnh", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Dinh", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c965"), "tien.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "tien@gmail.com", 0, 0, "Lê Minh Tiến", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "tien", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96f"), "hieu.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "hieu@gmail.com", 0, 0, "Võ Minh Hiếu", 1, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "hieu", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96e"), "vuong.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "vuong@gmail.com", 0, 0, "Nguyên Vương", 1, 170, true, 7, 38, "1111", "0396925225", 0, 0, "Tôi là Vương, rất vui khi được làm quen với bạn", "Thông tin của tôi", "vuong", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c967"), "nhung2.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "nhung@gmail.com", 0, 1, "Nguyễn Huyền Nhung", 0, 170, true, 7, 37, "1111", "0369875463", 1, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "nhung2", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c977"), "mai.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "mai@gmail.com", 0, 1, "Xuân Maiii", 0, 170, true, 7, 37, "1111", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Mai", 65 },
                    { new Guid("ec826af8-0310-48cf-8a14-da11bdb1c96d"), "tam.jpg", null, new DateTime(2020, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1999, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "tam@gmail.com", 0, 0, "Nguyễn Thành Tâm", 1, 170, true, 7, 37, "admin", "0396925225", 0, 0, "Tôi là Tâm, rất vui khi được làm quen với bạn", "Thông tin của tôi", "Admin", 65 }
                });

            migrationBuilder.InsertData(
                table: "FeatureDetails",
                columns: new[] { "Id", "Content", "FeatureId", "Weight" },
                values: new object[,]
                {
                    { 1, "Nhỏ nhắn", 1, 1 },
                    { 58, "Thường xuyên", 11, 3 },
                    { 57, "Thỉnh thoảng", 11, 2 },
                    { 56, "Không chơi game", 11, 1 },
                    { 54, "Thường xuyên", 10, 3 },
                    { 53, "Thỉnh thoảng", 10, 2 },
                    { 52, "Ít khi đi", 10, 1 },
                    { 59, "Nghiện game", 11, 4 },
                    { 51, "Thường xuyên", 9, 3 },
                    { 49, "Ít khi đi", 9, 1 },
                    { 48, "Náo nhiệt", 8, 5 },
                    { 47, "Vui tươi", 8, 4 },
                    { 46, "Bình yên", 8, 3 },
                    { 45, "Êm đềm", 8, 2 },
                    { 44, "Tĩnh lặng", 8, 1 },
                    { 50, "Thỉnh thoảng", 9, 2 },
                    { 43, "Nhạc Bolero", 7, 5 },
                    { 60, "Không nấu ăn", 12, 1 },
                    { 62, "Thỉnh thoảng", 12, 3 },
                    { 76, "Không uống", 17, 1 },
                    { 75, "Hút nhiều", 16, 3 },
                    { 74, "Hút xã giao", 16, 2 },
                    { 73, "Không hút thuốc", 16, 1 },
                    { 72, "Thường xuyên", 15, 3 },
                    { 71, "Thỉnh thoảng", 15, 2 },
                    { 61, "Ít nấu ăn", 12, 2 },
                    { 70, "Ít khi chơi", 15, 1 },
                    { 68, "Nuôi cho vui", 14, 2 },
                    { 67, "Không thích", 14, 1 },
                    { 66, "Tính đồ công nghệ", 13, 3 },
                    { 65, "Chỉ theo dõi", 13, 2 },
                    { 64, "Bình thường", 13, 1 },
                    { 63, "Thường xuyên", 12, 4 },
                    { 69, "Thích thú cưng", 14, 3 },
                    { 42, "Rap - Hip hop", 7, 4 },
                    { 41, "Dance", 7, 3 },
                    { 40, "Pop", 7, 2 },
                    { 15, "Lạc quan", 3, 3 },
                    { 14, "Giản dị", 3, 2 },
                    { 13, "An nhàn", 3, 1 },
                    { 12, "Trên cao học", 2, 6 },
                    { 11, "Cao học", 2, 5 },
                    { 10, "Đại học", 2, 4 },
                    { 16, "Lành mạnh", 3, 4 },
                    { 9, "Cao đẳng", 2, 3 },
                    { 7, "Phổ thông", 2, 1 },
                    { 6, "Vạm vỡ", 1, 6 },
                    { 5, "Cao lớn", 1, 5 },
                    { 4, "Mũm mĩm", 1, 4 },
                    { 3, "Cân đối", 1, 3 },
                    { 2, "Mảnh mai", 1, 2 },
                    { 8, "Trung cấp", 2, 2 },
                    { 17, "Năng động", 3, 5 },
                    { 18, "Tình cảm", 3, 6 },
                    { 19, "Tự do", 3, 7 },
                    { 39, "Nhạc trẻ", 7, 1 },
                    { 38, "Hoạt hình", 6, 9 },
                    { 37, "Lãng mạn", 6, 8 },
                    { 36, "Kinh dị", 6, 7 },
                    { 35, "Hài hước", 6, 6 },
                    { 34, "Cổ trang", 6, 5 },
                    { 33, "Chiến tranh", 6, 4 },
                    { 32, "Chiến tranh", 6, 3 },
                    { 31, "Khoa học viễn tưởng", 6, 2 },
                    { 30, "Hành động", 6, 1 },
                    { 101, "Đạo khác", 5, 5 },
                    { 100, "Tin Lành", 5, 4 },
                    { 29, "Phật giáo", 5, 3 },
                    { 28, "Thiên Chúa giáo", 5, 2 },
                    { 27, "Không có đạo", 5, 1 },
                    { 77, "Uống xã giao", 17, 2 },
                    { 78, "Uống nhiều", 17, 3 }
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
                name: "IX_SearchFeatures_FeatureDetailId",
                table: "SearchFeatures",
                column: "FeatureDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFeatures_FeatureId",
                table: "SearchFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchFeatures_UserId",
                table: "SearchFeatures",
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
                name: "ImageScores");

            migrationBuilder.DropTable(
                name: "LikeImages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "SearchFeatures");

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
