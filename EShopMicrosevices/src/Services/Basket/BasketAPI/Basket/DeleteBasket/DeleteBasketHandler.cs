namespace BasketAPI.Basket.DeleteBasket
{
    /// <summary>
    /// This method is handling the delete commando by using 
    /// the IUnityOfWork not as an injected service,
    /// but requesting when excuting the task because it is
    /// a singleton service and it cannot consume a scoped sevice
    /// </summary>
    /// <param name="unityOfWork"></param>
    public record DeleteBasketCommand(string UserName) 
        : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);

    internal class DeleteBasketCommandHandler
        (IServiceScopeFactory serviceScopeFactory)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnityOfWork>();

            var cart = await unityOfWork.BasketRepository.Get(x => x.UserName == command.UserName);

            unityOfWork.BasketRepository.Delete(cart);

            await unityOfWork.Commit(cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
