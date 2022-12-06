using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAPI.Data.Models
{
    public class PagedResult
    {
        public List<Contact> Contacts { get; set; }

        public int Total { get; set; }

    }
}
