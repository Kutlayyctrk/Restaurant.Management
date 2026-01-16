using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCKNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "money", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserProfiles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableStatus = table.Column<int>(type: "int", nullable: false),
                    WaiterId = table.Column<int>(type: "int", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_AspNetUsers_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSellable = table.Column<bool>(type: "bit", nullable: false),
                    CanBeProduced = table.Column<bool>(type: "bit", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    WaiterId = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransActionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransActions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransActions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransActions_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeItems_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletionDate", "InsertedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "71b7ce7b-13b0-44f0-8544-bc58aff796cc", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN", 1, null },
                    { 2, "31799a81-8fb1-475a-b1be-0074af288a50", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurant Müdürü", "RESTAURANT MÜDÜRÜ", 1, null },
                    { 3, "e6ea9871-424a-409b-81bb-0470f286d0e2", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "İnsan Kaynakları Müdürü", "İNSAN KAYNAKLARI MÜDÜRÜ", 1, null },
                    { 4, "e063fb5d-e25b-47ad-81a5-5750577dfbb9", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mutfak Şefi", "MUTFAK ŞEFİ", 1, null },
                    { 5, "f2677904-7cc8-4b8c-ad56-de5119aad971", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bar Şefi", "BAR ŞEFİ", 1, null },
                    { 6, "046eb7f5-d15b-48ef-8263-60f2b90b63a4", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aşçı", "AŞÇI", 1, null },
                    { 7, "459cd4ad-3a7c-4055-b74e-4823e6a3a015", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barmen", "BARMEN", 1, null },
                    { 8, "b2f44450-159b-4287-a20a-57b4295cf917", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "garson", "GARSON", 1, null },
                    { 9, "096afaad-7702-4267-9f47-a661b5a98900", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hizmet Personeli", "HİZMET PERSONELİ", 1, null },
                    { 10, "e3fbbd20-2737-453b-8322-8a3baf7bb340", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "İdari Personel", "İDARİ PERSONEL", 1, null },
                    { 11, "f09a294d-a461-446f-80e1-96ef3935bf7d", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stajyer", "STAJYER", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletionDate", "Email", "EmailConfirmed", "InsertedDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "d93ee359-1e06-417c-aa99-9653916861e5", null, "admin@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEMyXuRvJAmSfIgvphlfJ/pu+Ws9IoIFVhhQXc8kHPIp+A50KNBSL4ExL6jKwmP0BzA==", null, false, "8929952a-8a99-41dc-95e6-d5d77811a42b", 1, false, null, "admin" },
                    { 2, 0, "b2b1d82f-1fa9-4c99-a5cf-24426ea4813b", null, "mudur@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MÜDÜR", "AQAAAAIAAYagAAAAEFtw+0uabfB5n/nOfGBNuBz9PTPbEuW91XW7SZVK6sSFujhEykGN3sFKjSGBp+eROg==", null, false, "86bed055-da75-4233-8c07-50e688977a56", 1, false, null, "müdür" },
                    { 3, 0, "2b9b43a7-166b-4c40-8764-acf414e3ca05", null, "insankaynaklari@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "INSANKAYNAKLARI", "AQAAAAIAAYagAAAAEFkp/6Hn8jUsNYXFgyS6oNXJtaS9sq9tzDiAEofAaJTOSvBWRLZ4TxW+GuMm3DFnIg==", null, false, "937c8528-a665-4f68-a898-866f29d27fe1", 1, false, null, "insankaynaklari" },
                    { 4, 0, "8579c9a1-fa91-4d73-9e6d-9b830faf09f5", null, "mutfakşef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUTFAKŞEF", "AQAAAAIAAYagAAAAEOZA2gwpkmYEOyBSnCR5fxephhsohdYgK6qAPtgy9PoL4OGIuYJ4SaOvSnMUI3NIxw==", null, false, "316b08fc-86f2-4e25-a26a-5fc052ebbed2", 1, false, null, "mutfakşef" },
                    { 5, 0, "e8ed2d86-9d44-49fd-89d0-56da559393dc", null, "barşef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARŞEF", "AQAAAAIAAYagAAAAELMsj4J6c0y59XiXD+PheUFpT7ivxuaq4oq7bDACa9cIpac2e+Rr3m9iT//sXOZfTQ==", null, false, "c287da95-410c-4468-895f-f8b35aecc5c3", 1, false, null, "barşef" },
                    { 6, 0, "ea55f6bb-dbff-4493-905d-b6e79bc3a664", null, "aşçı@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "AŞÇI", "AQAAAAIAAYagAAAAELopJ3TZ5gb1bwaPov1A9Zt76Tov+Ctbuzaj5Mt+dZMiatjNE9ytMc3Aco4zlVnYrg==", null, false, "5dc46cc2-9429-49c1-b9f7-7805d81524e9", 1, false, null, "aşçı" },
                    { 7, 0, "d0de8833-317d-4d5c-90b6-e5a104f186f0", null, "barmen@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARMEN", "AQAAAAIAAYagAAAAEHm6LJujQgZVnvBsy8/zkGRS5ggHQhlqRWe6XDv0a6bcERG5IqaHdiwPsA5WmsWRqA==", null, false, "02b9284f-5176-4ca9-ab90-9b19144a4816", 1, false, null, "barmen" },
                    { 8, 0, "48c96dc0-5f64-4fe2-b216-d531d3f37794", null, "garson@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "GARSON", "AQAAAAIAAYagAAAAENJWzzOmxRuzyMJMfrGrHeAD2RmmX9TMeu4mbI8E+maENCot4YP9Tcvep3MypXz01g==", null, false, "e2485856-e5a5-4d89-95ac-a3121a966ce4", 1, false, null, "garson" },
                    { 9, 0, "a7638a9c-28c5-4f97-9169-7cbf360919c4", null, "idaripersonel@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "IDARIPERSONEL", "AQAAAAIAAYagAAAAEIpqg5fjoePx2FinSxx2GKeyEgYRil9LjBsH0an02pN27cgRGxGqj0/3+sveMgxRhg==", null, false, "c2b438d9-d8fd-42ad-8da2-116de2512b3a", 1, false, null, "idaripersonel" },
                    { 10, 0, "7f670569-af26-417f-9b55-4364d9f24195", null, "hizmetpersoneli@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "HIZMETPERSONELI", "AQAAAAIAAYagAAAAEO4IXfxW5IddM88tFjK52CvDAjPPNcmLouECX1d1jRztrJ/Xu6M+Djj/RSprIYmkuQ==", null, false, "6de3a25f-9492-4aac-b6db-497bc953f8de", 1, false, null, "hizmetpersoneli" },
                    { 11, 0, "a35e81b5-f137-4025-83ba-ae1a8257e715", null, "stajyer@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "STAJYER", "AQAAAAIAAYagAAAAEOzVMrGRGfHlGPep12h3TQ17P4vzCSasm/B3R0Debc+SSW59ezFBPXcR4/cM8aQ6Sg==", null, false, "c7073e9b-f5a4-4479-9dd3-97786c0d1565", 1, false, null, "stajyer" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "DeletionDate", "Description", "InsertedDate", "ParentCategoryId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Yiyecekler", null, "Ana Gıda Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 2, "İçecekler", null, "Ana İçecek Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 3, "Sarf Malzemeleri", null, "Ana Sarf Malzemeleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 4, "DemirBaş", null, "Ana DemirBaş Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactName", "DeletionDate", "Email", "InsertedDate", "PhoneNumber", "Status", "SupplierName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "İkitelli OSB, İstanbul", "Ali Yıldız", null, "info@lezzettedarik.com", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+90 (212) 555 0101", 1, "Lezzet Tedarik A.Ş.", null },
                    { 2, "Gaziemir, İzmir", "Ayşe Demir", null, "siparis@mutfakmalzemeleri.com", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+90 (232) 555 0202", 1, "Mutfak Malzemeleri Ltd.", null },
                    { 3, "Çankaya, Ankara", "Mehmet Kaya", null, "iletisim@dogalurunler.org", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+90 (312) 555 0303", 1, "Doğal Ürünler Kooperatifi", null },
                    { 4, "Kartal, İstanbul", "Zeynep Çelik", null, "destek@icecekmerkezi.com", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+90 (216) 555 0404", 1, "İçecek Merkezi", null },
                    { 5, "Beşiktaş, İstanbul", "Cengiz Ak", null, "tedarik@kafebar.com", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "+90 (212) 555 0707", 1, "Kafe & Bar Tedarik", null }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "Status", "TableNumber", "TableStatus", "UpdatedDate", "WaiterId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 1", 0, null, null },
                    { 2, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 2", 0, null, null },
                    { 3, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 3", 0, null, null },
                    { 4, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 4", 0, null, null },
                    { 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 5", 0, null, null },
                    { 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 6", 0, null, null },
                    { 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Masa 7", 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "Status", "UnitAbbreviation", "UnitName", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "kg", "Kilogram", null },
                    { 2, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "ad", "Adet", null },
                    { 3, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "lt", "Litre", null },
                    { 4, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "pkt", "Paket", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserProfiles",
                columns: new[] { "Id", "AppUserId", "BirthDate", "DeletionDate", "FirstName", "HireDate", "InsertedDate", "LastName", "Salary", "Status", "TCKNo", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1990, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahmet", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yılmaz", 2147483647m, 1, "12345678901", null },
                    { 2, 2, new DateTime(1985, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mehmet", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Demir", 150000m, 1, "10987654321", null },
                    { 3, 3, new DateTime(1992, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ayşe", new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kara", 120000m, 1, "23456789012", null },
                    { 4, 4, new DateTime(1995, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fatma", new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çelik", 115000m, 1, "34567890123", null },
                    { 5, 5, new DateTime(1993, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ali", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Şahin", 110000m, 1, "45678901234", null },
                    { 6, 6, new DateTime(1996, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zeynep", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yıldız", 90000m, 1, "56789012345", null },
                    { 7, 7, new DateTime(1994, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Can", new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Polat", 85000m, 1, "67890123456", null },
                    { 8, 8, new DateTime(1997, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Elif", new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aydın", 80000m, 1, "78901234567", null },
                    { 9, 9, new DateTime(1991, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mert", new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kılıç", 95000m, 1, "89012345678", null },
                    { 10, 10, new DateTime(1988, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Seda", new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Güneş", 105000m, 1, "90123456789", null },
                    { 11, 11, new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Burak", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arslan", 60000m, 1, "01234567890", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "DeletionDate", "Id", "InsertedDate", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, null, 1, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, 2, null, 2, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 3, 3, null, 3, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 4, 4, null, 4, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 5, 5, null, 5, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 6, 6, null, 6, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 7, 7, null, 7, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 8, 8, null, 8, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 9, 9, null, 9, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 10, 10, null, 10, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 11, 11, null, 11, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "DeletionDate", "Description", "InsertedDate", "ParentCategoryId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 5, "Etler", null, "Et Ürünleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null },
                    { 6, "Sebzeler", null, "Sebze Ürünleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null },
                    { 7, "Meyveler", null, "Meyve Ürünleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null },
                    { 8, "Alkollü İçecekler", null, "Alkollü İçecek Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null },
                    { 9, "Alkolsüz İçecekler", null, "Alkolsüz İçecek Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null },
                    { 10, "Temizlik Malzemeleri", null, "Temizlik Malzemeleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, null },
                    { 11, "Ofis Malzemeleri", null, "Ofis Malzemeleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, null },
                    { 12, "Mutfak Ekipmanları", null, "Mutfak Ekipmanları Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, null },
                    { 13, "Mobilyalar", null, "Mobilya Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CanBeProduced", "CategoryId", "DeletionDate", "InsertedDate", "IsSellable", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, false, 5, null, new DateTime(2026, 1, 16, 20, 44, 26, 103, DateTimeKind.Local).AddTicks(3601), true, "Dana Bonfile", 1, 1, 500m, null },
                    { 2, false, 5, null, new DateTime(2026, 1, 16, 20, 44, 26, 103, DateTimeKind.Local).AddTicks(3637), true, "Tavuk Göğsü", 1, 1, 150m, null },
                    { 3, false, 6, null, new DateTime(2026, 1, 16, 20, 44, 26, 103, DateTimeKind.Local).AddTicks(3641), true, "Marul", 1, 2, 20m, null },
                    { 4, false, 6, null, new DateTime(2026, 1, 16, 20, 44, 26, 103, DateTimeKind.Local).AddTicks(3645), true, "Domates", 1, 1, 25m, null },
                    { 5, false, 7, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Elma", 1, 1, 30m, null },
                    { 6, false, 7, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Muz", 1, 1, 40m, null },
                    { 7, false, 8, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Kola", 1, 3, 15m, null },
                    { 8, false, 8, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Gazoz", 1, 3, 12m, null },
                    { 9, false, 9, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Ayran", 1, 3, 10m, null },
                    { 10, false, 9, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Meyve Suyu", 1, 3, 18m, null },
                    { 11, false, 10, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Deterjan", 1, 4, 60m, null },
                    { 12, false, 10, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Çamaşır Suyu", 1, 4, 25m, null },
                    { 13, false, 11, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Kalem", 1, 4, 2m, null },
                    { 14, false, 11, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Defter", 1, 2, 15m, null },
                    { 15, false, 12, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Tencere", 1, 2, 200m, null },
                    { 16, false, 12, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Bıçak Seti", 1, 2, 150m, null },
                    { 17, false, 13, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Sandalye", 1, 2, 300m, null },
                    { 18, false, 13, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Masa", 1, 2, 700m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserProfiles_AppUserId",
                table: "AppUserProfiles",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId_RoleId",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId_ProductId",
                table: "OrderDetails",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WaiterId",
                table: "Orders",
                column: "WaiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_ProductId",
                table: "RecipeItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_RecipeId",
                table: "RecipeItems",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeItems_UnitId",
                table: "RecipeItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductId",
                table: "Recipes",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockTransActions_AppUserId",
                table: "StockTransActions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransActions_ProductId",
                table: "StockTransActions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransActions_SupplierId",
                table: "StockTransActions",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_WaiterId",
                table: "Tables",
                column: "WaiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "StockTransActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
