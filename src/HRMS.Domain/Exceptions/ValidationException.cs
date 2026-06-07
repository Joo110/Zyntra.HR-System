namespace HRMS.Domain.Exceptions;
public class DomainValidationException : DomainException
{
    public IEnumerable<string> Errors { get; }
    public DomainValidationException(string message, IEnumerable<string> errors) : base(message) { Errors = errors; }
}
