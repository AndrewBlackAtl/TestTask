using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ProjectileSpawnSystem))]
public sealed class ProjectileSpawnSystem : UpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<ProjectileSpawner>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _filter)
        {
            ref var spawner = ref item.GetComponent<ProjectileSpawner>();
            spawner.CurrentCooldown -= deltaTime;
            if (spawner.CurrentCooldown <= 0f)
            {
                var projectile = Instantiate(spawner.ProjectileConfig.ProjectilePrefab);
                projectile.transform.position = spawner.SpawnTransform.position;
                var collisionHandler = projectile.GetComponent<UnityCollisionHandler>();
                collisionHandler.InteractionLayerMask = spawner.HitMask;
                collisionHandler.Handler = new DealDamageHandler(World, spawner.ProjectileConfig.ProjectileDamage);
                
                var entity = projectile.GetComponent<EntityProvider>().Entity;
                entity.GetComponent<UnityRigidbody>().Rigidbody.position = spawner.SpawnTransform.position;
                entity.GetComponent<MoveSpeed>().Speed = spawner.ProjectileConfig.ProjectileSpeed;
                entity.GetComponent<MoveDirection>().Direction = Random.insideUnitCircle.normalized;
                entity.GetComponent<GameObjectLifetime>().CurrentLifetime = spawner.ProjectileConfig.ProjectileLifetime;
                
                spawner.CurrentCooldown = spawner.Cooldown;
            }
        }
    }
}