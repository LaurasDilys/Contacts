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
        public static User[] MockUsers()
        {
            var hasher = new PasswordHasher<User>();

            return new User[]
            {
                new User() { UserName = "user01", NormalizedUserName = "USER01", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37068334501", FirstName = "Atalanta", LastName = "Bassham" },
                new User() { UserName = "user02", NormalizedUserName = "USER02", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061034702", FirstName = "Rusty", LastName = "Sousa", },
                new User() { UserName = "user03", NormalizedUserName = "USER03", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061034503", FirstName = "Niall", LastName = "Irving", },
                new User() { UserName = "user04", NormalizedUserName = "USER04", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37068034504", FirstName = "Colman", LastName = "Odhams", },
                new User() { UserName = "user05", NormalizedUserName = "USER05", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37063034705", FirstName = "Corinne", LastName = "Farrell", },
                new User() { UserName = "user06", NormalizedUserName = "USER06", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061654506", FirstName = "Kalila", LastName = "Gemlbett", },
                new User() { UserName = "user07", NormalizedUserName = "USER07", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061233507", FirstName = "Francisca", LastName = "Treves", },
                new User() { UserName = "user08", NormalizedUserName = "USER08", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37064545408", FirstName = "Tamiko", LastName = "McCreery", },
                new User() { UserName = "user09", NormalizedUserName = "USER09", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061394509", FirstName = "Vaughan", LastName = "Cahen", },
                new User() { UserName = "user10", NormalizedUserName = "USER10", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061734510", FirstName = "Aggy", LastName = "Sterley", },
                new User() { UserName = "user11", NormalizedUserName = "USER11", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061533511", FirstName = "Camila", LastName = "Rathborne", },
                new User() { UserName = "user12", NormalizedUserName = "USER12", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061534512", FirstName = "Hadria", LastName = "Durkin", },
                new User() { UserName = "user13", NormalizedUserName = "USER13", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061734513", FirstName = "Quill", LastName = "Camelin", },
                new User() { UserName = "user14", NormalizedUserName = "USER14", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37063733514", FirstName = "Rebbecca", LastName = "Ellingsworth", },
                new User() { UserName = "user15", NormalizedUserName = "USER15", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061737515", FirstName = "Gard", LastName = "Santhouse", },
                new User() { UserName = "user16", NormalizedUserName = "USER16", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37063734516", FirstName = "Loydie", LastName = "Gianni", },
                new User() { UserName = "user17", NormalizedUserName = "USER17", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061271517", FirstName = "Granny", LastName = "Castiblanco", },
                new User() { UserName = "user18", NormalizedUserName = "USER18", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37067231518", FirstName = "Kingsly", LastName = "Kebbell", },
                new User() { UserName = "user19", NormalizedUserName = "USER19", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061271519", FirstName = "Tessa", LastName = "Picker", },
                new User() { UserName = "user20", NormalizedUserName = "USER20", PasswordHash = hasher.HashPassword(null, "password"), ShowMyContact = false, Id = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString(), PhoneNumber = "37061274520", FirstName = "Lorrin", LastName = "Dore", }
            };
        }
    }
}
