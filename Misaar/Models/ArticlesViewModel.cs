using System.Collections.Generic;
using System.Web.Mvc;

namespace Misaar.Models
{
    public class ArticlesViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public SelectList Categories { get; set; }
    }
}