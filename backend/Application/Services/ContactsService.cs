using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using Business.Interfaces.Services;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<IEnumerable<IContactResponse>> Get(string userId)
        {
            var contacts = await _repository.GetAsync(userId);
            var response = new List<IContactResponse>();
            foreach (var contact in contacts)
            {
                response.Add(_mapper.ContactResponseFrom(contact));
            }
            return response;
        }

        public async Task<IContactResponse> Create(string userId, ICreateContactRequest request)
        {
            var contact = _mapper.NewContactFrom(userId, request);
            await _repository.CreateAsync(contact);
            return _mapper.ContactResponseFrom(contact);
        }

        public async Task<IContactResponse> Update(IUpdateContactRequest request)
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
