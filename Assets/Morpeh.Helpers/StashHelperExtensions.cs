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
                throw new Exception($"[MORPEH] You are trying Add on null or disposed entity");
            }
#endif
            if (stash.components.Add(entity.entityId.id, default, out int slotIndex)) {
                entity.AddTransfer(stash.typeId, stash.offset);
                return ref stash.components.data[slotIndex];
            }

#if MORPEH_DEBUG
            MLogger.LogError($"You're trying to add on entity {entity.entityId.id} a component that already exists! Use Get or Set instead!");
#endif
            return ref stash.components.GetValueRefByKey(entity.entityId.id);
        }
    }
}