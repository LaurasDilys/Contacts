using Business.Interfaces.Dto;
using Business.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IContactsService
    {
        public Task<bool> Exists(string key);
        public Task<IEnumerable<IContact>> Get(string userKey);
        public Task<IContact> Create(string userId, ICreateContactRequest request);
        public Task<IContact> Update(IUpdateContactRequest request);
        public Task Delete(string key);
    }
}
