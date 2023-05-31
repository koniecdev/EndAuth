﻿using FluentValidation.Results;

namespace EndAuth.Application.Common.Exceptions;
public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this(new[] {error})
    {
    }
}