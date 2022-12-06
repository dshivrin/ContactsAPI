using ContactsAPI.Data.Models;
using ContactsAPI.Data.Repositories;
using ContactsAPI.Models;

namespace ContactsAPI.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contacRepository;

        public ContactService(IContactRepository contactRepository)
        {
            this.contacRepository = contactRepository;
        }
        public async Task<Contact> AddAsync(Contact contact)
        {
            return await contacRepository.AddAsync(contact);
        }

        public async Task<Contact> DeleteAsync(Contact contact)
        {
            return await contacRepository.DeleteAsync(contact);
        }

        public List<Contact> GetAll()
        {
            return contacRepository.GetAll().ToList();
        }

        public Task<Contact> GetContactByIDAsync(int id)
        {
           return contacRepository.GetContactByIDAsync(id);
        }

        public Task<Contact> UpdateAsync(Contact contact)
        {
            return contacRepository.UpdateAsync(contact);
        }
        public async Task<PagedResult> GetPaged(int skip, int take)
        {
            return await contacRepository.GetPagedContactsAsync(skip, take);
        }

    }
}
