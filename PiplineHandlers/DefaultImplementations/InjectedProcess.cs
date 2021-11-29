namespace PipelineHandlers;

public class InjectedProcess<TEntity, TProcess> : IProcess<TEntity>
    where TEntity : class
    where TProcess : ILogic<TEntity>
{
    private readonly TProcess _process;

    public InjectedProcess(TProcess process)
    {
        _process = process;
    }

    public bool Continue { get; set; } = true;
    public int Priority { get; set; } = 0;
    public TEntity Process(TEntity entity)
    {
        Continue = _process.TryRun(entity, out var output);

        return output ?? entity;
    }
}
