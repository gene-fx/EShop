using FluentValidation;

namespace OrderingApplication.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId)
    : IQuery<GetOrdersByCustomerResult>;

public record GetOrdersByCustomerResult(IReadOnlyCollection<OrderDto> Orders);

public class GetOrdersByCustomerValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("A valid Custormer Id must be informed");
    }
}