using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMockUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AlternativeEmail", "AlternativePhoneNumber", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Notes", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShowMyContact", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "fe4d4664-ce1c-407f-85e3-3815555bb146", 0, null, null, null, "af743441-f0b1-411b-9c1d-4ccbc9ff89af", null, null, false, "Atalanta", "Bassham", false, null, null, "USER01", null, "AQAAAAEAACcQAAAAEBXP+vMUbNXQciL6xluY8n39xKhHTJJTQEHeH1Fu5ekSxyL3zAd4fjhEpDR3+CwtVA==", "37068334501", false, "86a92a5f-79c8-4c0a-9d9e-93c0a2af7885", false, false, "user01" },
                    { "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8", 0, null, null, null, "fad1f96e-a96b-4ab6-9d02-1b7dc523894d", null, null, false, "Kingsly", "Kebbell", false, null, null, "USER18", null, "AQAAAAEAACcQAAAAEFma5MAUmIZebuW/YQvQRfM9A5gf7kgFGLb70LfJVPFSsPAmpEV3uJR8/83+JSShFg==", "37067231518", false, "0f3077c7-5caa-4449-a0d0-f99e9bcef25c", false, false, "user18" },
                    { "b961ed5a-10d7-437c-ae2f-ec8a42e0b942", 0, null, null, null, "bf80575b-f7e7-41f6-b2f3-6443701738ac", null, null, false, "Granny", "Castiblanco", false, null, null, "USER17", null, "AQAAAAEAACcQAAAAEIbb5CC0RpF7MnnGXSYO9ZA9RBHFPXkTGS5MISt0vxV418mVFxctqZ66Q4F7/L3Ejg==", "37061271517", false, "d5792a8b-1949-47a1-bd63-5819186a3ced", false, false, "user17" },
                    { "45592ea4-e1b1-471c-8563-26f3b8e58989", 0, null, null, null, "26136236-e7fe-4078-a436-119f58396f4f", null, null, false, "Loydie", "Gianni", false, null, null, "USER16", null, "AQAAAAEAACcQAAAAEE9TI8qNphYmditjQuClZ363wfe2hoQ8Lv29csV+YIumKiUjRthEW/kPL7sclgyrgQ==", "37063734516", false, "4b3a03cb-e2f9-4fc0-a24b-adc8cc34572d", false, false, "user16" },
                    { "e2c4da34-02ca-4ab5-a6da-78561d5a687b", 0, null, null, null, "7b8dcf28-38e8-4fe6-8370-74009c77cb07", null, null, false, "Gard", "Santhouse", false, null, null, "USER15", null, "AQAAAAEAACcQAAAAELZFCuiEcOL1O22yrVhiEih6uMZ86gJnO+enMWeOtZA12uy51BQ34RvAJAIVa3+AhQ==", "37061737515", false, "77e2f1e8-1358-48c3-862a-7b2c1ca5bea7", false, false, "user15" },
                    { "d75ef10e-b29f-4264-bae7-3dd329d61bc4", 0, null, null, null, "8906f210-57b2-489f-a877-f69f62b7d2b9", null, null, false, "Rebbecca", "Ellingsworth", false, null, null, "USER14", null, "AQAAAAEAACcQAAAAEO9vNi8QlNEbvOw7ZMRC/euDJbLUq2U7cJdsgABe4zgcFf/clCd551UNpS2TpAvhHQ==", "37063733514", false, "80667c14-1402-4fee-aadd-89af2cf55936", false, false, "user14" },
                    { "09c3b944-c30e-42ad-93ed-33c3a4eb7130", 0, null, null, null, "bfc85e29-7c1d-41d9-b93e-f70988fcc1e4", null, null, false, "Quill", "Camelin", false, null, null, "USER13", null, "AQAAAAEAACcQAAAAEFuQQbyg8VMGJNv47akwtkJUvLfLONUxap70mb3AHdjKQRZLEg++h9asSKWXvpiHTg==", "37061734513", false, "9181ba8f-8bad-43ff-8a53-13a22f222bac", false, false, "user13" },
                    { "6cf22040-790b-4996-bf28-281ce4a6c613", 0, null, null, null, "7bbe4198-b81b-4152-a5f3-ea0e61217f8a", null, null, false, "Hadria", "Durkin", false, null, null, "USER12", null, "AQAAAAEAACcQAAAAEDLn3Wuf+J5DXHN4hqswiTLvPiOcsnn7m3Dn0eBjTSnpOoC3wQn0na7lhhrovSzJ0Q==", "37061534512", false, "2ce8dee5-579e-4a3d-bc24-f62bd4924b98", false, false, "user12" },
                    { "3878c586-0249-487e-8621-3e5bcbd38cde", 0, null, null, null, "31db655a-65ba-4568-bcfa-48da24e091ac", null, null, false, "Camila", "Rathborne", false, null, null, "USER11", null, "AQAAAAEAACcQAAAAECk/F4pkUZ7sLIpL1df8uYpsTO16s667TLOol/kjZfqqyzScURoPbkqV9+34u+P3IA==", "37061533511", false, "94af76f9-dfff-49ff-a4c2-7289c989d44b", false, false, "user11" },
                    { "b063338e-8b7b-4135-aa84-07f48cddca06", 0, null, null, null, "f91e272c-62e7-4bbf-a294-49aa1f4682e1", null, null, false, "Aggy", "Sterley", false, null, null, "USER10", null, "AQAAAAEAACcQAAAAEIeDXngSzxe+DkfBpatH5Ve0m/Qoe/tOLl1WqG5K3ReHit/Hyipuk/mz4+63NCNZzg==", "37061734510", false, "d2de88e6-0524-4dd3-8e8a-af61370d1694", false, false, "user10" },
                    { "7124516a-9d7b-4a92-93c1-8529113a1e0b", 0, null, null, null, "5e2001ba-de22-4f47-9ac0-f034bd2136a5", null, null, false, "Vaughan", "Cahen", false, null, null, "USER09", null, "AQAAAAEAACcQAAAAEIu9v0u6e8WVgVxK7vr1UYgRdUhlnIh7UGMMRBwE/0Ea5OHq0LwaNTjqDs0wOUiDHQ==", "37061394509", false, "c080342f-db39-47e3-a842-a3d54999239d", false, false, "user09" },
                    { "21cd7a65-4b90-4fd0-99ee-e0317e216f0d", 0, null, null, null, "4e7f50a0-fa63-414e-a75e-956079a1f0ac", null, null, false, "Tamiko", "McCreery", false, null, null, "USER08", null, "AQAAAAEAACcQAAAAEEH56ZUg4qIoEWovXsbZZAKbjo+Skzh0XIFApfHU8+TK/u0zg4xSmIZqg1edNsYLLw==", "37064545408", false, "d09b2e99-98ae-4f0c-ac16-47582013b313", false, false, "user08" },
                    { "545d5fd3-f04e-4a3c-85e0-17d3afff29d4", 0, null, null, null, "cd24d7c5-e3df-4281-bac1-df4a91b6ac3a", null, null, false, "Francisca", "Treves", false, null, null, "USER07", null, "AQAAAAEAACcQAAAAEFGis2HsW5EfQF+7x66WJYAfXLl9m0rGnWXOwA0Z9c6OmPEClAOPcHCkRGIXnWhghg==", "37061233507", false, "e6472d0b-cbf3-493e-b12f-a9ea2b875530", false, false, "user07" },
                    { "20380a83-579a-4336-83c1-2e89d4f39f83", 0, null, null, null, "5aa88091-2515-496b-a441-8e827d32d1b9", null, null, false, "Kalila", "Gemlbett", false, null, null, "USER06", null, "AQAAAAEAACcQAAAAEEjj7PV9Vo/dZyBfwisxVe6DjcoY6tCn2lLiMoxy7ZbHAElLm40NHxeTVnn3A0TgZQ==", "37061654506", false, "71380fb3-e484-4088-9f92-4c3e1101e656", false, false, "user06" },
                    { "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16", 0, null, null, null, "dd1c0ad9-d749-4e9a-b11b-60cf1fcb8321", null, null, false, "Corinne", "Farrell", false, null, null, "USER05", null, "AQAAAAEAACcQAAAAEOQiWC8sodUBNV+NtkhcDut6C0dn0MmYxQQUWypA0nakx4GWbSlhMR2OJl9GGlSsBA==", "37063034705", false, "8c90accb-194d-4953-9f3b-169986d4083c", false, false, "user05" },
                    { "0376277c-aa98-4fbb-9779-114fb75a4f06", 0, null, null, null, "63086d41-15b8-48d9-8af1-e4143710a916", null, null, false, "Colman", "Odhams", false, null, null, "USER04", null, "AQAAAAEAACcQAAAAELOsrNw56JlsVfLqhXos6lY9eGvygYqE/uenQttN+KgFU1yOQ4Xita0SBGYNsjoR4g==", "37068034504", false, "4fa160ab-4115-4e7f-a1e2-d2a96a2d8387", false, false, "user04" },
                    { "12c742ca-ec6f-4563-8a08-0e7ba0a449c8", 0, null, null, null, "d5c5275c-54d4-4262-b55d-2cfc4281d7bb", null, null, false, "Niall", "Irving", false, null, null, "USER03", null, "AQAAAAEAACcQAAAAEHqmxpeBOckpvHjo5qV4EJeHcV6aDdb8BVwTz/340q/mW0V/QXdXgZHBRjf/mDRfLw==", "37061034503", false, "c412c432-4f98-4f91-a548-c9a9a6111e51", false, false, "user03" },
                    { "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0", 0, null, null, null, "41a3d9a1-138d-4818-a75c-99d3662e89cb", null, null, false, "Rusty", "Sousa", false, null, null, "USER02", null, "AQAAAAEAACcQAAAAEHs8y0/9G41DXfGkOFJtkMhL63WSrZxTB/2gj04/PoteMqp9O3I1bVoeAA1SQteOyA==", "37061034702", false, "42c52fcd-fee1-4e96-8cb1-6636f2fb5987", false, false, "user02" },
                    { "6bf67d2d-15ab-465c-89c4-2d199289b8db", 0, null, null, null, "ab10e129-0884-49a0-94eb-f14855cc425e", null, null, false, "Tessa", "Picker", false, null, null, "USER19", null, "AQAAAAEAACcQAAAAEBLANqHhShdxRQt+S4TUwUPU32qb18A3dFnSeKqIlN/rmRcI+qzmKLfzbcI77lIl5A==", "37061271519", false, "40946359-aa46-4c83-be6b-93ae3885f726", false, false, "user19" },
                    { "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916", 0, null, null, null, "7628f777-0b8c-4a95-ad7d-6e812f4beecd", null, null, false, "Lorrin", "Dore", false, null, null, "USER20", null, "AQAAAAEAACcQAAAAEOfHlmqvLsCrm7fDy+tqqRiDhqeuCt0rL0eGZRuz50rJy5SpS5Hrm2v42juqY9+/wA==", "37061274520", false, "7cdc9701-619f-4de8-b4f7-b08b35daa7d1", false, false, "user20" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0376277c-aa98-4fbb-9779-114fb75a4f06");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09c3b944-c30e-42ad-93ed-33c3a4eb7130");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "12c742ca-ec6f-4563-8a08-0e7ba0a449c8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "20380a83-579a-4336-83c1-2e89d4f39f83");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21cd7a65-4b90-4fd0-99ee-e0317e216f0d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3878c586-0249-487e-8621-3e5bcbd38cde");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45592ea4-e1b1-471c-8563-26f3b8e58989");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "545d5fd3-f04e-4a3c-85e0-17d3afff29d4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bf67d2d-15ab-465c-89c4-2d199289b8db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6cf22040-790b-4996-bf28-281ce4a6c613");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7124516a-9d7b-4a92-93c1-8529113a1e0b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b063338e-8b7b-4135-aa84-07f48cddca06");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b961ed5a-10d7-437c-ae2f-ec8a42e0b942");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d75ef10e-b29f-4264-bae7-3dd329d61bc4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e2c4da34-02ca-4ab5-a6da-78561d5a687b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fe4d4664-ce1c-407f-85e3-3815555bb146");
        }
    }
}
