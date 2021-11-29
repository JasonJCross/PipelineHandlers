namespace PipelineHandlers;

public interface ILogic<TEntity> where TEntity : class
{
    void SetupRun(TryProcess<TEntity> process);
    bool TryRun(TEntity entity, out TEntity? output);
}