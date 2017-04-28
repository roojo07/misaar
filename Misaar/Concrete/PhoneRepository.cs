using Misaar.Abstract;
using Misaar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Misaar.Concrete
{
    public class PhoneRepository : IRepository<Phone>
    {
        private ApplicationDbContext db;

        public PhoneRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void Create(Phone item)
        {
            db.Phones.Add(item);
        }

        public void Delete(int id)
        {
            Phone obj = db.Phones.Find(id);
            if (obj != null)
            {
                db.Phones.Remove(obj);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<Phone> Get(int? id)
        {
            return await db.Phones.FindAsync(id);
        }

        public async Task<IEnumerable<Phone>> GetAll()
        {
            return await db.Phones.ToListAsync();
        }

        public IEnumerable<Phone> GetAllSync()
        {
            return db.Phones.ToList();
        }


        public void Update(Phone item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}