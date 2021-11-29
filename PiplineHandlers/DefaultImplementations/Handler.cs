namespace PipelineHandlers;

public class Handler<TEntity> : IHandler<TEntity> where TEntity : class
{
    private IHandler<TEntity>? NextHandler { get; set; }

    public IHandler<TEntity> SetNext(IHandler<TEntity> handler)
    {
        NextHandler = handler;
        return NextHandler;
    }

    public IProcess<TEntity> Handle(IProcess<TEntity> process)
    {
        if (NextHandler == null)
        {
            return process;
        }

        return process.Continue ? NextHandler.Handle(process) : process;
    }
}