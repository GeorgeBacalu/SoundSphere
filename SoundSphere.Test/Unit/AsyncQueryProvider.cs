using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace SoundSphere.Test.Unit
{
    public class AsyncQueryProvider<T> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public AsyncQueryProvider(IQueryProvider inner) => _inner = inner;

        public IQueryable CreateQuery(Expression expression) => new AsyncQueryable<T>(expression);

        public IQueryable<U> CreateQuery<U>(Expression expression) => new AsyncQueryable<U>(expression);

        public object? Execute(Expression expression) => _inner.Execute(expression);

        public V Execute<V>(Expression expression) => _inner.Execute<V>(expression);

        public V ExecuteAsync<V>(Expression expression, CancellationToken cancellationToken)
        {
            var expectedResultType = typeof(V).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                .GetMethod(nameof(IQueryProvider.Execute), genericParameterCount: 1, types: new[] { typeof(Expression) })
                ?.MakeGenericMethod(expectedResultType)
                ?.Invoke(this, new[] { expression });
            return (V?)typeof(Task)
                .GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(expectedResultType)
                ?.Invoke(null, new[] { executionResult })
                ?? throw new InvalidOperationException("The result of Invoke is null and cannot be cast to Task<T>.");
        }
    }
}