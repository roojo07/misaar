using Misaar.Models;
using System;
using System.Threading.Tasks;

namespace Misaar.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ProductRepository prodRepo;
        private FileRepository fileRepo;
        private ArticleRepository artRepo;
        private PictureRepository picRepo;
        private CategoryRepository catRepo;
        private ContactRepository contRepo;
        private PhoneRepository phoneRepo;
        private SeoRepository metaRepo;

        public ProductRepository Products
        {
            get
            {
                if (prodRepo == null)
                    prodRepo = new ProductRepository(db);
                return prodRepo;
            }
        }

        public FileRepository Files
        {
            get
            {
                if (fileRepo == null)
                    fileRepo = new FileRepository(db);
                return fileRepo;
            }
        }

        public ArticleRepository Articles
        {
            get
            {
                if (artRepo == null)
                    artRepo = new ArticleRepository(db);
                return artRepo;
            }
        }

        public PictureRepository Pictures
        {
            get
            {
                if (picRepo == null)
                    picRepo = new PictureRepository(db);
                return picRepo;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (catRepo == null)
                    catRepo = new CategoryRepository(db);
                return catRepo;
            }
        }

        public ContactRepository Contacts
        {
            get
            {
                if (contRepo == null)
                    contRepo = new ContactRepository(db);
                return contRepo;
            }
        }

        public PhoneRepository Phones
        {
            get
            {
                if (phoneRepo == null)
                    phoneRepo = new PhoneRepository(db);
                return phoneRepo;
            }
        }

        public SeoRepository SeoTags
        {
            get
            {
                if (metaRepo == null)
                    metaRepo = new SeoRepository(db);
                return metaRepo;
            }
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}