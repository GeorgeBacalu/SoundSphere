namespace SoundSphere.Test.Unit
{
    public interface IAsyncQueryable<T> : IQueryable<T>, IAsyncEnumerable<T> { }
}