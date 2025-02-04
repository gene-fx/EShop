namespace BuildingBlocks.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string UserName) : base(UserName)
    {

    }
}
