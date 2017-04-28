using Misaar.Abstract;
using Misaar.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Misaar.Concrete
{
    public class ProductRepository : IRepository<Product>
    {
        private ApplicationDbContext db;
        
        public ProductRepository(ApplicationDbContext context)
        {
            db = context;           
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await db.Products.Include(c => c.Category).ToListAsync();
        }
       
        public async Task<Product> Get(int? id)
        {
            return await db.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id);    
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
           
        }

        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
                     
        }

        public void Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
                db.Products.Remove(product);            
        }
       
        public void Dispose()
        {            
            db.Dispose();                       
        }

    }
}