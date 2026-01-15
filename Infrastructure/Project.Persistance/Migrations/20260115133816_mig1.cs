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
                    { 1, "be28c748-40d0-4161-af38-977b3fe3608b", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN", 1, null },
                    { 2, "123bc3e1-add8-4136-a5d6-76b18834199c", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurant Müdürü", "RESTAURANT MÜDÜRÜ", 1, null },
                    { 3, "43959260-c5eb-4916-828f-0fb30d22e093", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "İnsan Kaynakları Müdürü", "İNSAN KAYNAKLARI MÜDÜRÜ", 1, null },
                    { 4, "45ef3670-7d0d-4aa8-bafc-eddd2dc04a72", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mutfak Şefi", "MUTFAK ŞEFİ", 1, null },
                    { 5, "069941b4-50ac-474c-8465-ca3213293dcd", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bar Şefi", "BAR ŞEFİ", 1, null },
                    { 6, "b1b8e549-d662-4374-b1b5-da1e33a872e9", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aşçı", "AŞÇI", 1, null },
                    { 7, "00b22760-bc8b-43b4-bda2-e18a8c8a9b7c", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barmen", "BARMEN", 1, null },
                    { 8, "f48a1e83-6ac2-44e8-b90c-b60e74656d2b", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "garson", "GARSON", 1, null },
                    { 9, "d0004a53-862c-45a4-a59e-002a972fcf6a", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hizmet Personeli", "HİZMET PERSONELİ", 1, null },
                    { 10, "03c00d72-3f1b-4987-b3a7-8a4978c0bb2a", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "İdari Personel", "İDARİ PERSONEL", 1, null },
                    { 11, "aa5b3905-0f04-4987-81e9-44cf801dbcee", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stajyer", "STAJYER", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletionDate", "Email", "EmailConfirmed", "InsertedDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "bd2d3f8d-ed12-4ed3-8b0d-06a108d606fe", null, "admin@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEGTPAaGYblWwF4SB1RjLTrsI9y+d82G2sg3mulr1KVGCVQyDYJF+/1k9JE2+5ustOg==", null, false, "60e41f6f-3ec2-4905-b29d-fd2f477db921", 1, false, null, "admin" },
                    { 2, 0, "46486b3a-6ee4-455b-a72e-63605fce245f", null, "mudur@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MÜDÜR", "AQAAAAIAAYagAAAAEAJAK1JbWX6FLrIb+oiMJoKsa49Hk0HrFyicpFdPa+9dY85qHJRkrPRw/+8g3G1Q6Q==", null, false, "c32e7a8b-0915-44d7-bd41-e75a4c96a397", 1, false, null, "müdür" },
                    { 3, 0, "7d96d85b-2873-4ef4-8c04-311fc8c97b7f", null, "insankaynaklari@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "INSANKAYNAKLARI", "AQAAAAIAAYagAAAAEL+tGoz76ny1o2SCtOkiQn4BpI4x5KA61JV3s0kpglex98MtmPEX303LfstJvzQgQg==", null, false, "d605cdd8-b699-4b3e-9448-227e737c5e41", 1, false, null, "insankaynaklari" },
                    { 4, 0, "a7d27b48-0ea1-478e-8977-35699ed3b03f", null, "mutfakşef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUTFAKŞEF", "AQAAAAIAAYagAAAAEMbXOIako1ITdY3YtBxIRMhqZGngDdsk/v0/k55R2xkNwj6zhBNIj5h/osiX//Nn/w==", null, false, "94f4a6ab-2989-4e7e-b3b1-4c27a70f1349", 1, false, null, "mutfakşef" },
                    { 5, 0, "ca20f5e4-73bc-4ef8-8364-c45eb8f7723b", null, "barşef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARŞEF", "AQAAAAIAAYagAAAAEEXEJ8ra1eCmgxBnRsKM/Q/BTLI+rkVTe+6ehZ5563DaIT8RH+oJSXHhh76CdbGh/Q==", null, false, "08ef8353-488e-4228-a8c5-022127dc9ef9", 1, false, null, "barşef" },
                    { 6, 0, "17416624-8c83-4583-b2a2-9eba6680aef2", null, "aşçı@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "AŞÇI", "AQAAAAIAAYagAAAAEHVY3SVUFGH68+SzUBpmwK+V0cjE+y0d8agTaWFtPGYC6ux6YffNHXLfcqa5CKiUhw==", null, false, "30379c70-7d83-4253-a1a8-1bd16964d4ea", 1, false, null, "aşçı" },
                    { 7, 0, "6f61f37a-8646-4fdb-a688-fd314a6ff2e2", null, "barmen@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARMEN", "AQAAAAIAAYagAAAAEOfxtQF+eUMpi9B3f/JwOGLa0VIQiboFbhoh9xApyXQI+y7vAP1VM7LeJ/0OS686nw==", null, false, "fb41bf7b-f6ae-492f-b7c0-ac888d166dca", 1, false, null, "barmen" },
                    { 8, 0, "e14f88f9-ad61-4d53-827a-05c3b904799a", null, "garson@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "GARSON", "AQAAAAIAAYagAAAAEEsfBxnFOrh2Opo588kQ9w8nlx0Jz10gLdgXt28w5rZm4EQYrks4uwGuo3UskcVZ6A==", null, false, "c9a0ca1d-997e-4a31-92a9-53f16478ddb5", 1, false, null, "garson" },
                    { 9, 0, "b63c9788-acb9-437b-800d-0e43fb30b41e", null, "idaripersonel@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "IDARIPERSONEL", "AQAAAAIAAYagAAAAELNfjBPZ6ukSd+2U1PiHmWYnR8jknjzD1eJj0BtgEFrXYso8ITkL8Npsl29JV9RhKw==", null, false, "0a888d63-b69a-4db1-89c5-f727d35f635b", 1, false, null, "idaripersonel" },
                    { 10, 0, "7e7bf340-6f56-4ba6-9428-1b50b046aae8", null, "hizmetpersoneli@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "HIZMETPERSONELI", "AQAAAAIAAYagAAAAEPFL3s0QEgyNEubD77brlfUf3NTUGR9/4kt4PuR15+laq7gPxU4ZQ+y+5DoGqyhi8g==", null, false, "49c1ae59-d4d4-4e4c-9b25-d763c5c20465", 1, false, null, "hizmetpersoneli" },
                    { 11, 0, "ed3edd63-dcc8-4187-9d30-b09ec339c14b", null, "stajyer@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "STAJYER", "AQAAAAIAAYagAAAAEFQRfM8NPXgMRN3ciG3XaVKqPS3keuhD6W34mEjvXQmi4xVEOsraUHd7KpOAEslAww==", null, false, "4762eb14-23e2-44cc-966d-b565b78bbf1e", 1, false, null, "stajyer" }
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
                    { 1, false, 5, null, new DateTime(2026, 1, 15, 16, 38, 14, 575, DateTimeKind.Local).AddTicks(2935), true, "Dana Bonfile", 1, 1, 500m, null },
                    { 2, false, 5, null, new DateTime(2026, 1, 15, 16, 38, 14, 575, DateTimeKind.Local).AddTicks(2970), true, "Tavuk Göğsü", 1, 1, 150m, null },
                    { 3, false, 6, null, new DateTime(2026, 1, 15, 16, 38, 14, 575, DateTimeKind.Local).AddTicks(2974), true, "Marul", 1, 2, 20m, null },
                    { 4, false, 6, null, new DateTime(2026, 1, 15, 16, 38, 14, 575, DateTimeKind.Local).AddTicks(2978), true, "Domates", 1, 1, 25m, null },
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
