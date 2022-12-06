using ContactsAPI.Data.Models;
using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAPI.Data.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactByIDAsync(int id);

        Task<List<Contact>> GetAllContactsAsync();

        Task<PagedResult> GetPagedContactsAsync(int skip, int take);
    }
}
