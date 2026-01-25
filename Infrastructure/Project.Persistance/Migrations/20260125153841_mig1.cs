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
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
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
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsSellable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsExtra = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CanBeProduced = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsReadyMade = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    TableId = table.Column<int>(type: "int", nullable: true),
                    WaiterId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Orders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
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
                name: "MenuProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuProducts_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetailState = table.Column<int>(type: "int", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "StockTransActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    OrderDetailId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransActions_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "DeletionDate", "InsertedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "06c1484f-2abc-4eed-b232-cb55ee0db43d", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN", 1, null },
                    { 2, "8044888b-64ba-4334-957c-592ee10ab3ef", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurant Muduru", "RESTAURANT MUDURU", 1, null },
                    { 3, "8169586f-d099-412d-8e88-a943f596107e", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insan Kaynaklari Muduru", "INSAN KAYNAKLARI MUDURU", 1, null },
                    { 4, "b9eac596-9ad1-4c1d-910c-b4858cc8307c", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mutfak Sefi", "MUTFAK SEFI", 1, null },
                    { 5, "c86fe182-d431-4d10-8389-f9777a055f93", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bar Sefi", "BAR SEFI", 1, null },
                    { 6, "c7600831-4e89-4d6f-b932-9eff34271b27", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asci", "ASCI", 1, null },
                    { 7, "1de4d4cc-f0ac-4d87-8f91-acdfb9bb68e9", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barmen", "BARMEN", 1, null },
                    { 8, "3d9552ce-b6fe-448e-bd33-850b98cea814", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Garson", "GARSON", 1, null },
                    { 9, "966cb4e2-23d4-42e3-a98a-730ca6531418", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hizmet Personeli", "HIZMET PERSONELI", 1, null },
                    { 10, "8ee423c1-21f7-4c79-b436-aa8b72e6bceb", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Idari Personel", "IDARI PERSONEL", 1, null },
                    { 11, "53b4aee0-8bc0-48d0-927b-83e19a2fa986", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stajyer", "STAJYER", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletionDate", "Email", "EmailConfirmed", "InsertedDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "bcdb6f9b-b80f-42e4-a6a6-4f65fafa294f", null, "admin@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAIAAYagAAAAECJ5SesDY2Xxj7mZ75J/mxfrp4KjUEnEqS7mMO6LRAoqjI+SKaUxrQxM9l4pkGDORg==", null, false, "7c40ffcd-566e-4491-9820-853d3be94b56", 1, false, null, "admin" },
                    { 2, 0, "3a2647b9-4eea-4461-aa22-aadc8ef98724", null, "mudur@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUDUR", "AQAAAAIAAYagAAAAECAMCaegx8pIFASYiJfrkXXqTlozYnk+TCscFcq/HSYDfDgfu3lUrxfvwdHI8MKGVQ==", null, false, "dba78627-67b1-4399-9501-777838b3bf3b", 1, false, null, "mudur" },
                    { 3, 0, "e286a9e7-d3b1-439b-bbcc-2ce2f0a63521", null, "insankaynaklari@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "INSANKAYNAKLARI", "AQAAAAIAAYagAAAAENpNYCPjOsAvgFXpMummoxYQnuwdZkDyrmoiOEaAq3ciaoFz3wx64H8PZXvXxmzT0g==", null, false, "558bee26-656a-419c-97fc-67091152fc68", 1, false, null, "insankaynaklari" },
                    { 4, 0, "1456ec88-613c-40aa-ade2-e2b506d21b2d", null, "mutfaksef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUTFAKSEF", "AQAAAAIAAYagAAAAEJS/9YRSEJnrICxa6RO9d8XOr/sog3esGhufSfpRf4x9wAabTk+HLRYrMmQJDkTwHQ==", null, false, "f4b45897-0d64-42de-afff-886f9bdd8824", 1, false, null, "mutfaksef" },
                    { 5, 0, "9c6d4267-ce19-4223-95f5-67e713a4e15f", null, "barsef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARSEF", "AQAAAAIAAYagAAAAEAYD5ku6c8wZsAES0D5MUSsTCiYL8MRwoh2J5Qx3a/vHKsLdMVPwFrSnhNOOBEBF9A==", null, false, "80690021-4056-4874-83b7-eefd6b4bd9df", 1, false, null, "barsef" },
                    { 6, 0, "36ca9567-f90e-4fd9-bf5d-3cab9ff8baad", null, "asci@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ASCI", "AQAAAAIAAYagAAAAENLm9/MDf8cT9qRo3WZOGO0DgNDJgYxK1OSBI33/IR1V+i7HLyvhKgSgzjnxNsaf4g==", null, false, "7f07ebae-017a-4c1c-b1cb-2f3a06ebb315", 1, false, null, "asci" },
                    { 7, 0, "33bcafe4-55ef-425d-9906-2be6a4c0d3aa", null, "barmen@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARMEN", "AQAAAAIAAYagAAAAEMFwYW6RnXKpIw3rUSsWX04TA1zxM3B/t+CPPdENFdbYIfwKlGP5Ze//+kKhp2SEfw==", null, false, "208cfbcc-8a99-4b9c-8f60-9b60998c4444", 1, false, null, "barmen" },
                    { 8, 0, "62f01084-fada-470b-9670-0d2d66da870a", null, "garson@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "GARSON", "AQAAAAIAAYagAAAAEHVivvF3xzN+hn5cknr3OhLlzG3nSxIL460+Tv/4cKJ1z9ThPhVsKQf78D5+cXM23Q==", null, false, "8e809ee4-ad84-4640-97bc-831a41fd8637", 1, false, null, "garson" },
                    { 9, 0, "c4b0b3a6-3e45-4312-a6b0-c857fee9f4e0", null, "idaripersonel@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "IDARIPERSONEL", "AQAAAAIAAYagAAAAEKuit8wUO7f3I5RJAZV1tknPoxnBUg4CGDdRtXsh2dwOsFYkW1rldUlatOxjH89FFA==", null, false, "5952b351-e776-433c-bf1a-3f368f58e349", 1, false, null, "idaripersonel" },
                    { 10, 0, "4bcfb2e9-e30a-47d8-a985-30c50a349b73", null, "hizmetpersoneli@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "HIZMETPERSONELI", "AQAAAAIAAYagAAAAEEN3++6Bg8ksA9bN4U3t/YzoXAB+vigLSky9YFIe4+tolM9hZ/6/ldgUJdNd1W1A9g==", null, false, "fab04432-7073-4972-9ad0-2b4c87e09985", 1, false, null, "hizmetpersoneli" },
                    { 11, 0, "bd16f391-eb4c-49f2-883f-7ed4a8586958", null, "stajyer@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "STAJYER", "AQAAAAIAAYagAAAAEJyDP2ieLmgWH6BI+IddRaCsWTwVITuqZmPKCdTaKkRP66jciKWKBNe7rRAxhhIxSQ==", null, false, "afef6a9f-9800-4da3-ad11-f96bd0ca1925", 1, false, null, "stajyer" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "DeletionDate", "Description", "InsertedDate", "ParentCategoryId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Yiyecekler", null, "Ana Gıda Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 2, "İçecekler", null, "Ana İçecek Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 3, "Sarf Malzemeleri", null, "Ana Sarf Malzemeleri Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { 4, "Demirbaş", null, "Ana Demirbaş Kategorisi", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "DeletionDate", "EndDate", "InsertedDate", "IsActive", "MenuName", "StartDate", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "2026 Kış Menüsü", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, null, new DateTime(2026, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "2026 Yaz Menüsü", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null }
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
                columns: new[] { "RoleId", "UserId", "DeletionDate", "InsertedDate", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 2, 2, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 3, 3, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 4, 4, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 5, 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 6, 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 7, 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 8, 8, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 10, 9, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 9, 10, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null },
                    { 11, 11, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null }
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
                table: "Orders",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "OrderDate", "OrderState", "Status", "SupplierId", "TableId", "TotalPrice", "Type", "UpdatedDate", "WaiterId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 20, 30, 0, 0, DateTimeKind.Unspecified), 1, 1, null, 1, 250m, 0, null, 2 },
                    { 2, null, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, 2, 180m, 0, null, 3 },
                    { 3, null, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 21, 15, 0, 0, DateTimeKind.Unspecified), 1, 1, null, 3, 320m, 0, null, 2 },
                    { 4, null, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 18, 21, 30, 0, 0, DateTimeKind.Unspecified), 1, 1, null, 4, 95m, 0, null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CanBeProduced", "CategoryId", "DeletionDate", "InsertedDate", "IsSellable", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, true, 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Dana Bonfile", 1, 1, 250m, null },
                    { 2, true, 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Tavuk Izgara", 1, 1, 180m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletionDate", "InsertedDate", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 3, 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patates", 1, 2, 30m, null },
                    { 4, 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Domates", 1, 2, 25m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletionDate", "InsertedDate", "IsReadyMade", "IsSellable", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 5, 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Elma", 1, 2, 20m, null },
                    { 6, 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Muz", 1, 2, 22m, null },
                    { 7, 8, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Bira", 1, 3, 60m, null },
                    { 8, 8, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Şarap", 1, 3, 120m, null },
                    { 9, 9, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Coca Cola", 1, 3, 35m, null },
                    { 10, 9, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Soda", 1, 3, 20m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CanBeProduced", "CategoryId", "DeletionDate", "InsertedDate", "IsSellable", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 11, true, 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Izgara Köfte", 1, 1, 150m, null },
                    { 12, true, 5, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Antrikot", 1, 1, 200m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletionDate", "InsertedDate", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 13, 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soğan", 1, 2, 15m, null },
                    { 14, 6, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biber", 1, 2, 18m, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DeletionDate", "InsertedDate", "IsReadyMade", "IsSellable", "ProductName", "Status", "UnitId", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 15, 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Portakal", 1, 2, 25m, null },
                    { 16, 7, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Çilek", 1, 2, 40m, null },
                    { 17, 8, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Rakı", 1, 3, 180m, null },
                    { 18, 8, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Viski", 1, 3, 250m, null },
                    { 19, 9, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Ayran", 1, 3, 15m, null },
                    { 20, 9, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Gazoz", 1, 3, 18m, null }
                });

            migrationBuilder.InsertData(
                table: "MenuProducts",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "IsActive", "MenuId", "ProductId", "Status", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 101, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 1, 1, 0m, null },
                    { 102, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 2, 1, 0m, null },
                    { 103, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 9, 1, 0m, null },
                    { 104, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 7, 1, 0m, null },
                    { 105, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 19, 1, 0m, null }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "DeletionDate", "DetailState", "DiscountAmount", "DiscountRate", "InsertedDate", "OrderId", "ProductId", "Quantity", "Status", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, 1, 200m, null },
                    { 2, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, 1, 1, 50m, null },
                    { 3, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2, 1, 90m, null },
                    { 4, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 1, 1, 50m, null },
                    { 5, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11, 1, 1, 150m, null },
                    { 6, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 19, 2, 1, 15m, null },
                    { 7, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 20, 1, 1, 18m, null },
                    { 8, null, 0, 0m, 0m, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 15, 1, 1, 25m, null }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CategoryId", "DeletionDate", "Description", "InsertedDate", "Name", "ProductId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 201, 5, null, "Dana bonfile tabağı için temel reçete", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dana Bonfile Reçetesi", 1, 1, null },
                    { 202, 5, null, "Tavuk ızgara tabağı için temel reçete", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tavuk Izgara Reçetesi", 2, 1, null }
                });

            migrationBuilder.InsertData(
                table: "RecipeItems",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "ProductId", "Quantity", "RecipeId", "Status", "UnitId", "UpdatedDate" },
                values: new object[,]
                {
                    { 301, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1m, 201, 1, 1, null },
                    { 302, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 150m, 201, 1, 2, null },
                    { 303, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 50m, 201, 1, 2, null },
                    { 304, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1m, 202, 1, 1, null },
                    { 305, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 100m, 202, 1, 2, null },
                    { 306, null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 30m, 202, 1, 2, null }
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
                name: "IX_MenuProducts_MenuId",
                table: "MenuProducts",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuProducts_ProductId",
                table: "MenuProducts",
                column: "ProductId");

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
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

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
                name: "IX_StockTransActions_OrderDetailId",
                table: "StockTransActions",
                column: "OrderDetailId");

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
                name: "MenuProducts");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "StockTransActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
