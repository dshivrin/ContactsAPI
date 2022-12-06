using ContactsAPI.Data.Models;
using ContactsAPI.Models;

namespace ContactsAPI.Service.Services
{
    public interface IContactService
    {
        List<Contact> GetAll();

        Task<PagedResult> GetPaged(int skip, int take);

        Task<Contact> GetContactByIDAsync(int id);

        Task<Contact> AddAsync(Contact contact);

        Task<Contact> UpdateAsync(Contact contact);

        Task<Contact> DeleteAsync(Contact contact);
    }
}
