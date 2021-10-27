using Application.Dto.Contact;
using Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContactsService
    {
        private readonly ContactsRepository _repository;
        private readonly MapperService _mapper;

        public ContactsService(ContactsRepository repository, MapperService mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Exists(string id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<ICollection<ContactResponse>> Get(string userId)
        {
            var contacts = await _repository.GetAsync(userId);
            var response = new List<ContactResponse>();
            foreach (var contact in contacts)
            {
                response.Add(_mapper.ContactResponseFrom(contact));
            }
            return response;
        }

        public async Task<ContactResponse> Create(string userId, CreateContactRequest request)
        {
            var contact = _mapper.NewContactFrom(userId, request);
            await _repository.CreateAsync(contact);
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<ContactResponse> Update(UpdateContactRequest request)
        {
            var contact = await _repository.GetOneAsync(request.Id);
            _mapper.UpdateContact(contact, request);
            await _repository.SaveChangesAsync();
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task Delete(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
