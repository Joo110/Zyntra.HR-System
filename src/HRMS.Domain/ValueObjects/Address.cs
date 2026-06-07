namespace HRMS.Domain.ValueObjects;
public record Address(string Street, string City, string State, string Country, string? PostalCode = null)
{
    public override string ToString() => $"{Street}, {City}, {State}, {Country}";
}
