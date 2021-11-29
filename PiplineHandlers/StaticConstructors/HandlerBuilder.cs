using System.Collections.Generic;

namespace PipelineHandlers;
public static class DefaultHandlerBuilder
{
    public static IHandler<TEntity>? Build<TEntity>(IEnumerable<IProcess<TEntity>> processes) where TEntity : class
    {
        var result = new List<IHandler<TEntity>>();

        // Setup the handler chain

        return null;
    }

}
