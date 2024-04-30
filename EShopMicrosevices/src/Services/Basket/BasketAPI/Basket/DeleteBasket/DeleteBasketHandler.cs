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

    public class DeleteBasketCommandHandler(IUnityOfWork unityOfWork)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {

            //TODO: CORRECT THE LOGGIN AND VALIDATION LIFETIME THAT IS LEADIN TO THE NEXT ERROR:
            //? System.InvalidOperationException: Cannot resolve scoped service 'System.Collections.Generic.IEnumerable`1[MediatR.IPipelineBehavior`2[BasketAPI.Basket.DeleteBasket.DeleteBasketCommand,BasketAPI.Basket.DeleteBasket.DeleteBasketResult]]' from root provider.
            return new DeleteBasketResult(true);
        }
    }
}