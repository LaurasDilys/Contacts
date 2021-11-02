using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddMockEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AlternativeEmail", "AlternativePhoneNumber", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Notes", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "ShowMyContact", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1c7a718b-642b-457f-8f8f-6490a6db4663", 0, null, null, null, "fd654cc0-ead4-4299-850a-4dd8dbcd45ba", null, null, false, "Žuvis", "Paukštė", false, null, null, "USER", null, "AQAAAAEAACcQAAAAEDxPhq2ComglJ40apP5mfdHGYQFOuV+rM+E5hmDwth1sADQvXfzGrmvSQ894Jg2fPA==", "37065432100", false, "f8f1374d-da28-4006-b5cf-f0466782a8a5", true, false, "user" },
                    { "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0", 0, null, null, null, "fb53f0de-6265-4a37-ba08-3dc13b62d9d8", null, null, false, "Steve", "Kebbell", false, null, null, "USER18", null, "AQAAAAEAACcQAAAAEBVYSmDjSAtn/hzM6t17UlZa/LbebUHsOWm/OBj9W9NuSdwJkyv0t/eZJXltefty0g==", "37065231518", false, "22fbab60-1cb8-4d0e-8b64-74cb929cee5b", true, false, "user18" },
                    { "12c742ca-ec6f-4563-8a08-0e7ba0a449c8", 0, null, null, null, "ccdad726-7333-495a-8e48-d1567a3ab49a", null, null, false, "Tim", "Castiblanco", false, null, null, "USER17", null, "AQAAAAEAACcQAAAAELRB4Nv/EBh9kVgngUy/s8MMMsHNLw8n4SoQQgKfSIQ1ZRDJ+2zN3l0SVZBG3AzNRA==", "37067271517", false, "4ed9188f-7674-47d6-9cef-9dcb31948021", true, false, "user17" },
                    { "0376277c-aa98-4fbb-9779-114fb75a4f06", 0, null, null, null, "990cfccc-9f42-49a4-802b-bdf1e3f9e7ea", null, null, false, "Louis", "Gianni", false, null, null, "USER16", null, "AQAAAAEAACcQAAAAEH0362Vfp7UDTjWBOoG6gMyuR3fzy0rTIA+QguEPLXfXoyRTlAoLBYFdx1O032ZrXw==", "37063734516", false, "df5d77f0-4aa6-4a4f-9aad-af738929c2b8", true, false, "user16" },
                    { "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16", 0, null, null, null, "4785afaa-b809-4062-bb83-f1caa23af49c", null, null, false, "Gordon", "Santhouse", false, null, null, "USER15", null, "AQAAAAEAACcQAAAAEB/5JfSLV2hMwui9VffqVT1uLJKYkX6QIPjVG27WfsbZb56lx9aWponAYLf/8Y4+zQ==", "37068737515", false, "6e296db6-6ff9-4225-9476-52c9a871591a", true, false, "user15" },
                    { "20380a83-579a-4336-83c1-2e89d4f39f83", 0, null, null, null, "e343e687-1c43-4c58-86e6-8920ffd2213d", null, null, false, "Stephen", "Ellingsworth", false, null, null, "USER14", null, "AQAAAAEAACcQAAAAEKxty9Ch+GhlH7Ff035LIHoRn+kPVzE+Y13VoQyMG8PMA8Sjeq4FenMRL5TeqDSKTA==", "37063733514", false, "26753e0e-f81a-40b2-84fc-6f690ff1e6b8", true, false, "user14" },
                    { "545d5fd3-f04e-4a3c-85e0-17d3afff29d4", 0, null, null, null, "4599d67a-2d87-4358-a5ce-263037c2be00", null, null, false, "Quill", "Camelin", false, null, null, "USER13", null, "AQAAAAEAACcQAAAAELfsxg5z1NNe8I6UB1uweKuaBDu7URVPl2AxnkkDrhMq2i5tUgTKH9aWpWvet/kuFA==", "37061734513", false, "37a3b033-b77a-40de-8931-fc4236432333", true, false, "user13" },
                    { "21cd7a65-4b90-4fd0-99ee-e0317e216f0d", 0, null, null, null, "5ed14431-ddac-40a1-b072-d1a09f9250ad", null, null, false, "Tom", "Durkin", false, null, null, "USER12", null, "AQAAAAEAACcQAAAAEFoFu3q62jqBdBq8kxJY7vmQ7If1TnVqeJtIJwuNqCYRQH9MlUWypZiS5QrMyduqAA==", "37063534512", false, "6edcb045-e008-48fe-bed9-3c77443e85a2", true, false, "user12" },
                    { "7124516a-9d7b-4a92-93c1-8529113a1e0b", 0, null, null, null, "c51c1314-da5b-4865-8898-19571c95e1c4", null, null, false, "Camila", "Rathborne", false, null, null, "USER11", null, "AQAAAAEAACcQAAAAEGU/pqhcqi2CEnhski4uZv30SI+gptkCYxCHm2MmtMkyoAmxB50BwXBZYJiIdXPauQ==", "37061533511", false, "9a84cad4-ad25-44a3-8dbd-2f387aa7eb35", true, false, "user11" },
                    { "6bf67d2d-15ab-465c-89c4-2d199289b8db", 0, null, null, null, "4a26adae-646c-4969-a85a-c0bb01aca7a4", null, null, false, "Tessa", "Picker", false, null, null, "USER19", null, "AQAAAAEAACcQAAAAEAqvKkqqeYoet0cV/+FcH3IPMe6zy/I/g/Pn5k7B73q0L/g2qAlfhS59T0aL64mSyQ==", "37062271519", false, "929c28d7-8b89-43d4-a816-30e63526f148", true, false, "user19" },
                    { "b063338e-8b7b-4135-aa84-07f48cddca06", 0, null, null, null, "147b7fd4-895b-41fe-9cf9-2bb92f9a34e4", null, null, false, "Aggy", "Sterley", false, null, null, "USER10", null, "AQAAAAEAACcQAAAAEPAJf4+yCYCNwXyr1gJjCwUk3YgwXDIWI8yROlbGOCslzlecY2wX5AkywFAZPV7xtA==", "37068734510", false, "8fd5f620-35f2-47be-b12b-58a4e4d3d423", true, false, "user10" },
                    { "6cf22040-790b-4996-bf28-281ce4a6c613", 0, null, null, null, "41786b16-d33b-4845-b3f3-238cf3161e10", null, null, false, "Tamiko", "McCreery", false, null, null, "USER08", null, "AQAAAAEAACcQAAAAEPVyKuOM0FpiXc0MZtcQqABT2+P3p0spVUL89PP9WbRjQbYGB4w07F01oL4UR6+nlA==", "37064545408", false, "30d037c5-3ebd-4c55-a559-b4cbca787615", true, false, "user08" },
                    { "09c3b944-c30e-42ad-93ed-33c3a4eb7130", 0, null, null, null, "c9cc9d1f-cd7a-4e9b-9bda-6f56ef4b1f65", null, null, false, "Frank", "Treves", false, null, null, "USER07", null, "AQAAAAEAACcQAAAAEMMWExJY6hupoJpGFfcVFBPhYBt27FvcImzuDKswB1wyMVKpdfbWcjS/CkUigd76pA==", "37067233507", false, "f716ead1-c220-4aa7-af8f-621c79b34d1b", true, false, "user07" },
                    { "d75ef10e-b29f-4264-bae7-3dd329d61bc4", 0, null, null, null, "ac8c97ae-cf03-4fcd-9e26-49022058550c", null, null, false, "Kalila", "Gemlbett", false, null, null, "USER06", null, "AQAAAAEAACcQAAAAEGnZpQL8AK7p2DDS3zWMBfsq1BHOijHw3/+c51FGcpKe2q15VTuV0lIBIWuUwJEbFg==", "37064654506", false, "f685ba25-89d7-4925-912a-e17eb7dfc524", true, false, "user06" },
                    { "e2c4da34-02ca-4ab5-a6da-78561d5a687b", 0, null, null, null, "64d00cfb-362a-48d5-8aeb-8e50e3cc41e1", null, null, false, "Corinne", "Farrell", false, null, null, "USER05", null, "AQAAAAEAACcQAAAAEIeamzxYD2zRJPUZoMOMGhvLeLWTrXQXF9LzVIot8r7OCNpqPD6l6JE/Ba3I/9tt5g==", "37063034705", false, "eb8791dc-6196-4819-8684-1c262187b964", true, false, "user05" },
                    { "45592ea4-e1b1-471c-8563-26f3b8e58989", 0, null, null, null, "89244028-e76f-4143-a315-20946d99e279", null, null, false, "Colman", "Odhams", false, null, null, "USER04", null, "AQAAAAEAACcQAAAAEOS352Wyb8nVTWesT16Juxt61fJbJ0ooCaXixiZwKryLM3W+X9dIiPu1wdWFae1zqA==", "37068034504", false, "bc3bc9a5-5188-43cc-82e1-1194235c499b", true, false, "user04" },
                    { "b961ed5a-10d7-437c-ae2f-ec8a42e0b942", 0, null, null, null, "334ac604-dfde-4d9e-ba70-9ba2597b249f", null, null, false, "Nolan", "Irving", false, null, null, "USER03", null, "AQAAAAEAACcQAAAAEIKfN7vG4GIc7tGyR6b5kDxJj/jDbvnGjSWlEY7bi+UvhLC8922hnXK6DKLu+dGivw==", "37069034503", false, "8a2594ac-0e20-4667-8dc0-f5e891c450b2", true, false, "user03" },
                    { "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8", 0, null, null, null, "bde3c71b-3036-41a7-9fdb-3b6db0e55e03", null, null, false, "Jill", "Sousa", false, null, null, "USER02", null, "AQAAAAEAACcQAAAAEODyRMV1hQo/7xIS8XmhLcCMCsMCpztRSVAmG5iL/swzVLW2km2xHEA09+uUZGkxtw==", "37065034702", false, "0238242b-1ed7-4a05-b3f8-472200c720f5", true, false, "user02" },
                    { "fe4d4664-ce1c-407f-85e3-3815555bb146", 0, null, null, null, "34993b57-95f8-4992-9b4f-198f9b5205ae", null, null, false, "John", "Bassham", false, null, null, "USER01", null, "AQAAAAEAACcQAAAAEDdaPBiAUdKiIekNCnkxk1I7kRlJCo8ZFJkSD5Q5hAcN06ywaiNcXps2SoQIg6S9Gw==", "37068334501", false, "44105175-06c0-469b-93de-4afc236cd0b0", true, false, "user01" },
                    { "3878c586-0249-487e-8621-3e5bcbd38cde", 0, null, null, null, "f42eb921-b885-47c7-a16d-d01730e09d98", null, null, false, "Rebbecca", "Cahen", false, null, null, "USER09", null, "AQAAAAEAACcQAAAAEJdf3dX1ixxGEUX6j04zW/D9t0pZfCSLxPFktUc02E6XacLxlG872GH/uSWw074tGw==", "37063394509", false, "8c75bbda-6c24-4bda-9dd5-5e539beff945", true, false, "user09" },
                    { "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916", 0, null, null, null, "3d4d101b-c0f2-4d32-8511-ebd8ff2782ac", null, null, false, "Lorrin", "Dore", false, null, null, "USER20", null, "AQAAAAEAACcQAAAAEO9SFwATGfoebjFaJ2jiACuYePKAg0bJxUwOsRYamiCn2E0kQNT2N/9K1WUl27vBlg==", "37061274520", false, "ea67f968-6363-410b-8dd6-fdeff042a0d2", true, false, "user20" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Address", "AlternativeEmail", "AlternativePhoneNumber", "CreatorId", "DateOfBirth", "Email", "FirstName", "LastName", "Me", "Notes", "PhoneNumber" },
                values: new object[,]
                {
                    { "261396d6-5733-432e-b939-74746e964548", null, null, null, "1c7a718b-642b-457f-8f8f-6490a6db4663", null, null, "Banginis", "Delfinauskas", false, "My best friend :)", null },
                    { "ab6c6904-0ada-408d-bccd-ba8b880fb433", null, null, null, "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0", null, null, "Steve", "Kebbell", true, null, "37065231518" },
                    { "d4aa4070-0b74-4225-9771-6f9460e4e25f", null, null, null, "12c742ca-ec6f-4563-8a08-0e7ba0a449c8", null, null, "Tim", "Castiblanco", true, null, "37067271517" },
                    { "a32d3490-d465-4f20-8e12-e14e85ebff58", null, null, null, "0376277c-aa98-4fbb-9779-114fb75a4f06", null, null, "Louis", "Gianni", true, null, "37063734516" },
                    { "79165502-ce2e-401a-8688-3dfb2ebaa1bd", null, null, null, "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16", null, null, "Gordon", "Santhouse", true, null, "37068737515" },
                    { "cea8e77c-e33d-40f2-a883-d5bb2defe941", null, null, null, "20380a83-579a-4336-83c1-2e89d4f39f83", null, null, "Stephen", "Ellingsworth", true, null, "37063733514" },
                    { "b972126a-8c31-4f33-b3a7-2af8dc18852f", null, null, null, "545d5fd3-f04e-4a3c-85e0-17d3afff29d4", null, null, "Quill", "Camelin", true, null, "37061734513" },
                    { "1085fa01-7321-4b15-87d4-01d0b61cbd4d", null, null, null, "21cd7a65-4b90-4fd0-99ee-e0317e216f0d", null, null, "Tom", "Durkin", true, null, "37063534512" },
                    { "e3fb3bc7-d0db-41b2-a3d6-9438914102e6", null, null, null, "7124516a-9d7b-4a92-93c1-8529113a1e0b", null, null, "Camila", "Rathborne", true, null, "37061533511" },
                    { "22f3c470-8bad-480f-a232-80b88928464c", null, null, null, "b063338e-8b7b-4135-aa84-07f48cddca06", null, null, "Aggy", "Sterley", true, null, "37068734510" },
                    { "a819f246-101d-4975-b20b-f038fc0b3f31", null, null, null, "3878c586-0249-487e-8621-3e5bcbd38cde", null, null, "Rebbecca", "Cahen", true, null, "37063394509" },
                    { "fd2c2c55-f898-496b-ad4f-8ddc7ee69412", null, null, null, "6cf22040-790b-4996-bf28-281ce4a6c613", null, null, "Tamiko", "McCreery", true, null, "37064545408" },
                    { "ea83de52-24f7-4e7d-9101-0fe6a5be77eb", null, null, null, "09c3b944-c30e-42ad-93ed-33c3a4eb7130", null, null, "Frank", "Treves", true, null, "37067233507" },
                    { "cf120bf8-8fae-40c2-9202-7fe8df8afdbc", null, null, null, "d75ef10e-b29f-4264-bae7-3dd329d61bc4", null, null, "Kalila", "Gemlbett", true, null, "37064654506" },
                    { "a585db2e-f1e5-48ac-a62f-13461e00d8cd", null, null, null, "e2c4da34-02ca-4ab5-a6da-78561d5a687b", null, null, "Corinne", "Farrell", true, null, "37063034705" },
                    { "3d0b85b1-8e0a-418b-add4-62cf87476a65", null, null, null, "45592ea4-e1b1-471c-8563-26f3b8e58989", null, null, "Colman", "Odhams", true, null, "37068034504" },
                    { "9a128e08-fc6b-4abc-bc93-a101d434e5df", null, null, null, "b961ed5a-10d7-437c-ae2f-ec8a42e0b942", null, null, "Nolan", "Irving", true, null, "37069034503" },
                    { "9be23521-04fc-4ec7-85a4-b246d68e58e4", null, null, null, "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8", null, null, "Jill", "Sousa", true, null, "37065034702" },
                    { "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc", null, null, null, "fe4d4664-ce1c-407f-85e3-3815555bb146", null, null, "John", "Bassham", true, null, "37068334501" },
                    { "6717ac51-f9f2-48a9-93ca-5c66a6364cef", null, null, null, "1c7a718b-642b-457f-8f8f-6490a6db4663", null, null, "Žuvis", "Paukštė", true, null, "37065432100" },
                    { "76532d83-c880-4af0-b35b-bdf69144288f", null, null, null, "6bf67d2d-15ab-465c-89c4-2d199289b8db", null, null, "Tessa", "Picker", true, null, "37062271519" },
                    { "e35b0737-4c62-4891-9b52-6d3c14234b00", null, null, null, "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916", null, null, "Lorrin", "Dore", true, null, "37061274520" }
                });

            migrationBuilder.InsertData(
                table: "ContactUsers",
                columns: new[] { "ContactId", "UserId" },
                values: new object[,]
                {
                    { "6717ac51-f9f2-48a9-93ca-5c66a6364cef", "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916" },
                    { "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc", "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                    { "9be23521-04fc-4ec7-85a4-b246d68e58e4", "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                    { "9a128e08-fc6b-4abc-bc93-a101d434e5df", "1c7a718b-642b-457f-8f8f-6490a6db4663" }
                });

            migrationBuilder.InsertData(
                table: "UnacceptedShares",
                columns: new[] { "ContactId", "UserId" },
                values: new object[,]
                {
                    { "3d0b85b1-8e0a-418b-add4-62cf87476a65", "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                    { "a585db2e-f1e5-48ac-a62f-13461e00d8cd", "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                    { "cf120bf8-8fae-40c2-9202-7fe8df8afdbc", "1c7a718b-642b-457f-8f8f-6490a6db4663" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactUsers",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

            migrationBuilder.DeleteData(
                table: "ContactUsers",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "6717ac51-f9f2-48a9-93ca-5c66a6364cef", "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916" });

            migrationBuilder.DeleteData(
                table: "ContactUsers",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "9a128e08-fc6b-4abc-bc93-a101d434e5df", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

            migrationBuilder.DeleteData(
                table: "ContactUsers",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "9be23521-04fc-4ec7-85a4-b246d68e58e4", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "1085fa01-7321-4b15-87d4-01d0b61cbd4d");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "22f3c470-8bad-480f-a232-80b88928464c");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "261396d6-5733-432e-b939-74746e964548");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "76532d83-c880-4af0-b35b-bdf69144288f");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "79165502-ce2e-401a-8688-3dfb2ebaa1bd");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "a32d3490-d465-4f20-8e12-e14e85ebff58");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "a819f246-101d-4975-b20b-f038fc0b3f31");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "ab6c6904-0ada-408d-bccd-ba8b880fb433");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "b972126a-8c31-4f33-b3a7-2af8dc18852f");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "cea8e77c-e33d-40f2-a883-d5bb2defe941");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "d4aa4070-0b74-4225-9771-6f9460e4e25f");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "e35b0737-4c62-4891-9b52-6d3c14234b00");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "e3fb3bc7-d0db-41b2-a3d6-9438914102e6");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "ea83de52-24f7-4e7d-9101-0fe6a5be77eb");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "fd2c2c55-f898-496b-ad4f-8ddc7ee69412");

            migrationBuilder.DeleteData(
                table: "UnacceptedShares",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "3d0b85b1-8e0a-418b-add4-62cf87476a65", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

            migrationBuilder.DeleteData(
                table: "UnacceptedShares",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "a585db2e-f1e5-48ac-a62f-13461e00d8cd", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

            migrationBuilder.DeleteData(
                table: "UnacceptedShares",
                keyColumns: new[] { "ContactId", "UserId" },
                keyValues: new object[] { "cf120bf8-8fae-40c2-9202-7fe8df8afdbc", "1c7a718b-642b-457f-8f8f-6490a6db4663" });

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
                keyValue: "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b063338e-8b7b-4135-aa84-07f48cddca06");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "3d0b85b1-8e0a-418b-add4-62cf87476a65");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "6717ac51-f9f2-48a9-93ca-5c66a6364cef");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "9a128e08-fc6b-4abc-bc93-a101d434e5df");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "9be23521-04fc-4ec7-85a4-b246d68e58e4");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "a585db2e-f1e5-48ac-a62f-13461e00d8cd");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: "cf120bf8-8fae-40c2-9202-7fe8df8afdbc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1c7a718b-642b-457f-8f8f-6490a6db4663");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45592ea4-e1b1-471c-8563-26f3b8e58989");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8");

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
