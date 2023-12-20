namespace RapidBlazor.WebUi.Shared.Common;

public class Result
{
    protected Result()
    {
        
    }
    
    public bool Succeeded { get; protected set; } = true;

    public IEnumerable<string> Errors { get; protected set; } = new List<string>();

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result { Succeeded = false, Errors = errors };
    }
}

public class Result<T> : Result
{
    public T? Value { get; private set; }

    public static Result<T> Success(T? value)
    {
        return new Result<T> { Value = value };
    }

    public new static Result<T> Failure(IEnumerable<string> errors)
    {
        return new Result<T> { Succeeded = false, Errors = errors };
    }
}
