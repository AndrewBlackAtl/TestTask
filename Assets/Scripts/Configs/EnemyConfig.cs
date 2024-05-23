using UnityEngine;

[CreateAssetMenu]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _hp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _stopRadius;
    [SerializeField] private GameObject _spawnObjectOnDeath;
    [SerializeField] private float _objectSpawnProbability;

    public float StopRadius => _stopRadius;
    public float HP => _hp;
    public float MoveSpeed => _moveSpeed;
    public float Damage => _damage;
    public GameObject Prefab => _enemyPrefab;
    public float AttackCooldown => _attackCooldown;
    public GameObject SpawnObjectOnDeath => _spawnObjectOnDeath;
    public float ObjectSpawnProbability => _objectSpawnProbability;
}
