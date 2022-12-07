using ContactsAPI.Models;

namespace ContactsAPI.Data.Models
{
    public class PagedResult
    {
        public List<Contact> Contacts { get; set; }

        public int Total { get; set; }

    }
}
