using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SimpleUIUpdateSystem))]
public sealed class SimpleUIUpdateSystem : UpdateSystem
{
    private Filter _healthFilter;
    private Filter _coinsFilter;
    
    public override void OnAwake()
    {
        _healthFilter = World.Filter.With<Health>().With<HealthUI>().Build();
        _coinsFilter = World.Filter.With<CoinsStorage>().With<CoinsUI>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _healthFilter)
        {
            var currentHP = item.GetComponent<Health>().CurrentHP;
            ref var healthUI = ref item.GetComponent<HealthUI>();
            healthUI.SetHP(currentHP);
            
        }
        foreach (var item in _coinsFilter)
        {
            var currentCoins = item.GetComponent<CoinsStorage>().Coins;
            ref var coinsUI = ref item.GetComponent<CoinsUI>();
            coinsUI.SetCoins(currentCoins);
        }
    }
}