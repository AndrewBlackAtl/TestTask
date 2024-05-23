using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ProjectileSpawner : IComponent
{
    public ProjectileConfig ProjectileConfig;
    public Transform SpawnTransform;
    public InteractionLayerEnum HitMask;
    public float Cooldown;
    public float CurrentCooldown;
}