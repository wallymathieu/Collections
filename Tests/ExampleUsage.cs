using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallyMathieu.Collections;

namespace Tests
{
    public class ExampleUsage
    {
        private readonly IProductDb db;

        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Product
        {
            public int Key { get; set; }
            public string Description { get; set; }
        }
        public interface IProductDb
        {
            IQueryable<Product> Products { get; }
            Task<int> SaveChangesAsync();
            void Add(Product product);
        }

        public ExampleUsage(IProductDb db)
        {
            this.db = db;
        }

        public async Task<string> Post(IEnumerable<ProductModel> body)
        {
            var comparison = db.Products.ToDictionary(c => c.Key)
                                    .Compare(body.ToDictionary(c => c.Id));
            foreach (var valueIntersection in comparison.Intersection)
            {
                valueIntersection.Left.Description = valueIntersection.Right.Name;
            }
            foreach (var incoming in comparison.Plus)
            {
                db.Add(new Product
                {
                    Key = incoming.Id,
                    Description = incoming.Name
                });
            }
            await db.SaveChangesAsync();
            return "Changed and created products";
        }
    }
}
