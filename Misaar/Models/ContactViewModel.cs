using System.Collections.Generic;

namespace Misaar.Models
{
    public class ContactViewModel
    {
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}