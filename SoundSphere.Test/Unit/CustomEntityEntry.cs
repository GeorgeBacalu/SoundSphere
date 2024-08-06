using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SoundSphere.Test.Unit
{
    public class CustomEntityEntry<T> : EntityEntry<T> where T : class
    {
        public CustomEntityEntry() : base(null) { }
    }
}