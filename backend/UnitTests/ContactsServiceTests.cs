using Application.Services;
using Data.Models;
using Data.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ContactsServiceTests
    {
        private readonly ContactsService _sut;
        private readonly Mock<IContactsRepository> _contactsRepository = new Mock<IContactsRepository>();
        private readonly Mock<IUsersRepository> _usersRepository = new Mock<IUsersRepository>();
        private readonly Mock<IMapperService> _mapper = new Mock<IMapperService>();

        public ContactsServiceTests()
        {
            _sut = new ContactsService(_contactsRepository.Object, _usersRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsContact_WithExpectedId()
        {
            // Arrange
            var contactId = Guid.NewGuid().ToString();
            var returnedContact = new Contact { Id = contactId };

            _contactsRepository.Setup(x => x.FindByIdAsync(contactId))
                .ReturnsAsync(() => returnedContact);

            // Act
            var contact = await _sut.FindByIdAsync(contactId);

            // Assert
            Assert.Equal(contactId, contact.Id);
        }
    }
}
