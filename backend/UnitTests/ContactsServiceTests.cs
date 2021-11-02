using Application.Interfaces;
using Application.Services;
using Business.Services;
using Data.Interfaces;
using Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ContactsServiceTests
    {
        private readonly ContactsService _sut;
        private readonly Mock<IContactsRepository> _contactsRepository = new Mock<IContactsRepository>();
        private readonly Mock<IUsersRepository> _usersRepository = new Mock<IUsersRepository>();
        private readonly IMapperService _mapper = new MapperService(new ContactInformationMapper());

        public ContactsServiceTests()
        {
            _sut = new ContactsService(_contactsRepository.Object, _usersRepository.Object, _mapper);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsContact_WithExpectedId()
        {
            // Arrange
            var contactId = Guid.NewGuid().ToString();
            var requestedContact = new Contact { Id = contactId };

            _contactsRepository.Setup(x => x.FindByIdAsync(contactId))
                .ReturnsAsync(() => requestedContact);

            // Act
            var contact = await _sut.FindByIdAsync(contactId);

            // Assert
            Assert.Equal(contactId, contact.Id);
        }

        [Fact]
        public async Task GetUserWithAllContactsAsync_MapsPersonalContactCorrectly()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var requestedUser = new User
            {
                Contacts = new List<Contact>
                {
                    new Contact { Me = true }
                },
                ContactUsers = new List<ContactUser>(),
                UnacceptedShares = new List<UnacceptedShare>()
            };

            _usersRepository.Setup(x => x.GetUserWithAllContactsAsync(userId))
                .ReturnsAsync(() => requestedUser);

            // Act
            var response = await _sut.GetAllContactsAsync(userId);

            // Assert
            Assert.True(response.Count == 1);
            Assert.True(response.First().Me);
        }
    }
}
