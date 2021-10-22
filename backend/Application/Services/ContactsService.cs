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
    public class ContactsService : IContactsService
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

        public async Task<IEnumerable<IContact>> Get(string userId)
        {
            return await _repository.GetAsync(userId);
        }

        public async Task<IContact> Create(string userId, ICreateContactRequest request)
        {
            var contact = _mapper.ContactFrom(userId, request);
            return await _repository.CreateAsync(contact);
        }

        public async Task<IContact> Update(IUpdateContactRequest request)
        {
            var contact = await _repository.GetOneAsync(request.Id);
            _mapper.UpdateContact(contact, request);
            await _repository.SaveChangesAsync();
            return contact;
        }

        public async Task Delete(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
