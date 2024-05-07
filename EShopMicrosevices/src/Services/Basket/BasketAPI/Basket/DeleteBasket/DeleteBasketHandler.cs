namespace BasketAPI.Basket.DeleteBasket
{
    /// <summary>
    /// This method handles the delete command using the 
    /// IServiceScopeFactore interface to create an instance 
    /// of the services registered in the container in a scoped
    /// context, therefore allowing a scoped service to be called 
    /// in a singleton service.
    /// ICommandHandler = SINGLETON
    /// IUnityOfWork = SCOPED
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    public record DeleteBasketCommand(string UserName) 
        : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValtidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValtidator()
        {
            RuleFor(model => model.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName is required");
        }
    }

    internal class DeleteBasketCommandHandler
        (IServiceScopeFactory serviceScopeFactory)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            var basketRepository = scope.ServiceProvider.GetRequiredService<IBasketRepository>();

            if(await basketRepository.Get(command.UserName) is null) 
            {
                throw new BasketNotFoundException(command.UserName);
            }

            if(await basketRepository.Delete(command.UserName, cancellationToken))
            {
                await basketRepository.Commit(cancellationToken);

                return new DeleteBasketResult(true);
            }

            return new DeleteBasketResult(false);
        }
    }
}
