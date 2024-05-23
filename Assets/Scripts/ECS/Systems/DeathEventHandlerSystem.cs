using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DeathEventHandlerSystem))]
public sealed class DeathEventHandlerSystem : UpdateSystem
{
    private Filter _destroyFilter;
    private Filter _removeComponentsFilter;
    private Filter _spawnObjectFilter;
    private Filter _raiseEventFilter;
    
    public override void OnAwake()
    {
        _destroyFilter = World.Filter.With<DeathEvent>().With<DestroyOnDeath>().Build();
        _removeComponentsFilter = World.Filter.With<DeathEvent>().With<RemoveComponentsOnDeath>().Build();
        _spawnObjectFilter = World.Filter.With<DeathEvent>().With<SpawnObjectOnDeath>().Build();
        _raiseEventFilter = World.Filter.With<DeathEvent>().With<RaiseEventOnDeath>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _spawnObjectFilter)
        {
            var spawnComp = item.GetComponent<SpawnObjectOnDeath>();
            if (spawnComp.Probability >= Random.value)
            {
                Instantiate(spawnComp.ObjectToSpawn, spawnComp.SpawnPoint.position, Quaternion.identity);
            }
        }
        
        foreach (var item in _raiseEventFilter)
        {
            item.GetComponent<RaiseEventOnDeath>().Event?.Invoke();
        }
        
        foreach (var item in _removeComponentsFilter)
        {
            var provider = item.GetComponent<RemoveComponentsOnDeath>().Provider;
            provider.enabled = false;
        }
        
        foreach (var item in _destroyFilter)
        {
            var objectToDestroy = item.GetComponent<DestroyOnDeath>().ObjectToDestroy;
            Destroy(objectToDestroy);
        }
    }
}