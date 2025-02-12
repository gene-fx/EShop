using FluentValidation;

namespace OrderingApplication.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("OrderId must be informed");
    }
}