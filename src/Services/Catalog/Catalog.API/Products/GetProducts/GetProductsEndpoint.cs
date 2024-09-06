﻿
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetPRoductsResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ( [AsParameters] GetProductsRequest request, ISender sender) =>
        {
            GetProductsQuery query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);

            var response = result.Adapt<GetPRoductsResponse>();
            return Results.Ok(response);
        })
     .WithName("GetProducts")
     .Produces<CreateProductResponse>(StatusCodes.Status201Created)
     .ProducesProblem(StatusCodes.Status400BadRequest)
     .WithSummary("Get Products")
     .WithDescription("Get Products");
    }
}