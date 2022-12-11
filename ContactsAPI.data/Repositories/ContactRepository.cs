using ContactsAPI.Data.Models;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly ContactContext contactContext;

        public ContactRepository(ContactContext contactContext) : base(contactContext)
        {
            this.contactContext = contactContext;
        }

        public async Task<List<Contact>> GetAllContactsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<PagedResult> GetPagedContactsAsync(int skip, int take)
        {
            var result = await contactContext.Contact.Skip(skip).Take(take).ToListAsync();
            var total = await GetAll().CountAsync();

            return new PagedResult { Contacts = result, Total = total };
        }

        public async Task<Contact> GetContactByIDAsync(int id)
        {
            var result = await contactContext.Contact.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null)
            {
                throw new Exception($"{nameof(GetContactByIDAsync)} Couldn't retrieve entities for id {id}");
            }

            return result;
        }

        public async Task<Contact> GetContactByEmailAsync(Contact contact)
        {
            ArgumentNullException.ThrowIfNull(contact);
            //var result = contactContext.Contact.Where(c => c.Email == email).FirstOrDefaultAsync();
            var result = await contactContext.Contact.FirstAsync(c => c.Email == contact.Email);
            if (result == null)
            {
                throw new Exception($"{nameof(GetContactByIDAsync)} Couldn't retrieve entities for contact {contact}");
            }
            return result;

        }

    }
}
