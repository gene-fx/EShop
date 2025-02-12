using FluentValidation;

namespace OrderingApplication.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Order Id is required");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is Required");
        RuleFor(x => x.Order.CustomerID).NotEmpty().WithMessage("Customer Id is required for any Order");
    }
}