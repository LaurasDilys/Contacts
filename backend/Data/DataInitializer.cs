using Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DataInitializer
    {
        public static User[] Users()
        {
            var hasher = new PasswordHasher<User>();

            return new User[]
            {
                new User() { Id = "1c7a718b-642b-457f-8f8f-6490a6db4663", UserName = "user",   NormalizedUserName = "USER",   PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37065432100", FirstName = "Žuvis", LastName = "Paukštė" },
                new User() { Id = "fe4d4664-ce1c-407f-85e3-3815555bb146", UserName = "user01", NormalizedUserName = "USER01", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37068334501", FirstName = "John", LastName = "Bassham" },
                new User() { Id = "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8", UserName = "user02", NormalizedUserName = "USER02", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37065034702", FirstName = "Jill", LastName = "Sousa", },
                new User() { Id = "b961ed5a-10d7-437c-ae2f-ec8a42e0b942", UserName = "user03", NormalizedUserName = "USER03", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37069034503", FirstName = "Nolan", LastName = "Irving", },
                new User() { Id = "45592ea4-e1b1-471c-8563-26f3b8e58989", UserName = "user04", NormalizedUserName = "USER04", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37068034504", FirstName = "Colman", LastName = "Odhams", },
                new User() { Id = "e2c4da34-02ca-4ab5-a6da-78561d5a687b", UserName = "user05", NormalizedUserName = "USER05", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37063034705", FirstName = "Corinne", LastName = "Farrell", },
                new User() { Id = "d75ef10e-b29f-4264-bae7-3dd329d61bc4", UserName = "user06", NormalizedUserName = "USER06", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37064654506", FirstName = "Kalila", LastName = "Gemlbett", },
                new User() { Id = "09c3b944-c30e-42ad-93ed-33c3a4eb7130", UserName = "user07", NormalizedUserName = "USER07", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37067233507", FirstName = "Frank", LastName = "Treves", },
                new User() { Id = "6cf22040-790b-4996-bf28-281ce4a6c613", UserName = "user08", NormalizedUserName = "USER08", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37064545408", FirstName = "Tamiko", LastName = "McCreery", },
                new User() { Id = "3878c586-0249-487e-8621-3e5bcbd38cde", UserName = "user09", NormalizedUserName = "USER09", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37063394509", FirstName = "Rebbecca", LastName = "Cahen", },
                new User() { Id = "b063338e-8b7b-4135-aa84-07f48cddca06", UserName = "user10", NormalizedUserName = "USER10", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37068734510", FirstName = "Aggy", LastName = "Sterley", },
                new User() { Id = "7124516a-9d7b-4a92-93c1-8529113a1e0b", UserName = "user11", NormalizedUserName = "USER11", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37061533511", FirstName = "Camila", LastName = "Rathborne", },
                new User() { Id = "21cd7a65-4b90-4fd0-99ee-e0317e216f0d", UserName = "user12", NormalizedUserName = "USER12", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37063534512", FirstName = "Tom", LastName = "Durkin", },
                new User() { Id = "545d5fd3-f04e-4a3c-85e0-17d3afff29d4", UserName = "user13", NormalizedUserName = "USER13", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37061734513", FirstName = "Quill", LastName = "Camelin", },
                new User() { Id = "20380a83-579a-4336-83c1-2e89d4f39f83", UserName = "user14", NormalizedUserName = "USER14", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37063733514", FirstName = "Stephen", LastName = "Ellingsworth", },
                new User() { Id = "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16", UserName = "user15", NormalizedUserName = "USER15", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37068737515", FirstName = "Gordon", LastName = "Santhouse", },
                new User() { Id = "0376277c-aa98-4fbb-9779-114fb75a4f06", UserName = "user16", NormalizedUserName = "USER16", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37063734516", FirstName = "Louis", LastName = "Gianni", },
                new User() { Id = "12c742ca-ec6f-4563-8a08-0e7ba0a449c8", UserName = "user17", NormalizedUserName = "USER17", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37067271517", FirstName = "Tim", LastName = "Castiblanco", },
                new User() { Id = "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0", UserName = "user18", NormalizedUserName = "USER18", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37065231518", FirstName = "Steve", LastName = "Kebbell", },
                new User() { Id = "6bf67d2d-15ab-465c-89c4-2d199289b8db", UserName = "user19", NormalizedUserName = "USER19", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37062271519", FirstName = "Tessa", LastName = "Picker", },
                new User() { Id = "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916", UserName = "user20", NormalizedUserName = "USER20", PasswordHash = hasher.HashPassword(null, "password"), SecurityStamp = Guid.NewGuid().ToString(), ShowMyContact = true, PhoneNumber = "37061274520", FirstName = "Lorrin", LastName = "Dore", }
            };
        }

        public static Contact[] Contacts()
        {
            return new Contact[]
            {
                new Contact() { CreatorId = "1c7a718b-642b-457f-8f8f-6490a6db4663", Id = "261396d6-5733-432e-b939-74746e964548", Me = false, Notes = "My best friend :)", FirstName = "Banginis", LastName = "Delfinauskas" },
                new Contact() { CreatorId = "1c7a718b-642b-457f-8f8f-6490a6db4663", Id = "6717ac51-f9f2-48a9-93ca-5c66a6364cef", Me = true, PhoneNumber = "37065432100", FirstName = "Žuvis", LastName = "Paukštė" },
                new Contact() { CreatorId = "fe4d4664-ce1c-407f-85e3-3815555bb146", Id = "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc", Me = true, PhoneNumber = "37068334501", FirstName = "John", LastName = "Bassham" },
                new Contact() { CreatorId = "7b371581-fb18-4eca-b58a-9b2fd5d2f3b8", Id = "9be23521-04fc-4ec7-85a4-b246d68e58e4", Me = true, PhoneNumber = "37065034702", FirstName = "Jill", LastName = "Sousa", },
                new Contact() { CreatorId = "b961ed5a-10d7-437c-ae2f-ec8a42e0b942", Id = "9a128e08-fc6b-4abc-bc93-a101d434e5df", Me = true, PhoneNumber = "37069034503", FirstName = "Nolan", LastName = "Irving", },
                new Contact() { CreatorId = "45592ea4-e1b1-471c-8563-26f3b8e58989", Id = "3d0b85b1-8e0a-418b-add4-62cf87476a65", Me = true, PhoneNumber = "37068034504", FirstName = "Colman", LastName = "Odhams", },
                new Contact() { CreatorId = "e2c4da34-02ca-4ab5-a6da-78561d5a687b", Id = "a585db2e-f1e5-48ac-a62f-13461e00d8cd", Me = true, PhoneNumber = "37063034705", FirstName = "Corinne", LastName = "Farrell", },
                new Contact() { CreatorId = "d75ef10e-b29f-4264-bae7-3dd329d61bc4", Id = "cf120bf8-8fae-40c2-9202-7fe8df8afdbc", Me = true, PhoneNumber = "37064654506", FirstName = "Kalila", LastName = "Gemlbett", },
                new Contact() { CreatorId = "09c3b944-c30e-42ad-93ed-33c3a4eb7130", Id = "ea83de52-24f7-4e7d-9101-0fe6a5be77eb", Me = true, PhoneNumber = "37067233507", FirstName = "Frank", LastName = "Treves", },
                new Contact() { CreatorId = "6cf22040-790b-4996-bf28-281ce4a6c613", Id = "fd2c2c55-f898-496b-ad4f-8ddc7ee69412", Me = true, PhoneNumber = "37064545408", FirstName = "Tamiko", LastName = "McCreery", },
                new Contact() { CreatorId = "3878c586-0249-487e-8621-3e5bcbd38cde", Id = "a819f246-101d-4975-b20b-f038fc0b3f31", Me = true, PhoneNumber = "37063394509", FirstName = "Rebbecca", LastName = "Cahen", },
                new Contact() { CreatorId = "b063338e-8b7b-4135-aa84-07f48cddca06", Id = "22f3c470-8bad-480f-a232-80b88928464c", Me = true, PhoneNumber = "37068734510", FirstName = "Aggy", LastName = "Sterley", },
                new Contact() { CreatorId = "7124516a-9d7b-4a92-93c1-8529113a1e0b", Id = "e3fb3bc7-d0db-41b2-a3d6-9438914102e6", Me = true, PhoneNumber = "37061533511", FirstName = "Camila", LastName = "Rathborne", },
                new Contact() { CreatorId = "21cd7a65-4b90-4fd0-99ee-e0317e216f0d", Id = "1085fa01-7321-4b15-87d4-01d0b61cbd4d", Me = true, PhoneNumber = "37063534512", FirstName = "Tom", LastName = "Durkin", },
                new Contact() { CreatorId = "545d5fd3-f04e-4a3c-85e0-17d3afff29d4", Id = "b972126a-8c31-4f33-b3a7-2af8dc18852f", Me = true, PhoneNumber = "37061734513", FirstName = "Quill", LastName = "Camelin", },
                new Contact() { CreatorId = "20380a83-579a-4336-83c1-2e89d4f39f83", Id = "cea8e77c-e33d-40f2-a883-d5bb2defe941", Me = true, PhoneNumber = "37063733514", FirstName = "Stephen", LastName = "Ellingsworth", },
                new Contact() { CreatorId = "8a037a6f-0c0b-4803-aa23-f5b08b1b9f16", Id = "79165502-ce2e-401a-8688-3dfb2ebaa1bd", Me = true, PhoneNumber = "37068737515", FirstName = "Gordon", LastName = "Santhouse", },
                new Contact() { CreatorId = "0376277c-aa98-4fbb-9779-114fb75a4f06", Id = "a32d3490-d465-4f20-8e12-e14e85ebff58", Me = true, PhoneNumber = "37063734516", FirstName = "Louis", LastName = "Gianni", },
                new Contact() { CreatorId = "12c742ca-ec6f-4563-8a08-0e7ba0a449c8", Id = "d4aa4070-0b74-4225-9771-6f9460e4e25f", Me = true, PhoneNumber = "37067271517", FirstName = "Tim", LastName = "Castiblanco", },
                new Contact() { CreatorId = "1c0cde1d-4ea6-4474-8b87-c28f75fd1fb0", Id = "ab6c6904-0ada-408d-bccd-ba8b880fb433", Me = true, PhoneNumber = "37065231518", FirstName = "Steve", LastName = "Kebbell", },
                new Contact() { CreatorId = "6bf67d2d-15ab-465c-89c4-2d199289b8db", Id = "76532d83-c880-4af0-b35b-bdf69144288f", Me = true, PhoneNumber = "37062271519", FirstName = "Tessa", LastName = "Picker", },
                new Contact() { CreatorId = "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916", Id = "e35b0737-4c62-4891-9b52-6d3c14234b00", Me = true, PhoneNumber = "37061274520", FirstName = "Lorrin", LastName = "Dore", }
            };
        }

        public static ContactUser[] ContactUsers()
        {
            return new ContactUser[]
            {
                new ContactUser { ContactId = "6717ac51-f9f2-48a9-93ca-5c66a6364cef", UserId = "2de2bf7e-0aae-4e8a-b0ca-b94fc9ae4916" },
                new ContactUser { ContactId = "45c44e7c-e65d-4144-ba76-ae3d2f5e6adc", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                new ContactUser { ContactId = "9be23521-04fc-4ec7-85a4-b246d68e58e4", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                new ContactUser { ContactId = "9a128e08-fc6b-4abc-bc93-a101d434e5df", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" }
            };
        }

        public static UnacceptedShare[] UnacceptedShares()
        {
            return new UnacceptedShare[]
            {
                new UnacceptedShare { ContactId = "3d0b85b1-8e0a-418b-add4-62cf87476a65", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                new UnacceptedShare { ContactId = "a585db2e-f1e5-48ac-a62f-13461e00d8cd", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" },
                new UnacceptedShare { ContactId = "cf120bf8-8fae-40c2-9202-7fe8df8afdbc", UserId = "1c7a718b-642b-457f-8f8f-6490a6db4663" }
            };
        }
    }
}
