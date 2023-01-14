namespace Scellecs.Morpeh.Helpers.OneFrame {
    using System;
    using Collections;

    internal sealed class OneFrameRegister : IDisposable {
        private static readonly IntHashMap<OneFrameRegister> REGISTRIES = new IntHashMap<OneFrameRegister>();

        private readonly World world;
        private ICanClean[] oneFrameFilters;
        private int registeredFilters;

        public static OneFrameRegister GetFor(World world) {
            if (REGISTRIES.TryGetValue(world.identifier, out OneFrameRegister register)) {
                return register;
            }

            register = new OneFrameRegister(world);
            REGISTRIES.Add(world.identifier, register, out _);
            return register;
        }

        public static void RegisterOneFrame<TEvent>(World world)
                where TEvent : struct, IComponent {
            GetFor(world).RegisterOneFrame<TEvent>();
        }

        private OneFrameRegister(World world) {
            this.world = world;

            oneFrameFilters = new ICanClean[1];
            registeredFilters = 0;
        }

        private void RegisterOneFrame<T>()
                where T : struct, IComponent {
            for (var i = 0; i < registeredFilters; i++) {
                if (oneFrameFilters[i].GetInnerType() == typeof(T)) {
                    return;
                }
            }

            if (registeredFilters >= oneFrameFilters.Length) {
                Array.Resize(ref oneFrameFilters, oneFrameFilters.Length << 1);
            }

            oneFrameFilters[registeredFilters++] = new OneFrameFilter<T>(world);
        }

        public void CleanOneFrameEvents() {
            for (var i = 0; i < registeredFilters; i++) {
                oneFrameFilters[i].Clean();
            }
        }

        public void Dispose() {
            CleanOneFrameEvents();
            REGISTRIES.Remove(world.identifier, out _);
        }

        private sealed class OneFrameFilter<T> : ICanClean
                where T : struct, IComponent {
            private readonly Filter filter;

            public OneFrameFilter(World world) {
                filter = world.Filter.With<T>();
            }

            public void Clean() {
                filter.RemoveComponentForAll<T>();
            }

            public Type GetInnerType() {
                return typeof(T);
            }
        }

        private interface ICanClean {
            void Clean();
            Type GetInnerType();
        }
    }
}