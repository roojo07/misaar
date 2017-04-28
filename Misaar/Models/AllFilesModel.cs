using System.Collections.Generic;

namespace Misaar.Models
{
    public class AllFilesModel
    {
       public IEnumerable<Category> Categories { get; set; }
       public IEnumerable<Article> Articles { get; set; }
       public IEnumerable<Product> Products { get; set; }
    }
}