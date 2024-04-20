
using CatalogAPI.Products.GetProductByName;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProcuctResult>;

    public record UpdateProcuctResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(model => model.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(model => model.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(model => model.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(model => model.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(model => model.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(model => model.Price).NotEmpty().WithMessage("Price is required");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProcuctResult>
    {
        public async Task<UpdateProcuctResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"UpdateProductCommandHandler.Handle called with {command}");

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProcuctResult(true);
        }
    }
}
