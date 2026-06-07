namespace HRMS.Domain.ValueObjects;
public record EmailAddress
{
    public string Value { get; }
    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email cannot be empty");
        if (!value.Contains('@')) throw new ArgumentException("Invalid email format");
        Value = value.ToLowerInvariant().Trim();
    }
    public override string ToString() => Value;
}
