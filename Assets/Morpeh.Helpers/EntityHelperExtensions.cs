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
        [Obsolete("Use " + nameof(AddOrGet) + " instead")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetOrCreate<T>(this Entity entity)
                where T : struct, IComponent {
            if (entity.Has<T>()) {
                return ref entity.GetComponent<T>();
            }

            return ref entity.AddComponent<T>();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddOrGet<T>(this Entity entity)
                where T : struct, IComponent {
#if MORPEH_DEBUG
            if (entity.IsNullOrDisposed()) {
                throw new System.Exception($"[MORPEH] You are trying {nameof(AddOrGet)} on null or disposed entity {entity.entityId.id}");
            }
#endif

            return ref entity.world.GetStash<T>().AddOrGet(entity);
        }
    }
}