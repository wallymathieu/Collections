# Collections

## Functionality

Compare two dictionaries in order to be able to merge incoming data easily.

```c#
using WallyMathieu.Collections;
...
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
            foreach (var outgoing in comparison.Minus)
            {
                db.Remove(outgoing);
            }
            await db.SaveChangesAsync();
...
```
