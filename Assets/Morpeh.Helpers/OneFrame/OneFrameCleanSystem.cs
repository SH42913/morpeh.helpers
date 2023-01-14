namespace Scellecs.Morpeh.Helpers.OneFrame {
    internal sealed class OneFrameCleanSystem : ICleanupSystem {
        private OneFrameRegister register;

        public World World { get; set; }

        public void OnAwake() {
            register = OneFrameRegister.GetFor(World);
        }

        public void OnUpdate(float deltaTime) {
            register.CleanOneFrameEvents();
        }

        public void Dispose() {
            register.Dispose();
        }
    }
}