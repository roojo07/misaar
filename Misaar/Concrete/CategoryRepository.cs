using Misaar.Abstract;
using System.Collections.Generic;
using System.Linq;
using Misaar.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Misaar.Concrete
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public void Create(Category cat)
        {
            db.Categories.Add(cat);
        }

        public void Delete(int id)
        {
            Category cat = db.Categories.Find(id);
            if (cat != null)
                db.Categories.Remove(cat);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<Category>Get(string name)
        {
            return await db.Categories.Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await db.Categories.Include(p => p.Products).ToListAsync();
        }

        public IEnumerable<Category> GetAllSync()
        {
            return db.Categories.Include(p => p.Products).ToList();
        }

        public async Task<Category> Get(int? id)
        {
            return await db.Categories.Include(s => s.Files).Include(s => s.Products).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Category cat)
        {
            db.Entry(cat).State = EntityState.Modified;
        }
    }
}