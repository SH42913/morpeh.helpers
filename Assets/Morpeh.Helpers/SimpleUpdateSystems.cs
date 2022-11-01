namespace Morpeh.Helpers {
    using System.Runtime.CompilerServices;

    public abstract class SimpleUpdateSystem : UpdateSystem {
        protected bool inAwake;
        private Filter filter;

        public override void OnAwake() {
            inAwake = true;
            InitCache();
            filter = BuildFilter();
            OnUpdate(0f);
            inAwake = false;
        }

        public override void OnUpdate(float deltaTime) {
            if (filter.IsEmpty()) {
                return;
            }

            foreach (Entity ent in filter) {
                Process(ent, deltaTime);
            }
        }

        protected abstract Filter BuildFilter();
        protected abstract void InitCache();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity ent, in float deltaTime);
    }

    public abstract class SimpleUpdateSystem<T> : SimpleUpdateSystem
            where T : struct, IComponent {
        private ComponentsCache<T> cache;

        protected override void InitCache() {
            cache = World.GetCache<T>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent, ref cache.GetComponent(ent), deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T component, in float deltaTime);
    }

    public abstract class SimpleUpdateSystem<T1, T2> : SimpleUpdateSystem
            where T1 : struct, IComponent
            where T2 : struct, IComponent {
        private ComponentsCache<T1> cache1;
        private ComponentsCache<T2> cache2;

        protected override void InitCache() {
            cache1 = World.GetCache<T1>();
            cache2 = World.GetCache<T2>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T1>().With<T2>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent, ref cache1.GetComponent(ent), ref cache2.GetComponent(ent), deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, in float deltaTime);
    }

    public abstract class SimpleUpdateSystem<T1, T2, T3> : SimpleUpdateSystem
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent {
        private ComponentsCache<T1> cache1;
        private ComponentsCache<T2> cache2;
        private ComponentsCache<T3> cache3;

        protected override void InitCache() {
            cache1 = World.GetCache<T1>();
            cache2 = World.GetCache<T2>();
            cache3 = World.GetCache<T3>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T1>().With<T2>().With<T3>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent,
                    ref cache1.GetComponent(ent),
                    ref cache2.GetComponent(ent),
                    ref cache3.GetComponent(ent),
                    deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, ref T3 third, in float deltaTime);
    }
}