using BuildingBlocks.CQRS;
using Catalog.API.Models;


namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(Guid Id, string name, List<string> category, string description, string imageFile, decimal price)
: ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.name,
            Description = command.description,
            Category = command.category,
            ImageFile = command.imageFile,
            Price = command.price,
        };

        return new CreateProductResult(Guid.NewGuid());
    }
}
