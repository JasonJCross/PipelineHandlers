namespace PipelineHandlers;

public interface IProcess<TEntity> where TEntity : class
{
    bool Continue { get; set; }
    int Priority { get; }
    TEntity Process(TEntity data);
}