using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers.OneFrame;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<DamageEvent>().Build();
        World.RegisterOneFrame<DamageEvent>();
        World.RegisterOneFrame<DeathEvent>();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _filter)
        {
            var target = item.GetComponent<DamageEvent>().Target;
            var damage = item.GetComponent<DamageEvent>().DamageAmount;
            ref var health = ref target.GetComponent<Health>();
            var healthRemain = health.CurrentHP - damage;
            if (healthRemain <= 0f)
            {
                health.CurrentHP = 0f;
                target.AddComponent<DeathMarker>();
                target.AddComponent<DeathEvent>();
            }
            else
            {
                health.CurrentHP = healthRemain;
            }
        }
    }
}