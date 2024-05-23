using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(LifetimeCheckSystem))]
public sealed class LifetimeCheckSystem : UpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<GameObjectLifetime>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _filter)
        {
            ref var lifetimeComp = ref item.GetComponent<GameObjectLifetime>();
            lifetimeComp.CurrentLifetime -= deltaTime;
            if (lifetimeComp.CurrentLifetime <= 0f)
            {
                Destroy(lifetimeComp.GameObject);
            }
        }
    }
}