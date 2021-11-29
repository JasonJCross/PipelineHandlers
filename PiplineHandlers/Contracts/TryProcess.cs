namespace PipelineHandlers;

public delegate bool TryProcess<TEntity>(TEntity entity, out TEntity output) where TEntity : class;