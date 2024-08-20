using System.Linq.Expressions;

namespace SoundSphere.Test.Unit
{
    public class AsyncQueryable<T> : EnumerableQuery<T>, IAsyncQueryable<T>
    {
        public AsyncQueryable(IEnumerable<T> enumerable) : base(enumerable) { }

        public AsyncQueryable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());

        IQueryProvider IQueryable.Provider => new AsyncQueryProvider<T>(this);
    }
}