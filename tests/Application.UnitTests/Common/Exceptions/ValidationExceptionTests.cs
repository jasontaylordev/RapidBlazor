using FluentValidation.Results;
using RapidBlazor.Application.Common.Exceptions;

namespace RapidBlazor.Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreateAnEmptyErrorDictionary()
    {
        var actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failure = new List<ValidationFailure> { new ValidationFailure("Age", "must be over 18"), };

        var actual = new ValidationException(failure).Errors;

        actual.Keys.Should().BeEquivalentTo(new[] { "Age" });
        actual["Age"].Should().BeEquivalentTo(new[] { "must be over 18" });
    }
    
    [Fact]
    public void MultipleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Age", "must be 18 or older"),
            new ValidationFailure("Age", "must be 25 or younger"),
            new ValidationFailure("Password", "must contain at least 8 characters"),
            new ValidationFailure("Password", "must contain a digit"),
            new ValidationFailure("Password", "must contain upper case letter"),
            new ValidationFailure("Password", "must contain lower case letter"),
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        actual["Age"].Should().BeEquivalentTo(new string[]
        {
            "must be 25 or younger",
            "must be 18 or older",
        });

        actual["Password"].Should().BeEquivalentTo(new string[]
        {
            "must contain lower case letter",
            "must contain upper case letter",
            "must contain at least 8 characters",
            "must contain a digit",
        });
    }
}
