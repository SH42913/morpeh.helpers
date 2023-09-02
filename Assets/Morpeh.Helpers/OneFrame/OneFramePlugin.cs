namespace Scellecs.Morpeh.Helpers.OneFrame {
    using UnityEngine.Scripting;

    [Preserve]
    public sealed class OneFramePlugin : IWorldPlugin {
        [Preserve]
        public OneFramePlugin() { }

        [Preserve]
        public void Initialize(World world) {
            SystemsGroup systemsGroup = world.CreateSystemsGroup();
            systemsGroup.AddSystem(new OneFrameCleanSystem());
            world.AddPluginSystemsGroup(systemsGroup);
        }

        void IWorldPlugin.Deinitialize(World world) { }
    }
}