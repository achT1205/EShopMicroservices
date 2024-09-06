namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);
internal class GetProductByCategoryQueryHandler(IDocumentSession session)
    : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(_ => _.Category.Contains(query.Category)).ToListAsync();
        if (products == null)
        {
            throw new ProductNotFoundException("Product not found!");
        }
        return new GetProductByCategoryResult(products);
    }
}