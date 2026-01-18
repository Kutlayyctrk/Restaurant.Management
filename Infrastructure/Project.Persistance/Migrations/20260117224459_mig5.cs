using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Orders");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2405d19a-c8ba-4afe-bffc-b6f21e4d6d1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "337fe67a-f3b8-49b7-8ef2-63449f8c5c04");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "1fd50d70-8cbf-4704-a991-ffb58b01c9bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e60068c9-44e1-4f9a-8e59-675f790784dd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "d2bd94ac-0649-4890-8ce9-7f58601b3f02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "ac5a4d27-2545-42de-bd3f-b58ab1b36978");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "a3fc7368-4ad0-4268-bb48-780230985d7b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "616d7b68-cc7a-43a2-967c-3d045cbc15ec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "7c0b5bb0-dd54-4cd7-b575-5604a2846ba1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "33e845b6-2136-4fad-81a9-9a736841cbdd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "b6456262-a756-42cf-9ce8-37d000ff833b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76c138d0-8a8e-4589-9a9c-32fadbd25325", "AQAAAAIAAYagAAAAEKOZa5/DgMYiZ7HCvR7E2agxBOx5oStOVeJj6xpbBtkA+mcpdHDap4NsksUwzqTQBA==", "c99a41b0-b51c-470d-811e-12b81802851b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c08941e4-7018-4ac4-87da-2e8ff50c8b41", "AQAAAAIAAYagAAAAEKWXWsu6Y9M2aHF00Pl48XC08CVhMTvm+s+IMEZxZl1Woy5Q+0iIihJZvpkiQJD8Sw==", "d9c2b551-5b5f-44df-8968-4130f27ba6ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca8c924b-7ee9-458f-a957-137394bf42f7", "AQAAAAIAAYagAAAAEElxU0wkAmsGqezLcvrT7YEN95XY7ETd/fzvhezco/73/jvKn2SnHaqz6oRxuSycTQ==", "45716852-5465-4401-9649-878a5a679b88" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8acbce04-b68c-4936-a8f5-5d77d83993bc", "AQAAAAIAAYagAAAAELP6HuZgHmGvE1ybPRwA54WQbnpLL9B+8E61rk6qSCMqRujtBddajR4ABYdA9Q5WHw==", "59d6d607-0414-4995-aff7-2f9057943706" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2342413c-48de-4b4c-9b65-4e8d18fe8f57", "AQAAAAIAAYagAAAAEEGcNAIXUj94TtsxrwpKBh+q4YWpfR4LxkcfaCcO0mZymhLM2vdQLERHTJotD7QRJA==", "93792459-098a-4323-88b7-f59e01a93933" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83aaefa2-68ca-406c-8522-199ebff42fef", "AQAAAAIAAYagAAAAEIc3Bo9Ximt5EL8wigoI4jtwOJEZICb2JaVa6nBKB9fS2eYzBzxraZSdNoEneN5RtA==", "931b075d-779f-4347-9bd1-1d8daf92738c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b851f36-fcb8-4569-87bd-5817a3c7911a", "AQAAAAIAAYagAAAAEPKwpUTFAhTe6rzZev33waeeDwEUHDUiAB8ZnNm96TaqimGscdv4Eo4Fm9dmsmWZmQ==", "296a116b-9e39-42fe-b1e2-3c5c198b653d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7457dfe-8c67-4fab-b8d5-a8158e9df0b3", "AQAAAAIAAYagAAAAEGc4ju783dQa5zyOhtgS8kirc+inTbiMRVXhEs4vY1AjFUzad1BhKPTwDzekP2aIBg==", "e152eeec-2ad8-44f3-a470-00f570df718f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b239af8-632d-45e5-b589-b586c6b274d7", "AQAAAAIAAYagAAAAEG08P7tK2q+YsR48aJRyE3M8cJm7EV++381+sVj/eoRCoHWW+O+l2tgGGR7oPNxr+w==", "26e6f0bc-0683-4752-880e-db2d82e68616" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "714273ed-29aa-42d5-b167-80931fec2e39", "AQAAAAIAAYagAAAAEM6y7U2Gd19jcqAuO05YuiMcAewQ+l3Uc7i7kLWdNgOpXnbWrB/xI5pjOARMH722yQ==", "bc7bd157-9eb2-4259-9acc-9e37d71fbabb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fafd851-7bf8-450a-9662-4bd3202b734e", "AQAAAAIAAYagAAAAENLij8TikQBNULA46VMr0t5rjYQDdI+/zX8sdt2zkydH6bXu1YzAwAgJoQ2B4JT0EQ==", "bbb14d8a-b126-4e0e-bcf7-fccd218227dc" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 14, 10, 213, DateTimeKind.Local).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 14, 10, 213, DateTimeKind.Local).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 14, 10, 213, DateTimeKind.Local).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 1, 14, 10, 213, DateTimeKind.Local).AddTicks(2888));
        }
    }
}
