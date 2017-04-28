using Misaar.Abstract;
using Misaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Misaar.Concrete
{
    public class MetaRepository : IRepository<MetaTag>
    {
        private MisaarContext db;

        public MetaRepository(MisaarContext context)
        {
            db = context;
        }
        public void Create(MetaTag item)
        {
            db.MetaTags.Add(item);
        }

        public void Delete(int id)
        {
            MetaTag tag = db.MetaTags.Find(id);
            if(tag != null)
            {
                db.MetaTags.Remove(tag);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<MetaTag> Get(int? id)
        {
            return await db.MetaTags.FindAsync(id);
        }

        public async Task<IEnumerable<MetaTag>> GetAll()
        {
            return await db.MetaTags.ToListAsync();
        }

        public void Update(MetaTag item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

    }
}