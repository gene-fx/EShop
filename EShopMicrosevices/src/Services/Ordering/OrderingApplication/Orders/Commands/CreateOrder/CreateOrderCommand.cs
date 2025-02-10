using FluentValidation;

namespace OrderingApplication.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

//Validations Class
public class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is Required");
        RuleFor(x => x.Order.CustomerID).NotEmpty().WithMessage("OrderCustomerID is Required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}




