namespace Scellecs.Morpeh.Helpers {
    using System.Runtime.CompilerServices;

    public static class EntityHelperExtensions {
        [System.Obsolete("Use " + nameof(EntityExtensions.IsNullOrDisposed))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Exists(this Entity entity) {
            return entity != null && !entity.IsDisposed();
        }

        [System.Obsolete("Use " + nameof(EntityExtensions.AddComponent) + " with out bool exist argument")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetOrCreate<T>(this Entity entity)
                where T : struct, IComponent {
            if (entity.Has<T>()) {
                return ref entity.GetComponent<T>();
            }

            return ref entity.AddComponent<T>();
        }
    }
}