namespace Scellecs.Morpeh.Helpers.OneFrame {
    using JetBrains.Annotations;

    public static class OneFrameWorldExtension {
        [PublicAPI]
        public static void RegisterOneFrame<TEvent>(this World world)
                where TEvent : struct, IComponent {
            OneFrameRegister.RegisterOneFrame<TEvent>(world);
        }
    }
}