namespace Scellecs.Morpeh.Helpers {
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;
    using Systems;

    public abstract class SimpleFixedUpdateSystem : FixedUpdateSystem {
        [PublicAPI] protected bool inAwake;
        private Filter filter;

        public override void OnAwake() {
            inAwake = true;
            InitStashes();
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
        protected abstract void InitStashes();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity ent, in float deltaTime);
    }

    public abstract class SimpleFixedUpdateSystem<T> : SimpleFixedUpdateSystem
            where T : struct, IComponent {
        private Stash<T> stash;

        protected override void InitStashes() {
            stash = World.GetStash<T>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent, ref stash.Get(ent), deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T component, in float deltaTime);
    }

    public abstract class SimpleFixedUpdateSystem<T1, T2> : SimpleFixedUpdateSystem
            where T1 : struct, IComponent
            where T2 : struct, IComponent {
        private Stash<T1> stash1;
        private Stash<T2> stash2;

        protected override void InitStashes() {
            stash1 = World.GetStash<T1>();
            stash2 = World.GetStash<T2>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T1>().With<T2>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent, ref stash1.Get(ent), ref stash2.Get(ent), deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, in float deltaTime);
    }

    public abstract class SimpleFixedUpdateSystem<T1, T2, T3> : SimpleFixedUpdateSystem
            where T1 : struct, IComponent
            where T2 : struct, IComponent
            where T3 : struct, IComponent {
        private Stash<T1> stash1;
        private Stash<T2> stash2;
        private Stash<T3> stash3;

        protected override void InitStashes() {
            stash1 = World.GetStash<T1>();
            stash2 = World.GetStash<T2>();
            stash3 = World.GetStash<T3>();
        }

        protected override Filter BuildFilter() {
            return World.Filter.With<T1>().With<T2>().With<T3>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Process(Entity ent, in float deltaTime) {
            Process(ent, ref stash1.Get(ent), ref stash2.Get(ent), ref stash3.Get(ent), deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, ref T3 third, in float deltaTime);
    }
}