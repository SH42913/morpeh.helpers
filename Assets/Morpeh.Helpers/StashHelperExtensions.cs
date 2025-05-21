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

            if (stash.world.IsDisposed(entity)) {
                InvalidAddOperationException.ThrowDisposedEntity(entity, stash.Type);
            }

            if (stash.map.TryGetIndex(entity.Id, out var slotIndex)) {
                return ref stash.data[slotIndex];
            } else {
                slotIndex = stash.map.TakeSlot(entity.Id, out var resized);

                if (resized) {
                    ArrayHelpers.GrowNonInlined(ref stash.data, stash.map.capacity);
#if MORPEH_DEBUG
                    stash.world.newMetrics.stashResizes++;
#endif
                }

                var info = ComponentId<T>.info;
                stash.data[slotIndex] = default;
                stash.world.TransientChangeAddComponent(entity.Id, ref info);
            }

            return ref stash.data[slotIndex];
        }
    }
}