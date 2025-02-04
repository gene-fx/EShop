namespace OrderingDomain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;

    public string LastName { get; } = default!;

    public string EmailAddress { get; } = default!;

    public string AddressLine { get; } = default!;

    public string Country { get; } = default!;

    public string State { get; } = default!;

    public string ZipCode { get; } = default!;

    protected Address()
    {
    }

    private Address(string firstName, string lastName, string emailAdress,
        string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAdress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string emailAdress,
        string addressLine, string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAdress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        return new Address(firstName, lastName, emailAdress, addressLine, country, state, zipCode);
    }
}
