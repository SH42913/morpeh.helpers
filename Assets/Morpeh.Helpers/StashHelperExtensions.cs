namespace Scellecs.Morpeh.Helpers {
    using System.Runtime.CompilerServices;
    using Collections;
    using JetBrains.Annotations;

    public static class StashHelperExtensions {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddOrGet<T>(this Stash<T> stash, Entity entity)
                where T : struct, IComponent {
            stash.world.ThreadSafetyCheck();

#if MORPEH_DEBUG
            if (entity.IsNullOrDisposed()) {
                throw new System.Exception($"[MORPEH] You are trying {nameof(AddOrGet)} on null or disposed entity {entity.entityId.id}");
            }
#endif

            if (stash.components.Add(entity.entityId.id, default, out int slotIndex)) {
                entity.AddTransfer(stash.typeId);
                return ref stash.components.data[slotIndex];
            }

            return ref stash.components.GetValueRefByKey(entity.entityId.id);
        }
    }
}