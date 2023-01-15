namespace Scellecs.Morpeh.Helpers {
    using System;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public static class EntityHelperExtensions {
        [PublicAPI]
        [Obsolete("Use " + nameof(EntityExtensions.IsNullOrDisposed))]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Exists(this Entity entity) {
            return entity != null && !entity.IsDisposed();
        }

        [PublicAPI]
        [Obsolete("Use " + nameof(EntityExtensions.AddComponent) + " with out bool exist argument")]
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