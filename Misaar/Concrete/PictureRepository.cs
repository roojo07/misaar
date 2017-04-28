using Misaar.Abstract;
using Misaar.Models;

namespace Misaar.Concrete
{
    public class PictureRepository : IFileRepository<Picture>
    {
        private ApplicationDbContext db;

        public PictureRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(Picture item)
        {
            db.Pictures.Add(item);
        }

        public void Delete(Picture obj)
        {
            db.Pictures.Remove(obj);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Picture Get(int? id)
        {
            return db.Pictures.Find(id);
        }

    }
}