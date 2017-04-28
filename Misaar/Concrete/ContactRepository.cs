using Misaar.Abstract;
using Misaar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Misaar.Concrete
{
    public class ContactRepository : IRepository<Contact>
    {

        private ApplicationDbContext db;

        public ContactRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void Create(Contact item)
        {
            db.Contacts.Add(item);
        }

        public void Delete(int id)
        {
            Contact obj = db.Contacts.Find(id);
            if (obj != null)
            {
                db.Contacts.Remove(obj);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<Contact> Get(int? id)
        {
            return await db.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await db.Contacts.ToListAsync();
        }

        public IEnumerable<Contact> GetAllSync()
        {
            return db.Contacts.ToList();
        }

        public void Update(Contact contact)
        {
            db.Entry(contact).State = EntityState.Modified;
        }
    }
}