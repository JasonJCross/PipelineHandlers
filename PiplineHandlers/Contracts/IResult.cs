namespace PipelineHandlers;
public interface IResult<TEntity> where TEntity : class
{
    bool IsSuccess { get; }
    string Message { get; }
    TEntity Value { get; }
}
