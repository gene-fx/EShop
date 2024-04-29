 namespace BasketAPI.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName)
        : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool Success);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().NotNull().WithMessage("UserName is requirede");
        }
    }

    public class DeleteBasketCommandHandler(ILogger<DeleteBasketCommandHandler> logger)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //TODO: implement the logic within dbconnection            

            return new DeleteBasketResult(true);
        }
    }
}
