using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemySpawnerSystem))]
public sealed class EnemySpawnerSystem : UpdateSystem
{
    private Filter _spawnerFilter;
    private Filter _cameraFilter;
    private Filter _playerFilter;
    
    public override void OnAwake()
    {
        _spawnerFilter = World.Filter.With<EnemySpawner>().Build();
        _cameraFilter = World.Filter.With<MainCamera>().Build();
        _playerFilter = World.Filter.With<PlayerMarker>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _spawnerFilter)
        {
            ref var spawner = ref item.GetComponent<EnemySpawner>();
            spawner.CurrentCooldown -= deltaTime;
            if (spawner.CurrentCooldown <= 0f)
            {
                var enemy = Instantiate(spawner.Config.EnemyConfig.Prefab);
                var spawnPos = GetSpawnPos(spawner.Config.ScreenOffsetDistance);
                
                enemy.transform.position = spawnPos;
                
                var entity = enemy.GetComponent<EntityProvider>().Entity;
                entity.GetComponent<UnityRigidbody>().Rigidbody.position = spawnPos;
                entity.GetComponent<Target>().Entity = _playerFilter.First();
                entity.GetComponent<MoveSpeed>().Speed = spawner.Config.EnemyConfig.MoveSpeed;
                entity.GetComponent<InRadius>().Radius = spawner.Config.EnemyConfig.StopRadius;
                entity.GetComponent<Health>().CurrentHP = spawner.Config.EnemyConfig.HP;
                
                ref var dealDamagePerTime = ref entity.GetComponent<DealDamagePerTime>();
                dealDamagePerTime.Damage = spawner.Config.EnemyConfig.Damage;
                dealDamagePerTime.Cooldown = spawner.Config.EnemyConfig.AttackCooldown;
                
                ref var spawnOjectOnDeath = ref entity.GetComponent<SpawnObjectOnDeath>();
                spawnOjectOnDeath.Probability = spawner.Config.EnemyConfig.ObjectSpawnProbability;
                spawnOjectOnDeath.ObjectToSpawn = spawner.Config.EnemyConfig.SpawnObjectOnDeath;

                spawner.CurrentCooldown = spawner.Config.Cooldown;
            }
        }
    }

    private Vector3 GetSpawnPos(float spawnOffset)
    {
        var viewportPoint = GetRandomViewportBorderPoint();
        var ray = _cameraFilter.First().GetComponent<MainCamera>().Camera
            .ViewportPointToRay(viewportPoint);
        var offset =
            (new Vector3(viewportPoint.x, 0f, viewportPoint.y).normalized - new Vector3(0.5f, 0f, 0.5f)) *
            spawnOffset;
        Physics.Raycast(ray, out var hitInfo);
        var spawnPoint = hitInfo.point + offset;

        return spawnPoint;
    }

    private Vector2 GetRandomViewportBorderPoint()
    {
        var side = Random.Range(0, 4);
        Vector2 point;
        switch (side)
        {
            case 0: 
                point = new Vector2(0f, Random.Range(0f, 1f)); 
                break;
            case 1:
                point = new Vector2(Random.Range(0f, 1f), 1f);
                break;
            case 2:
                point = new Vector2(1f, Random.Range(0f, 1f));
                break;
            case 3:
            default:
                point = new Vector2(Random.Range(0f, 1f), 0f);
                break;
        }

        return point;
    }
}