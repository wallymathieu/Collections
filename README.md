# Collections

## Functionality

### SymmetricDiff

Compare two dictionaries in order to be able to merge incoming data easily.

```c#
using WallyMathieu.Collections;
...
    public async Task<string> Post(IEnumerable<ProductModel> body)
    {
        var comparison = db.Products.ToDictionary(c => c.Key)
                                .SymmetricDiff(body.ToDictionary(c => c.Id));
        foreach (var valueIntersection in comparison.Intersection)
        {
            valueIntersection.Left.Description = valueIntersection.Right.Name;
        }
        foreach (var (key,incoming) in comparison.OnlyInRight)
        {
            db.Add(new Product
            {
                Key = incoming.Id,
                Description = incoming.Name
            });
        }
        foreach (var (key,outgoing) in comparison.OnlyInLeft)
        {
            db.Remove(outgoing);
        }
        await db.SaveChangesAsync();
...
```

Compare two hash sets in order to quickly understand what's incoming and what's outgoing.

```c#
using WallyMathieu.Collections;
...
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
...
```

### BatchesOf

In order to get subsets of a list with a specific number of elements.

```c#
using WallyMathieu.Collections;
...
foreach (var batch in hugelist.BatchesOf(2000))
{
    await Process(batch);
    await db.SaveChangesAsync();
}
...
```

### Pairwise

Used to iterate over collection and get the collection elements pairwise.
Yields the result of the application of the map function over each pair.

```c#
using WallyMathieu.Collections;
...
var pairs = Enumerable.Range(0,4).Pairwise(Tuple.Create).ToArray(); 
// will be
Assert.Equal(new[] { Tuple.Create(0, 1), Tuple.Create(1, 2), Tuple.Create(2, 3) },pairs);
```