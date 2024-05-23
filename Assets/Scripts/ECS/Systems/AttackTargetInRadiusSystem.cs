using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AttackTargetInRadiusSystem))]
public sealed class AttackTargetInRadiusSystem : UpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<DealDamagePerTime>().With<InRadius>().With<Target>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var item in _filter)
        {
            ref var damageComponent = ref item.GetComponent<DealDamagePerTime>();
            damageComponent.CurrentCooldown -= deltaTime;
            if (damageComponent.CurrentCooldown <= 0f)
            {
                if (!item.GetComponent<InRadius>().IsInRadius)
                {
                    continue;
                }
                
                var target = item.GetComponent<Target>().Entity;
                
                World.CreateDamageEvent(target, damageComponent.Damage);
                
                damageComponent.CurrentCooldown = damageComponent.Cooldown;
            }
        }
    }
}