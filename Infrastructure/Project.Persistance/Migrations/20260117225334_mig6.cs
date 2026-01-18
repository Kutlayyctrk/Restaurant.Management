using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DetailState",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DetailState",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "67169965-10b6-41c3-aff0-f5a9c8d3e9b1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "806bce71-c385-467a-9e9d-589707836c26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "9dac4772-c9ad-4fd2-b9dc-5ac7a2cf1c7e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "fe568ec6-1df7-4fa5-a09b-eb63bb00bdcf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "35cec23e-1828-4b0b-883e-67cffcae7a68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "3717e70b-e449-4d19-adac-6d838538064a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "d67c835c-2543-49e8-9d6a-3c07c2fd81d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "5e886418-26cd-4b6e-a923-2d9bb723ae1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "b879bb99-21b8-40e2-8247-f4a01d4f6d6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "e1b9e900-85eb-4dd8-adae-987e79cd4425");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "2a1f2aba-9143-4ff3-9ee0-d04fbe1c581b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "111e7228-e70f-4f63-a8b0-a1948372a2c1", "AQAAAAIAAYagAAAAELBfxu/yEPogpbp0jEWJQ9F64QC66NxJhbgrp6s4w8h5SdC7tF5YQpIG+Shh29kWzA==", "3b51ea7d-6de0-45e1-a8f7-43193995be58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73eb22c8-f1fb-4b57-a87d-9bc2a7f1416d", "AQAAAAIAAYagAAAAEGztd5MfipAWzgkSig+tQX7DEB1z8U4IIJQr/PpX5AA/244c17HWVCWyTg8Xq9Dn9g==", "62cc35c4-b383-4ae5-80bd-48b5f102ea86" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f646c0f-6383-4b9c-a8e8-14fd6ccadf69", "AQAAAAIAAYagAAAAEKwFy/sjvkOwIToXLIlxvp5cD5H4ohT63jJIJhD7s4EXsDu4EhBjuEVQqLfYS5VbXQ==", "37c48475-66fd-4dd0-a543-8e8127221319" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eaac9efd-6cf6-4b19-962d-906610bc8abe", "AQAAAAIAAYagAAAAEHEgNIWgr2QNf5emJqccTZeMT+YdnJ6GXQ1W9f1ngGCUFrgWiqilm9+zQcx4t37Opw==", "ce54f07d-066c-4a0f-9b80-aa3256bdbf24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93aaa1fa-f324-4c0b-b8cf-5c0ce45d9399", "AQAAAAIAAYagAAAAEA0imnPK3jofm3tekawrIfpLbjn1jSvvRmC/f1bE8qK3fqJyi+kp7PIJqhJO7Awffg==", "0f06c2ba-9703-4672-a401-a52d68bcddce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3440a016-81aa-433d-927f-0361bd283197", "AQAAAAIAAYagAAAAEDA7uKKacPqjK05JYP86nvNH/m/0sSwRvatdiukj8xJoVXht/Km62F4J0Xwi90s3xw==", "1d659317-b179-4f22-9347-ec22489757f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6a72c2c-567b-4875-be90-2c6bfe126286", "AQAAAAIAAYagAAAAEIgZ/Z+cp9pHjDqUX4ucIpj7pZrKkJtt8qgPulL68oG9DQ0EuKZBmK7qObTLFzC2qQ==", "6f19d687-4e5c-4ff1-8bdb-876b7e58a9eb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab20b9a2-aeb9-4528-8c06-5e4bc43bfeb5", "AQAAAAIAAYagAAAAELGgI6YmN6n8bUmFpSsQmXd+GDYPeL2GSb/FwenYEe8k/2FmSNl56aRqo3YBHlrR6g==", "63368967-4b8b-44ca-b44a-7cc3ab64e63b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac65cd6a-4f3a-41a0-bfd2-cc4354b9328c", "AQAAAAIAAYagAAAAEJ5LM4DjNMCYUA+dTjkxBtvZ8IfHxprJdqrgtruTYp8S4Ux5htu39LQRsiMnu+5JFg==", "c5dcfef5-acaf-45ca-a29f-38c2d798eed0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd44bafe-6a15-4004-a844-6904f979228a", "AQAAAAIAAYagAAAAEJHYEU/fNm8Mv4/6xkKZqOKmTv7gRcKLbjTQOVvGMvsZ3k9KkASuXZy78A/1IYh1KA==", "eef62f20-c68f-492f-801a-b1bd9e6aaa87" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb112ca0-7835-436b-8ab1-bd0f21ba69b9", "AQAAAAIAAYagAAAAEJjJjdVTXDeE3el9DD9gjAQTn+C+P54gZ6nEpI8JsY6dIh6oN4Dt6h2t987Sr3Gz9w==", "3999983f-2b8d-45be-a236-40e68a1bbe0c" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 44, 57, 798, DateTimeKind.Local).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 44, 57, 798, DateTimeKind.Local).AddTicks(4276));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 44, 57, 798, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 44, 57, 798, DateTimeKind.Local).AddTicks(4285));
        }
    }
}
