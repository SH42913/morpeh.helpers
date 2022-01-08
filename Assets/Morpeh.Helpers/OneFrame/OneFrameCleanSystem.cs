namespace Morpeh.Helpers.OneFrame {
    using UnityEngine;

    [CreateAssetMenu(menuName = "ECS/Helpers/" + nameof(OneFrameCleanSystem))]
    public class OneFrameCleanSystem : LateUpdateSystem {
        public OneFrameRegister register;

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime) {
            register.CleanOneFrameEvents();
        }

        public static OneFrameCleanSystem Create() {
            return CreateInstance<OneFrameCleanSystem>();
        }
    }
}