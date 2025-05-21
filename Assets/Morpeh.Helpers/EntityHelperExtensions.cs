namespace Scellecs.Morpeh.Helpers {
    using System;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public static class EntityHelperExtensions {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Exists(this Entity entity) {
            return !entity.GetWorld().IsDisposed(entity);
        }

        [PublicAPI]
        [Obsolete("[MORPEH] Use Stash.AddOrGet() instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddOrGet<T>(this Entity entity)
                where T : struct, IComponent {
#if MORPEH_DEBUG
            if (entity.IsNullOrDisposed()) {
                InvalidAddOperationException.ThrowDisposedEntity(entity, typeof(T));
            }
#endif

            return ref entity.GetWorld().GetStash<T>().AddOrGet(entity);
        }
    }
}