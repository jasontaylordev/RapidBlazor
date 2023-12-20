namespace RapidBlazor.Domain.UnitTests.Common;

public class ValueObject
{
    
    [Fact]
    public void ShouldEqualDifferentObjectWithSameValue()
    {
        var left = new TestValueObject("obj");
        var right = new TestValueObject("obj");
        var result = left.Equals(right);

        result.Should().BeTrue();
    }

    [Fact]
    public void ShouldNotEqualDifferentObjectOfSameTypeIfOneIsNull()
    {
        TestValueObject left = new TestValueObject("obj");
        TestValueObject? right = null;
        var result = left.Equals(right);

        result.Should().BeFalse();
    }
    
    [Fact]
    public void ShouldNotEqualDifferentObject()
    {
        TestValueObject left = new TestValueObject("obj");
        object right = new();
        var result = left.Equals(right);

        result.Should().BeFalse();
    }

}

public class TestValueObject : Domain.Common.ValueObject
{
    public string Code { get; }

    public TestValueObject(string code)
    {
        Code = code;
    }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Code;
    }
}
