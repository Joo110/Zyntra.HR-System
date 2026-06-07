namespace HRMS.Domain.ValueObjects;
public record PhoneNumber
{
    public string Value { get; }
    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Phone cannot be empty");
        Value = value.Trim();
    }
    public override string ToString() => Value;
}
