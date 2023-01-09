namespace Scellecs.Morpeh.Helpers.OneFrame {
    using Systems;
    using UnityEngine;

    // ReSharper disable once ClassCanBeSealed.Global
    [CreateAssetMenu(menuName = "ECS/Helpers/" + nameof(OneFrameCleanSystem))]
    public class OneFrameCleanSystem : CleanupSystem {
        public OneFrameRegister register;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            register.CleanOneFrameEvents();
        }

        public override void Dispose() {
            register.Dispose();
        }

        // ReSharper disable once UnusedMember.Global
        public static OneFrameCleanSystem Create() {
            return CreateInstance<OneFrameCleanSystem>();
        }
    }
}