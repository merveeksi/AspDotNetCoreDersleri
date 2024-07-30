using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsApp.Models
{
    public class ProductViewModel : IEnumerable
    {
        public List<Product> Product { get; set; } =null!;
        public List<Category> Categories { get; set; } = null!;

        public string? SelectedCategory { get; set; }
        public List<Product> Products { get; set; }
        public string ProductId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}