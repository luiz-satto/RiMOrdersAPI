using Domain.Exceptions.Base;

namespace Application.Exceptions;

public sealed class ValidationException : BadRequestException
{
    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation errors ocurred") =>
        Errors = errors;

    public Dictionary<string, string[]> Errors { get; }
}
