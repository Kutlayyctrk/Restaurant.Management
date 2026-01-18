using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "009b545f-2949-4363-a35d-9912085ca90a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0d5baf7f-300c-4e60-8c80-f23bd780c360");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "bc14da42-756e-4ad5-8a2a-55aec65490ca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9bec68cc-5011-4ba2-b9eb-c0707e652d0f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "dc2337c0-30b7-480e-a937-115520b668c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "836789f4-26c2-48bd-8234-8bfab2a5eb85");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "8bccc630-b953-4088-acaa-df8c44f1ce97");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "1e9f68ef-8d46-4836-9e29-04d93d27b61e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "6f73f540-a354-4d60-865f-04ba78e02502");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "7889497a-04b4-4268-8465-f1d78ba103cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "3a9e7a54-6aae-4aca-b394-89f99e26bf27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dccfcc3-670d-4d16-99b3-7d17f556661d", "AQAAAAIAAYagAAAAEJC6Ps4MswatpTMCTWlVoUDAtk/sD6gEH7kYlvbug0CDPKlAba0qSxiD9sLHs1wCQg==", "b2423b67-2a09-4f88-8081-4c36a5193999" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bdc5af1-e289-4bf7-a09e-21e15b900409", "AQAAAAIAAYagAAAAENEUOqu7vQ5fnfvK73kPNbnGSMySxr7gioJcsGwLJI07Kq+XxhCb823ws0hDLHExSA==", "f76cb900-ea47-4f05-8e9e-a9a23ddd9229" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "13c82d55-9049-4227-80bd-85999cf92bf5", "AQAAAAIAAYagAAAAECr0lYyvqpoT7Ue35YleasNRxAN6971SAmcZWpaSooyi6/ZArq/lAzn7iIjfErmVIA==", "6bbd468c-2a68-4811-bbec-50deca7088a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e108bb23-2e2a-4dad-aa28-8880177769bf", "AQAAAAIAAYagAAAAEEOlWLqCbVBZrdWPFCsHoSGNpaN8Zt+hSXLu5p9jiGi34EAKLVO8D1ZMxdwfl0adZQ==", "0431a7cb-b5cf-475c-8db1-6f6ea98e3fb0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "757442d7-e873-440f-8505-8f1bb5de7ebf", "AQAAAAIAAYagAAAAEFebZFk8j1IBv79KtJbHvVTTFrqllqSRph+napxjKEGDixnPlYQSE8CX6v8VcMGQpA==", "4292016e-cb3c-47b0-bc0c-d5bc86181310" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "edd5a991-4516-45a1-b214-740c4548c67e", "AQAAAAIAAYagAAAAEHUl//21CAXuQZeHJywrRBUzU/GSgUMP058nu+hkPdVEh5Z7DJHywUVNmOocRyA8EQ==", "ebfdf0fc-69b9-4e2c-878c-47606b457e2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e73074d-5aa4-44fb-a4ab-c12035d5e7f9", "AQAAAAIAAYagAAAAEHBxxalOgJ9Mqi+2Ha0XgjyCBxSyL8zLJ0OIIzLgBdUwt/Q81Tif+hKPfna+V2iZxg==", "b9e25b5d-e946-4131-ae2e-3e71e8a5ecbc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95270bd7-4273-444e-b43c-51e30fc68009", "AQAAAAIAAYagAAAAEDNsF5tnA96Ue/2unEAAguCtoE0fym+0Jwb9kyZ1TFHjVnMPQAUzL8kfmhfB8Rwp9A==", "1850f65c-7094-420a-ad9f-6d5d3c37b6ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3c96498-7ddf-4713-8faf-238d4130d716", "AQAAAAIAAYagAAAAECUdYPNW25pFZRiyidlt/x5IP1OcAU3gfFsmDfD7C2UE8yHtyyfiXMqYG8T1mM6eIA==", "a80cfda5-7f6a-4dc6-bc62-3f5030f4d89d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5b624458-4a35-4dac-913b-6b40e35f203a", "AQAAAAIAAYagAAAAECBxqKKg8OYIt4bj12KBmEskiK9MwlxxEeBzzJL2OaM5IkeGYFd1nAPd2xwiBifgRA==", "6e01c387-74a9-47ea-8b90-ad4056cb48dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e701b95-c866-4954-a48e-0cd7bfd7e8ad", "AQAAAAIAAYagAAAAEA8eC3s0OdMMzrX9oYNYk8VD1A9ZKYsfHls2sqjwKqk5MlspnyYHI6UGjieBWbMHwA==", "88261a93-d3f7-4f74-b4a8-d388e3324842" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "DeletionDate", "InsertedDate", "OrderDate", "OrderState", "Status", "TableId", "TotalPrice", "UpdatedDate", "WaiterId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(9020), new DateTime(2026, 1, 18, 20, 30, 0, 0, DateTimeKind.Unspecified), 0, 1, 1, 250m, null, 2 },
                    { 2, null, new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(9037), new DateTime(2026, 1, 18, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 2, 180m, null, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8257));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8293));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8297));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8300));

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "DeletionDate", "DetailState", "InsertedDate", "OrderId", "ProductId", "Quantity", "Status", "UnitPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, 0, new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8749), 1, 5, 1, 1, 200m, null },
                    { 2, null, 0, new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8760), 1, 8, 1, 1, 50m, null },
                    { 3, null, 1, new DateTime(2026, 1, 18, 2, 44, 33, 148, DateTimeKind.Local).AddTicks(8763), 2, 6, 2, 1, 90m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6b4ac800-96e8-4ef9-b8e6-92f3b80055e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5076289a-b01d-45c2-af89-483512a53f32");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "76331928-c040-4e1b-9e61-d4fa71c25391");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "651e79d5-d503-4041-bb78-970ad34d19d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "515b54d2-72bf-42a9-96d6-0f98c0f8fce9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "cafd1ad0-ef6a-4a48-a8f8-3013b57309fb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "b5910f26-0af4-49e4-ae53-59c78a28b15b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "ac046732-f54a-4b1f-870b-ee05f0a82db2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "9023dd71-cef8-4973-a54b-ab6d573436ce");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "d61232aa-5577-42bd-80bf-5205bcdde06a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "b2a3f5cc-c619-478a-9aca-17b79a22fc82");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8dcb69c-bd39-4537-99a1-63e42a7081bc", "AQAAAAIAAYagAAAAED0r2KbybFIoB8TK7loWrMMODGAexJkgqA34PVcUd1ALLNfxTC9/5xZnbZzDqCIkBQ==", "fca2840f-f2dc-4806-a1ae-dd3a5ba9e128" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7209de5-09bb-457d-a891-3c7bab82a39f", "AQAAAAIAAYagAAAAENN8nS85Re9kdSiO013NzzySBEoU6IPD1OtgblhEETzyQ3ZPRRwMRPmvBAywzppdRA==", "c2db1c2c-e530-486c-998f-382cb2bff8b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0acf7404-47b7-4491-af14-6f323d6004d7", "AQAAAAIAAYagAAAAEPpcnkRmLjN4v7wq7gKhqQpE3IGiroInLYIGs8VLrDbsZiTBsLS8MJnhDCijiXC5/A==", "d61d74bd-0389-4522-a213-78544bae2bd2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c192272-b02f-4901-9a85-6dec1e8271ee", "AQAAAAIAAYagAAAAEOC19tYuqcQdYxSBVVeqUR8J+tF7fRM/io2c3NLaF/yFQrCgszI0+aP3sKgZAzzEpQ==", "84e24dda-4060-4210-a5f3-65563bb2bcef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e7f8d33-d701-4cef-ac72-be71efec22b5", "AQAAAAIAAYagAAAAEPZqAWNmfHplW6MjhzRVVvJPn/klg+XNQlvVdIyPkLKXi5sTCQ9jrA8Q8Qi6FP2xSw==", "9d83cb7d-e78d-476d-933d-f717fbfb6e15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "664dcc67-7cbf-4b10-895b-24b25f0ba0eb", "AQAAAAIAAYagAAAAECrLslb7biSZirRXZtp1Qx9JTV8/HtfOaoBjeXx0Lhwga89YYdCTEgupcIcJpSLfjQ==", "01197da1-c24b-431b-9d4e-a2385e420139" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c3ba937-ed65-4032-9025-734a6fb6a9e4", "AQAAAAIAAYagAAAAEATAmXASIlAYirzvXkC/H176DLfSSUdQ0sGBq3wXVT0YnLF30Y/AZ6nU5eHyqyIaKQ==", "47f8411d-ed3b-46e2-909b-c444ea3f46b8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d143b5c1-67c5-4c50-a358-5e81cd6236d4", "AQAAAAIAAYagAAAAEDDN9Jh8rhzDKxWomdNcYFfYgjI/GqBugc463v21BwlUVynOQzirQbx47QJSbCvByg==", "03c1f9b0-715c-40ba-ab89-6fd829d86205" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6938aceb-4d0b-423b-9d53-046c3ab8e6cf", "AQAAAAIAAYagAAAAEAlNhabdXoHcxHaW+Ka9me7SKvrRTmjcM19ZKmlYnSJfFKnweHotgPdq7kY4MghMfg==", "96085513-4409-45fd-999b-224cb4ec7522" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a823a021-7987-4de2-9db4-d9181b94cd90", "AQAAAAIAAYagAAAAEPCY0C4MivzvCGlv10cigegDGFay4+ZQWHe5wDRb0SnN/2jjKX+vbJAaA94FxvuRzw==", "cad98ece-2714-4bbe-a977-0d98e30914e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f6a65f2e-8124-4bdd-be33-cc71309648af", "AQAAAAIAAYagAAAAEPc7IpfLdJPBJPfu8WiZbdeidzh9qw40KW3hBcfETB+7XK6RahUqG9gZffdRmxOn7A==", "3c3208b5-d208-4ea6-8a2d-5f619b775405" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 53, 31, 831, DateTimeKind.Local).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 53, 31, 831, DateTimeKind.Local).AddTicks(4194));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 53, 31, 831, DateTimeKind.Local).AddTicks(4198));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 53, 31, 831, DateTimeKind.Local).AddTicks(4202));
        }
    }
}
