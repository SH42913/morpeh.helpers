namespace Morpeh.Helpers {
    using System.Runtime.CompilerServices;

    public abstract class SimpleUpdateSystem<T> : UpdateSystem where T : struct, IComponent {
        protected Filter filter;
        protected bool inAwake;

        public override void OnAwake() {
            inAwake = true;
            filter = World.Filter.With<T>();
            OnUpdate(0f);
            inAwake = false;
        }

        public override void OnUpdate(float deltaTime) {
            Filter.ComponentsBag<T> components = filter.Select<T>();
            for (int i = 0, length = filter.Length; i < length; i++) {
                Process(filter.GetEntity(i), ref components.GetComponent(i), deltaTime);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T component, in float deltaTime);
    }

    public abstract class SimpleUpdateSystem<T1, T2> : UpdateSystem where T1 : struct, IComponent
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
            foreach (Entity ent in filter) {
                ref T1 first = ref ent.GetComponent<T1>();
                ref T2 second = ref ent.GetComponent<T2>();
                Process(ent, ref first, ref second, deltaTime);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Process(Entity entity, ref T1 first, ref T2 second, in float deltaTime);
    }
}