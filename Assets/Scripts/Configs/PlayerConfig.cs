using UnityEngine;

[CreateAssetMenu]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private float _baseHP;

    public float BaseMoveSpeed => _baseMoveSpeed;
    public float BaseHP => _baseHP;
}