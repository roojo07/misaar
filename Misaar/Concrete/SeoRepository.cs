using Misaar.Abstract;
using Misaar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Misaar.Concrete
{
    public class SeoRepository : IRepository<SeoData>
    {
        private ApplicationDbContext db;

        public SeoRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void Create(SeoData item)
        {
            db.SeoData.Add(item);
        }

        public void Delete(int id)
        {
            SeoData tag = db.SeoData.Find(id);
            if(tag != null)
            {
                db.SeoData.Remove(tag);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<SeoData> Get(int? id)
        {
            return await db.SeoData.FindAsync(id);
        }

        public async Task<IEnumerable<SeoData>> GetAll()
        {
            return await db.SeoData.ToListAsync();
        }

        public void Update(SeoData item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}