using ContactsAPI.Data.Models;
using ContactsAPI.Models;

namespace ContactsAPI.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactByIDAsync(int id);

        Task<Contact> GetContactByEmailAsync(Contact contact);

        Task<List<Contact>> GetAllContactsAsync();

        Task<PagedResult> GetPagedContactsAsync(int skip, int take);
    }
}
