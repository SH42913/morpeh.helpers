using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers.OneFrame;
using UnityEngine;

[Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
[Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
[Unity.IL2CPP.CompilerServices.Il2CppSetOption(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/" + nameof(OneFrameTestSystem))]
public sealed class OneFrameTestSystem : Scellecs.Morpeh.Systems.UpdateSystem {
    [SerializeField] private OneFrameRegister oneFrameRegister;

    private Filter oneFrameFilter;

    public override void OnAwake() {
        oneFrameRegister.RegisterOneFrame<OneFrameComponent>(World);
        oneFrameFilter = World.Filter.With<OneFrameComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        if (oneFrameFilter.IsEmpty()) {
            World.CreateEntity().AddComponent<OneFrameComponent>();
        } else {
            Debug.LogError("There's more than one component! Looks like OneFrame was not clean!");
        }
    }

    public static OneFrameTestSystem Create() => CreateInstance<OneFrameTestSystem>();

    private struct OneFrameComponent : IComponent { }
}