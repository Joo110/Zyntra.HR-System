namespace HRMS.Domain.ValueObjects;
public record Money(decimal Amount, string Currency = "USD")
{
    public static Money Zero(string currency = "USD") => new(0, currency);
    public Money Add(Money other) => new(Amount + other.Amount, Currency);
    public Money Subtract(Money other) => new(Amount - other.Amount, Currency);
    public override string ToString() => $"{Amount:N2} {Currency}";
}
