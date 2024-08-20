namespace SoundSphere.Test.Unit
{
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        public readonly IEnumerator<T> _inner;

        public AsyncEnumerator(IEnumerator<T> inner) => _inner = inner;

        public ValueTask DisposeAsync() => new ValueTask(Task.Run(() => _inner.Dispose()));

        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());

        public T Current => _inner.Current;
    }
}