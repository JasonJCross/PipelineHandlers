namespace PipelineHandlers;

public interface IHandler<TEntity> where TEntity : class
{
    IHandler<TEntity> SetNext(IHandler<TEntity> handler);
    IProcess<TEntity> Handle(IProcess<TEntity> process);
}