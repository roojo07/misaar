using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Misaar.Abstract
{
    public interface IRepository<T>:IDisposable where T : class
    {        
        Task<IEnumerable<T>> GetAll();        
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        Task<T> Get(int? id);
       
    }
}
