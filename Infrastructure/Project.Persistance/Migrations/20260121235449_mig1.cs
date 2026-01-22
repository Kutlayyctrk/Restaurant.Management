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
                    DetailState = table.Column<int>(type: "int", nullable: false),
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
                    { 1, "05a703d0-13d6-4305-9434-84fd11fbe9fb", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN", 1, null },
                    { 2, "8e1d8b06-e6f9-4bcc-8cd9-bf00e54e0309", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurant Muduru", "RESTAURANT MUDURU", 1, null },
                    { 3, "91d1bb31-71f9-4861-86d2-314558654c4a", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insan Kaynaklari Muduru", "INSAN KAYNAKLARI MUDURU", 1, null },
                    { 4, "a428420e-bcfd-42e0-ad7a-f86c111853a6", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mutfak Sefi", "MUTFAK SEFI", 1, null },
                    { 5, "31ef340a-df6f-49f0-91ab-fda8ba02abc7", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bar Sefi", "BAR SEFI", 1, null },
                    { 6, "d70015a9-761e-42b8-a7d6-1e96137c1e4e", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Asci", "ASCI", 1, null },
                    { 7, "e676b4b5-c050-4678-8756-2f68c1943df9", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barmen", "BARMEN", 1, null },
                    { 8, "c2efae5d-40b6-49ca-be3c-50c0c18570be", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Garson", "GARSON", 1, null },
                    { 9, "5b4908f1-ffaf-4db9-83fc-7315f4dfd9b3", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hizmet Personeli", "HIZMET PERSONELI", 1, null },
                    { 10, "19dd3a0b-d714-4642-adaf-834a92780489", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Idari Personel", "IDARI PERSONEL", 1, null },
                    { 11, "4605a6e8-d6f2-4899-b5bf-256f1fe64b31", null, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stajyer", "STAJYER", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DeletionDate", "Email", "EmailConfirmed", "InsertedDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "9e031628-206e-44d4-9e72-4b6847c67dfc", null, "admin@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEDIP59URS/9aaXrZUM5XN8cSfiwSU/0VbFbGeRI3xXYR2gR75T3KP/gJG9MPpKN7YQ==", null, false, "8c91cf72-35f3-4b29-bf6e-c68f3da8bef9", 1, false, null, "admin" },
                    { 2, 0, "64c28294-6904-410e-82ff-64266517795b", null, "mudur@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUDUR", "AQAAAAIAAYagAAAAELw+2crW+K2eFO2tSmmmpHbDghYgfmaQggVVmvxIZUYU9A8J/Ci279vbktiOpOC1xA==", null, false, "060dfcd1-a770-4aca-af2e-98f1dc6eba8c", 1, false, null, "mudur" },
                    { 3, 0, "e72f0e42-a3f3-4d19-ba2f-242811759383", null, "insankaynaklari@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "INSANKAYNAKLARI", "AQAAAAIAAYagAAAAEMxW/+pHVPabMHRLoYvk2Q0iF7ayyKF434f/WMlPfBHB41mym33ui5wja3Y8KvlvJQ==", null, false, "2f88d724-2feb-49a2-96fc-0cb6aaab2a5b", 1, false, null, "insankaynaklari" },
                    { 4, 0, "14747202-bb36-4e95-aad0-37f84cf750bf", null, "mutfaksef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "MUTFAKSEF", "AQAAAAIAAYagAAAAEPe+7jjSgSPqvDpK+ulhCN3mpXe4XkZgeGA8lbvLd4FWWnI6H14o4zxb7a+5jhebkA==", null, false, "ef282c99-1c30-4446-8880-6ee822cd50e6", 1, false, null, "mutfaksef" },
                    { 5, 0, "293cb4b6-e5fc-4f11-893c-fa1bdd2045c1", null, "barsef@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARSEF", "AQAAAAIAAYagAAAAEGMm9xhwMxBBWPKqoJypnO5/Xy1rON1eDunDUvsDxeKhDFibnDs2QW1EMZ2fx5tLlA==", null, false, "cf219803-e827-4ab4-9616-fdb584211a94", 1, false, null, "barsef" },
                    { 6, 0, "48f4a386-1b6c-476c-b73c-7f863582fb28", null, "asci@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "ASCI", "AQAAAAIAAYagAAAAEHul4qyYqB8yTAzeBSKjRC9S0gemaSYlR4SMzOQn1FpjySGt61W8Spevp9rD66QB0g==", null, false, "29e93dee-a6af-46ff-a35c-27783eaf41aa", 1, false, null, "asci" },
                    { 7, 0, "b1029507-bd9d-4fc1-9257-e7ce3ced69ef", null, "barmen@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "BARMEN", "AQAAAAIAAYagAAAAEFaY6ml9OEPKWPwOydgdp0FlN1qydDQ0Bo0X/M63NCc7J4Ax9sRKEH/Zqp1hTKMiBw==", null, false, "36acd771-ce08-45e1-b913-0f0aa1a71de7", 1, false, null, "barmen" },
                    { 8, 0, "5140c03a-fffc-47f5-87ed-079c6e2b94e8", null, "garson@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "GARSON", "AQAAAAIAAYagAAAAED2SGltTkEhrqlkbP+Wen8fx7+/jCrSLJwdD4trP8Q/iACzGS894bf6kvVgWZKgrMg==", null, false, "77c2452d-b10a-444c-9f69-e090a957cf08", 1, false, null, "garson" },
                    { 9, 0, "76162319-5644-443f-8650-40b3bc401a07", null, "idaripersonel@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "IDARIPERSONEL", "AQAAAAIAAYagAAAAEBGwCDEPcJDKp+hTZzFseSILSfZOzUKlMKZGNn/zcDSRISk3kjIxuf0fzvtGRZznXQ==", null, false, "380effba-4be1-46df-bf44-db8b0bbc5881", 1, false, null, "idaripersonel" },
                    { 10, 0, "66e10fa6-fdac-4458-8448-c8ba34579f0a", null, "hizmetpersoneli@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "HIZMETPERSONELI", "AQAAAAIAAYagAAAAEFesxaH1rjVIVb+LHrv1SAnl/RklwjJFFs3Mb/VCeQVPeSFU0ykCeIwBLBfByRIXAQ==", null, false, "00967073-c23b-405a-a900-7746dc0f8983", 1, false, null, "hizmetpersoneli" },
                    { 11, 0, "48a1f04c-cf79-4dac-bd50-4bdc1c4eed73", null, "stajyer@restaurantmanagement.com", true, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, "STAJYER", "AQAAAAIAAYagAAAAEACSmyq/UZxW2tvJvwiT2T6u048C+OIamO6d3Y+P+pUqytr+rs7KyQAtJHQW96rZBA==", null, false, "87feb4e4-18dd-466b-ada1-c69373cbfb11", 1, false, null, "stajyer" }
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
                columns: new[] { "Id", "DeletionDate", "DetailState", "InsertedDate", "OrderId", "ProductId", "Quantity", "Status", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 1, 1, 200m, null },
                    { 2, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9, 1, 1, 50m, null },
                    { 3, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 2, 1, 90m, null },
                    { 4, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7, 1, 1, 50m, null },
                    { 5, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 11, 1, 1, 150m, null },
                    { 6, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 19, 2, 1, 15m, null },
                    { 7, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 20, 1, 1, 18m, null },
                    { 8, null, 0, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 15, 1, 1, 25m, null }
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
                name: "MenuProducts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RecipeItems");

            migrationBuilder.DropTable(
                name: "StockTransActions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Menus");

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
