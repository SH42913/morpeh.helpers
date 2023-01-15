namespace Scellecs.Morpeh.Helpers {
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    public static class FilterHelperExtensions {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveComponentForAll<T>(this Filter filter)
                where T : struct, IComponent {
            foreach (Entity ent in filter) {
                ent.RemoveComponent<T>();
            }
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllEntities(this Filter filter, World world) {
            foreach (Entity ent in filter) {
                world.RemoveEntity(ent);
            }
        }
    }
}