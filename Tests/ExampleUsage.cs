using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallyMathieu.Collections;

namespace Tests
{
    public class ExampleUsage
    {
        private readonly IProductDb db;
        private readonly IPublisher publisher;

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
            void Remove(Product product);
            IQueryable<int> PublishedProducts { get; }
        }

        public interface IPublisher
        {
            Task Publish(Product product);
            Task UnPublish(Product product);
        }
        public ExampleUsage(IProductDb db)
        {
            this.db = db;
        }

        public async Task<string> Post(IEnumerable<ProductModel> body)
        {
            var comparison = db.Products.ToDictionary(c => c.Key)
                                    .SymmetricDiff(body.ToDictionary(c => c.Id));
            foreach (var valueIntersection in comparison.Intersection)
            {
                valueIntersection.Left.Description = valueIntersection.Right.Name;
            }
            foreach (var (_,incoming) in comparison.OnlyInRight)
            {
                db.Add(new Product
                {
                    Key = incoming.Id,
                    Description = incoming.Name
                });
            }
            foreach (var (_,outgoing) in comparison.OnlyInLeft)
            {
                db.Remove(outgoing);
            }
            await db.SaveChangesAsync();
            return "Changed and created and deleted products";
        }


        public class PublishedProductsModel
        {
            public int[] ProductIds { get; set; }
        }
        
        public async Task Post(PublishedProductsModel body)
        {
            var comparison = db.PublishedProducts.ToHashSet()
                                .SymmetricDiff(body.ProductIds.ToHashSet());

            foreach (var product in db.Products.Where(p=>comparison.OnlyInRight.Contains(p.Key)))
            {
                await publisher.Publish(product);
            }

            foreach (var product in db.Products.Where(p=>comparison.OnlyInLeft.Contains(p.Key)))
            {
                await publisher.UnPublish(product);
            }
        }
    }
}
