namespace Morpeh.Helpers {
    using System.Runtime.CompilerServices;

    public abstract class SimpleFixedUpdateSystem<T> : FixedUpdateSystem where T : struct, IComponent {
        protected Filter filter;
        protected bool inAwake;

        public override void OnAwake() {
            inAwake = true;
            filter = World.Filter.With<T>();
            OnUpdate(0f);
            inAwake = false;
        }

        public override void OnUpdate(float deltaTime) {
            if (filter.IsEmpty()) {
                return;
            }

            ComponentsCache<T> cache = World.GetCache<T>();
            foreach (Entity ent in filter) {
                Process(ent, ref cache.GetComponent(ent), deltaTime);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T component, in float deltaTime);
    }

    public abstract class SimpleFixedUpdateSystem<T1, T2> : FixedUpdateSystem where T1 : struct, IComponent
                                                                              where T2 : struct, IComponent {
        protected Filter filter;
        protected bool inAwake;

        public override void OnAwake() {
            inAwake = true;
            filter = World.Filter.With<T1>().With<T2>();
            OnUpdate(0f);
            inAwake = false;
        }

        public override void OnUpdate(float deltaTime) {
            if (filter.IsEmpty()) {
                return;
            }

            ComponentsCache<T1> cache1 = World.GetCache<T1>();
            ComponentsCache<T2> cache2 = World.GetCache<T2>();
            foreach (Entity ent in filter) {
                Process(ent, ref cache1.GetComponent(ent), ref cache2.GetComponent(ent), deltaTime);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, in float deltaTime);
    }
}