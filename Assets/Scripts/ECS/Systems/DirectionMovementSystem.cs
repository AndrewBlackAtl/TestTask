using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DirectionMovementSystem))]
public sealed class DirectionMovementSystem : FixedUpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<MoveDirection>().With<MoveSpeed>().With<UnityRigidbody>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _filter)
        {
            var rig = item.GetComponent<UnityRigidbody>().Rigidbody;
            var dir = item.GetComponent<MoveDirection>().Direction;
            var speed = item.GetComponent<MoveSpeed>().Speed;
            rig.MovePosition(rig.position + new Vector3(dir.x, 0f, dir.y) * (deltaTime * speed));
        }
    }
}