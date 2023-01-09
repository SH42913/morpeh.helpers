﻿namespace Scellecs.Morpeh.Helpers.OneFrame {
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS/Helpers/" + nameof(OneFrameRegister))]
    public sealed class OneFrameRegister : ScriptableObject {
        public int startCapacity = 1;

        private ICanClean[] oneFrameFilters;
        private int registeredFilters;

        private void OnEnable() {
            CleanFilters();
        }

        // ReSharper disable once UnusedMember.Global
        public void RegisterOneFrame<T>(World world)
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
            CleanFilters();
        }

        private void CleanFilters() {
            oneFrameFilters = new ICanClean[startCapacity];
            registeredFilters = 0;
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