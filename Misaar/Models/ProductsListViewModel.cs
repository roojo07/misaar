using System.Collections.Generic;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public SelectList Categories { get; set; }
    }
}