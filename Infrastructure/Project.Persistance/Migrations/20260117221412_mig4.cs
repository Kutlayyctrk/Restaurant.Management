using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c1dd1623-f34a-42ac-9325-c72593b4a679");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "53ea3ddb-098f-4b93-a6ba-3912b8edb101");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "06cbaf54-57dc-4510-8e97-d7590b0c5a79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2d1e55f5-9509-4bd3-b6f5-b6abe2961f5d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "983818d5-2fc2-4f67-aacb-48cb8f027916");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "5b6b31af-56cc-474a-b94f-505f60c3e7f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "04ef2478-8069-461f-8721-d31f6a753e42");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "98322421-84e7-4d4c-b32d-f84f25734a89");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "c94dd56b-c891-4160-b588-4e5de97514e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "fec6658e-80b6-46c3-a011-793670d6b788");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "917bfc55-8c82-4653-b6c2-ec98eea95483");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d18eb65-3790-44d5-b877-d32ba9e52b70", "AQAAAAIAAYagAAAAEDTciK/ZYo8rcdQ5uwNdqpdRXnvVM2KrbBl9mux/+KhkokUDi4K26xvhZqbpJbmJBQ==", "0189adbc-ced8-4a29-a1a5-b43459375998" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e0cc69f-65fd-44d8-9d79-2ff7407702cb", "AQAAAAIAAYagAAAAEHx6aZnoqEeHIKYQn8SX3cpqko3ew9Qx5o+hUXXC9ja9hSQTem4fQ7EcV3d06ickuw==", "072e872f-facf-4878-a858-40d020f30537" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e71780a-af60-4f21-8636-769fe9f91a4f", "AQAAAAIAAYagAAAAEB1N+zcbIcmm3iVtgCPZ1xlODARiiPTKkCWXVBvqGvxyjhvNV0RfoMRbb2HhiteJSA==", "08a200a0-08db-473c-9afa-e6b353cf78ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18feb73e-1e56-4194-a20b-4e4895c4894b", "AQAAAAIAAYagAAAAEEIK3xqsI3S0VhA2/9o28PphF8Xj++F4s3aSaFVLqKp5PX3yrRlLsQ/N2OiEomeESQ==", "0c83b72e-accc-4770-861d-623e77f033d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79d2cb6c-935b-41fb-a3b1-4e492f6ac2ad", "AQAAAAIAAYagAAAAEOY1Pao9Kz2etHpCElGUTda8Jx7L2aiaa168rhBRveCzychv4rqowyCKwrQa0Dl5AQ==", "f904b249-df24-4528-9a22-c8cd72969af5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "016f33c7-baf7-4add-b32b-80dc1661131b", "AQAAAAIAAYagAAAAEKrlH1KRcXDFvF9og8Yv4sT/Gb+8l9YRwvSykQtu0UkP27gren1NT85Ua3x2a9d56Q==", "bf1e2ad0-ce8b-4682-8e72-899cd4a5d5bd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "109fa283-748f-4699-bd21-18c9e15ef41f", "AQAAAAIAAYagAAAAEOAHdhYjCYVGe/5KF3aD6t30tHKYhsYxpyfX8wo8TqR8IeIh9VpYnC5xNdupTRSyyQ==", "0229532e-0cf4-413c-9cee-58dd8439e94c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32bc84bd-c99b-4519-8091-e791f5a11038", "AQAAAAIAAYagAAAAEPll8gPYherMcjHW8ZOSeFd1AK+bO//21kXq2AoQujfghFkhEY0c5GBoJVr82Maixw==", "8ec9c96b-2061-4094-996e-da5572776fb5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab9eb70c-e6f8-4799-9181-18c20cb732b6", "AQAAAAIAAYagAAAAENPWfRh/XjdY160qP5aKm/moCjj4S8nLRougsxI3dqWWKxXASGpQhDnlNxylLfVaTg==", "7455ddeb-9fc0-44a5-9432-8423bd2be355" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "851d61af-9094-473d-a65a-2bfb17a32830", "AQAAAAIAAYagAAAAEIpV4wufz8CD2MRd24zWd5JiFmabmB9EEVpc9PvQd3m0dgV2wWB2pjzjdTGoJfcMQw==", "6dc6a351-927f-44ad-b47f-2425cb7f323c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdb57342-76cc-424b-8bcf-bae358c19923", "AQAAAAIAAYagAAAAENKsxouk284UyjC4ChjBQCYOPOxPzyFspjtFJbpP/HKmsNKQp/YBlqpaP52pftqMTA==", "68aae3df-0fca-4613-95f6-2e14e0b41ca7" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 0, 30, 24, 1, DateTimeKind.Local).AddTicks(9444));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 0, 30, 24, 1, DateTimeKind.Local).AddTicks(9476));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 0, 30, 24, 1, DateTimeKind.Local).AddTicks(9480));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 0, 30, 24, 1, DateTimeKind.Local).AddTicks(9492));
        }
    }
}
