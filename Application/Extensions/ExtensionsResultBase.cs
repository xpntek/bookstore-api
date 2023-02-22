using FluentResults;

namespace Application.Extensions;

public static class ExtensionsResultBase
{
    public static TResult To<TResult>(this ResultBase resultBase)
        where TResult : ResultBase, new()
    {
        var result = new TResult();

        result.Errors.AddRange(resultBase.Errors);
        result.Successes.AddRange(resultBase.Successes);
        result.Reasons.AddRange(resultBase.Reasons);
        
        return result;
    }
}