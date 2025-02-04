namespace OrderingDomain.Exception;

public class DomainException : ApplicationException
{
    public DomainException(string msg) :
        base($"Domain Exception: \"{msg}\" throws from Domain Layer")
    {
    }
}
