using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d0ad15f8-d4b3-4e2a-bed5-ce8534611c1f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b56aa9b8-94f7-4e24-b2bb-45bc71dc5258");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4406ebb8-a023-4c0c-9df0-a0e3b08fc739");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f2da7a97-63e5-4cea-b9ef-eb59c7638e72");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "192143a9-50b1-429e-b401-e462a69200cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "38d7280b-0adc-4ccb-8c19-ea9270a17699");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "4866e7c4-7be2-418c-a1f9-acb261438cec");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "6e3a5856-4474-4395-b3ce-cbb3e7c3bbdd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "99b49382-29dd-4647-ba24-56ea66eae765");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "7bc7b526-fed1-4e0a-a9bc-ee392ad9a415");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "7a8b7fc5-8ec7-4020-9af3-6fea909d7b0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16455da2-4b6f-4e80-9519-ef752da1c24f", "AQAAAAIAAYagAAAAENLz30jLRxw5Ez9YXgjxPn8p6fVqRQSyIqa+no1NpoxNbyD5fCgxYL4DBOtqg85mtw==", "73975c54-421e-48bf-8237-f92eb335f12d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64765485-cd1e-4b37-bed2-4a953bf0bac9", "AQAAAAIAAYagAAAAEIEBh2ZPE7xezEG5jzgdJ3aR0acE6r/cnSdNu9JZfdQHHz2ikWpFKZUiHdElnCBicQ==", "72b4f59a-39d8-45a7-844b-b28845a3e1b1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65cca01c-05cd-4b07-ad6f-0cac29455143", "AQAAAAIAAYagAAAAEDJk0IDdgfwFwaatU4Zz9ETGXKtcWEFa24tFYvE6p6k+UA0GnN7KvkbANjQo1Js7jA==", "fb84f3ef-2bf3-4671-befe-21ea3e6a3814" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bf6c373-bab6-43b4-874a-66db6ee97ebe", "AQAAAAIAAYagAAAAEAHxi1FffMlxasVb+DCeO1DxhgoVfj0+O5X9DvLmcCeoP9i6PnZ9vZs7boSxvY4jeg==", "21357545-f653-4b13-abd0-e3ae38cf5897" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f485ed3a-8905-42fe-85c3-b7cf05c167ac", "AQAAAAIAAYagAAAAEBubs9HJ7HNwXxY+XuZhYLtOZiZNRTIPJ9IXmL8NyrIxswoDEgFE2NBQqx5N4BfYXg==", "09744f81-ecc8-4bcf-aff0-3e0368d4499f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b167b8a2-d6f7-4ac2-85cc-5c58a9332630", "AQAAAAIAAYagAAAAEFuMpHY0s7XUz9AXP4xEWY6RXswjoPS9gGi/mTy2fh9O4RGJnI4T3uY4N+7M/LzMdQ==", "f28d41ff-1e66-41ee-86ca-d1e27aacb4ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab225ef3-7c93-4334-a89c-faed544a3b8b", "AQAAAAIAAYagAAAAELyyiPkNJv4pnvQl9MF0nCy9z5lUMQJC6UqFk1eQVYFSVb715/+uTCFT5FbVlXVLZQ==", "65b0aa3b-b7c2-4743-8197-1c86363ce846" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c366a4a-d42b-41f8-a6f2-a299c1e36578", "AQAAAAIAAYagAAAAEESzNAdRwmhn6eELT9/kL4DDdQ7XNAySaXXatKGqeJHQc/RDBMsEO2fVxvqNON69dA==", "54b8852c-c8ff-4149-9949-95f5b314e9a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6c3a879-5108-452b-9d4f-0afa43cf5bff", "AQAAAAIAAYagAAAAEAhirCKQKb7Siyo9jvgcgnfRE8YYNYUiaRJFFFg3ueZ7dyfK0OvhVGyCknPhEQ0OGA==", "b349ecaf-9594-4f84-b838-8df6dce2ce2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88089605-1f41-4e86-8cec-924ac61971fa", "AQAAAAIAAYagAAAAEPI4AzZWUDnqocskvLVq93CrjGNGB6bRIhO8qsHV+dHO8FSEs9Dr6A+l+bIKP+eepg==", "55602628-1d27-4edf-8d94-514f7ac86ba9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c3bdf339-1dab-45ff-96a1-4d88769c486e", "AQAAAAIAAYagAAAAEHvG9Obv1fR334ee0Gau0KZBnEbYdv4LtbvdSSjRuVpgiTyYZtlPvMeDFUZsN7HoXQ==", "16c6a294-3e6f-4b7f-baa4-9fb534c188b3" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 17, 15, 53, 44, 481, DateTimeKind.Local).AddTicks(2915));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 17, 15, 53, 44, 481, DateTimeKind.Local).AddTicks(2969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 17, 15, 53, 44, 481, DateTimeKind.Local).AddTicks(2976));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 17, 15, 53, 44, 481, DateTimeKind.Local).AddTicks(2983));
        }
    }
}
