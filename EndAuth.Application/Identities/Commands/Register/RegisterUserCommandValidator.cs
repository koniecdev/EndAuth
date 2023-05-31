﻿using EndAuth.Shared.Identities.Commands.Register;
using FluentValidation;

namespace EndAuth.Application.Identities.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(m => m.Email).EmailAddress();
        RuleFor(m => m.Username).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(m => m.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}