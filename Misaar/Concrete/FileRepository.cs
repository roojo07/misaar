using Misaar.Abstract;
using Misaar.Models;

namespace Misaar.Concrete
{
    public class FileRepository : IFileRepository<File>
    {
        private ApplicationDbContext db;

        public FileRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public void Create(File item)
        {
            db.Files.Add(item);
        }

        public void Delete(File obj)
        {
            db.Files.Remove(obj);
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public File Get(int? id)
        {
            return db.Files.Find(id);
        }
    }
}