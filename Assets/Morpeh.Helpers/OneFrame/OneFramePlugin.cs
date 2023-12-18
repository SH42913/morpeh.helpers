namespace Scellecs.Morpeh.Helpers.OneFrame {
    using UnityEngine;
    using UnityEngine.Scripting;

    [Preserve]
    public sealed class OneFramePlugin : IWorldPlugin {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void Init() {
            WorldExtensions.AddWorldPlugin(new OneFramePlugin());
        }

        [Preserve]
        internal OneFramePlugin() { }

        [Preserve]
        public void Initialize(World world) {
            SystemsGroup systemsGroup = world.CreateSystemsGroup();
            systemsGroup.AddSystem(new OneFrameCleanSystem());
            world.AddPluginSystemsGroup(systemsGroup);
        }

        void IWorldPlugin.Deinitialize(World world) { }
    }
}