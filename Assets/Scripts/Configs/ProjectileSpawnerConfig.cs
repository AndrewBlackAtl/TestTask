using UnityEngine;

[CreateAssetMenu]
public class ProjectileSpawnerConfig : ScriptableObject
{
    [SerializeField] private ProjectileConfig _projectileConfig;
    [SerializeField] private InteractionLayerEnum _hitMask;
    [SerializeField] private float _cooldown;
    
    public ProjectileConfig ProjectileConfig => _projectileConfig;
    public InteractionLayerEnum HitMask => _hitMask;
    public float Cooldown => _cooldown;
}