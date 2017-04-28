using Misaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaar.Abstract
{
    public interface IFileRepository<T> : IDisposable where T : class
    {
        void Delete(T obj);
        void Create(T item);               
        T Get(int? id);
    }
}
