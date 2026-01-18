using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "0782eeb4-160e-4ffb-b61e-4c8a57025d88");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "869a61d9-cb47-45df-95ef-f90a8cf7ef6a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "287913cd-434c-4bce-a7da-3f84d000e46a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "690fdafd-6438-4e43-b2ff-1804ad47182b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "e608f4cf-0d29-48b0-93da-741042aaaf3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "4e6022b0-4d51-420c-983f-dc585926ac14");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "a56d7ced-6142-4b20-bd1c-662deb7d7401");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "025aee1a-fa7a-421a-84f5-ed5693f0f09c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "5e86de9b-ffd0-4628-8dc0-1693d696250d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "38f15083-812b-479e-ae4b-e9a6773cc610");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "6453a284-db6e-4b6d-ad61-a9c2ec7a48ee");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb1ca947-3d65-4d12-b7d8-947bee5b75cb", "AQAAAAIAAYagAAAAEHld23ZX0vGkexjXF3JYQCvRM/prCOCSxpRtnmRo/XieMXQzwjuvBGtJST8nqYdpnA==", "a50b270d-d972-4f8a-bd39-2f4818fccfa5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70c294b8-573b-47f0-8571-8d8414c2c5ca", "AQAAAAIAAYagAAAAEIxpo763HwETaZl3B+YSJNoonROxV00Kl6nqw39LWy1Q8jgrQJLBMwvVvQT4bOC9vQ==", "4d3f20eb-ab37-48b2-afee-5bad02c1f6ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78c6d9e2-e606-42cb-a896-a799cc1488ef", "AQAAAAIAAYagAAAAEJvUPE0LRpRxuJdNZlOjtw0hmEDMKCXB3fzHDTG2U2K0uCCpTRoCge6ha1D6oJNzmA==", "934a8772-bfea-4c8e-b39e-ae726e381254" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "087caecd-2314-4c1f-8d6e-55d0f48beb87", "AQAAAAIAAYagAAAAELq+SKbJpD3fFxFxExKy57GcapsMP1atM8CYHTnXAwK2XEWaDjW+b0OQdcX2HOn2FA==", "3373e86c-3da1-483f-9e6f-468e9edd56be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb3ff81b-0cd2-40d0-b8dc-e309bbdbe3a9", "AQAAAAIAAYagAAAAEP0+Farg4yNkqqHv5sabzIoAJJg3UNNwM9k3OvrRg2JTnMBo7g9WMn7MWKYAwQY7Yw==", "a8206fb5-8c52-4f07-8c82-d7a3afd0e8d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6e72d8d-06e5-4b1a-a24a-6c6f79164e5e", "AQAAAAIAAYagAAAAEC3raOnWACKeL1nQdiiwgXAFLEyjR/VNxKwdVCIjnOlDp+Y5IpKeeBJatHBR+aYGXw==", "1f6745ed-24ec-41a6-88ce-9730a3941620" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6017c82-a41b-439e-b8c6-f78463c1851c", "AQAAAAIAAYagAAAAEE4gf6Vn30qU8yS2D6hiZVb8tU69TQOQLBBPMk6S15C2REgswYvfmzRmJacEXbYdBg==", "dd73ea47-be16-4c1b-9248-8c72a480a19a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31855f43-7078-466f-852b-d87615ff5dfd", "AQAAAAIAAYagAAAAEGvX6yUrHGVkj5fw49XUMlFpzQwxYiXMKzwUXGx/J86pMASenID31lZKiasfZAd7og==", "312a6da2-2ce0-4f2d-b2a0-a2466e5142f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37bd86ef-1fed-435c-a058-3bf3e9810518", "AQAAAAIAAYagAAAAEHQ5RKjrjvLzR3b0F7f3bJF0+UvtoDBduaWIHUUpo9OeojSP1aG04OwBTQdVqjvn2A==", "2f757565-efef-44f4-8b24-0eed39f0fb8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5057980-2237-46c9-8083-22222935e77f", "AQAAAAIAAYagAAAAEHJgcMyC/yLb7fOzuFZP4wW0++/HcpudcILQ5HR9uulHClY1J7MbcSs1/PunPShQmg==", "97b92b98-fe37-423b-9f23-993240f5523d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a52a111b-2821-47a7-9258-b714d08fee17", "AQAAAAIAAYagAAAAEG0p3Z/ATM+RLzoD+2M3+Oven4zO25YyQ2T2kyRyTeft1Tc7P4YjUT49MYC9PEHsNg==", "95466361-2038-4412-8b1a-6757fe69c88e" });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1867));

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1878));

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1883));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(2004));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1203));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1267));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1272));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 45, 37, 703, DateTimeKind.Local).AddTicks(1277));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8c724e3a-31bd-49d2-9129-d33d55d73e41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "742fc1d0-5eab-4b7a-831e-671556c3a285");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3f600fd7-9d98-454f-b0fa-d161e99aa92c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "8cb9af67-f1b9-4c6e-8d5e-381a7a5bc3d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "70d4c9fd-7643-42d3-bb3b-b3c4613fd17e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "9242cfd4-526f-4abb-b4e0-cc6f53f00111");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "15f6bcee-0bac-4175-a07a-3788d043693a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "ConcurrencyStamp",
                value: "aa1d6755-9f6f-4b73-97fd-a3d62f650ff0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "ConcurrencyStamp",
                value: "c0ca6d40-b8fb-4ecd-8c29-450fdb98ef8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 10,
                column: "ConcurrencyStamp",
                value: "9bdbfc68-b568-4ad7-9692-32989a515053");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 11,
                column: "ConcurrencyStamp",
                value: "43b3c159-3b26-4edd-a825-75e1c43138d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9cd6190a-1a41-4893-be38-64b7369badea", "AQAAAAIAAYagAAAAEHs9VxxYnOg4/YOpjynRVqhfQnkVqUu6ydPzTR8KnJNSoYRdwQwe9MS9FXV8H+C/EQ==", "64608572-e75c-4bbb-b173-80575941390e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "389498d1-3aec-4902-ab6d-213e7a16542d", "AQAAAAIAAYagAAAAEJEMtpTSzS/lMf5bDaO7IOBArWddch8Upw73nTnrwTxTsPZvnKNWgm0j03y2QKdjGw==", "daa6635a-a71d-4f5f-b7bc-245eaae2ba66" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae997665-f0f8-4cae-b616-1875d93880bc", "AQAAAAIAAYagAAAAEBfTX1LPu2OWvZscxIhg8tUiQeDLIOT5CGkamdbkEQDMC8JSGCQzXs+bbSRV+kmtng==", "65134d5a-6f7e-47d0-ab60-c6ec9783c075" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40fdbdae-a61b-40ed-946b-9bc94c0ea513", "AQAAAAIAAYagAAAAEOaTrq9GOmtuOlFu/XV8PtwGfp+FijaOsTEgf8QN1xWqt9W+sdYMFxvG1LklKSaLuA==", "e685beb0-eb35-40b6-8340-f66683255b0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb6a8ee9-a92d-479d-9224-ee4485fdd9bc", "AQAAAAIAAYagAAAAEHacbedJSn0mY8Z9FLgA7z7yerGVmpREVEGnilBgkDvwv/LZA80+RtVjlB95CyPg1w==", "af0478be-5629-464f-b8db-ae522b69d9c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cabc86b6-2980-493e-810b-16bdad1a207d", "AQAAAAIAAYagAAAAENz5KatM+xo5mx2admf03Op1iG5G07CLbWOGjV2oo1IUtlhRd0H2v6Di19NoMWScVg==", "fe2cf5aa-3c56-4213-b0cc-85f93bc2c69b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e80ae542-d821-49be-b967-3882ad8b2663", "AQAAAAIAAYagAAAAEKL0OQWqiRjs2XSG3Wcg2onxxEHAZ35Pr0XKSjaVbt1lG2MRMb8GRgZtzYekTSGYfg==", "9bd67477-028a-44f6-980c-5c324052bf3a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "601561ea-bbee-4848-848a-30071490e6b1", "AQAAAAIAAYagAAAAEBOKXjr+rybZ3iTcPQpdA4T6uhp7ee+fZJNYYuCmpngFgosNjsvaxvRBMwRNEBf4CQ==", "a3dddddf-ff14-4d28-8b47-2d7a56008f52" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b786248e-f3d6-4783-8687-e61eeed1f0cf", "AQAAAAIAAYagAAAAEFK8RUZOC6zUfIZB7K6Ark5JHR0A7d8FP6TG31Biy5YqPX8XoNiLVyfDsgs1xYmgxg==", "9b758f3e-6eef-4638-8e77-8a5523e00426" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a443ea70-72ff-41db-a04b-23cac4e3fe3f", "AQAAAAIAAYagAAAAEDvsNidRusDZA49Flt41QixfLqH3WdTa7HYnmtX4P5Gma3HQlg7xtjuYQY7W72k/aQ==", "8623a686-a307-4ea5-97b4-0fc253b2b11b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6dc0370f-6dd4-4787-9839-b58d872b8edc", "AQAAAAIAAYagAAAAEDkaQ/MwRqnyw7XyRWfY3i4ppT7klaLznXOu9JDiOo2pbaT1XVDcTCZLII7YPppfBg==", "468a51a9-93b3-452c-bfeb-6c1b515994c6" });

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(8077));

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "OrderDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(8096));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(8183));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(8192));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(7532));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(7566));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "InsertedDate",
                value: new DateTime(2026, 1, 18, 15, 24, 58, 654, DateTimeKind.Local).AddTicks(7575));
        }
    }
}
