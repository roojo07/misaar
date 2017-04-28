using Misaar.Abstract;
using Misaar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Misaar.Concrete
{
    public class ArticleRepository : IRepository<Article>
    {
        private ApplicationDbContext db;

        public ArticleRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            return await db.Articles.ToListAsync();
        }
        public async Task<IEnumerable<Article>> GetAll(string category)
        {
            return await db.Articles.Where(p => p.Category == category).ToListAsync();
        }

       
        public async Task<Article> Get(int? id)
        {
            return await db.Articles.Include(s => s.Pictures).FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Create(Article article)
        {
            db.Articles.Add(article);
        }

        public void Update(Article article)
        {
            db.Entry(article).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Article article = db.Articles.Find(id);
            if (article != null)
                db.Articles.Remove(article);
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}