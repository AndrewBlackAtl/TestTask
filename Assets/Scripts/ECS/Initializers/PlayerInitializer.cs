using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerInitializer))]
public sealed class PlayerInitializer : Initializer
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private ProjectileSpawnerConfig _projectileSpawnerConfig;
    
    public override void OnAwake()
    {
        var playerFilter = World.Filter.With<PlayerMarker>().With<MoveSpeed>().With<Health>().With<ProjectileSpawner>().Build();
        var player = playerFilter.First();

        ref var projectileSpawner = ref player.GetComponent<ProjectileSpawner>();
        projectileSpawner.ProjectileConfig = _projectileSpawnerConfig.ProjectileConfig;
        projectileSpawner.HitMask = _projectileSpawnerConfig.HitMask;
        projectileSpawner.Cooldown = _projectileSpawnerConfig.Cooldown;

        player.GetComponent<MoveSpeed>().Speed = _playerConfig.BaseMoveSpeed;
        player.GetComponent<Health>().CurrentHP = _playerConfig.BaseHP;
    }
}