namespace PipelineHandlers;

public class BaseLogic<TEntity> : ILogic<TEntity> where TEntity : class
{
    private TryProcess<TEntity>? _process;

    public void SetupRun(TryProcess<TEntity> process)
    {
        _process = process;
    }

    public bool TryRun(TEntity entity, out TEntity? output)
    {
        output = entity;

        var result = _process?.Invoke(entity, out output);
        return result ?? false;
    }
}