using UnityEngine;

[CreateAssetMenu]
public class ProjectileConfig : ScriptableObject
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileLifetime;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileDamage;
    
    public GameObject ProjectilePrefab => _projectilePrefab;
    public float ProjectileLifetime => _projectileLifetime;
    public float ProjectileSpeed => _projectileSpeed;
    public float ProjectileDamage => _projectileDamage;
}
