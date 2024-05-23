using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FollowTargetSystem))]
public sealed class FollowTargetSystem : FixedUpdateSystem
{
    private Filter _filter;
    
    public override void OnAwake()
    {
        _filter = World.Filter.With<MoveDirection>().With<UnityRigidbody>().With<Target>().With<InRadius>().Build();
    }

    public override void OnUpdate(float deltaTime) 
    {
        foreach (var item in _filter)
        {
            var followTarget = item.GetComponent<Target>();
            ref var moveDir = ref item.GetComponent<MoveDirection>();
            var selfRigidbody = item.GetComponent<UnityRigidbody>().Rigidbody;
            var followRigidbody = followTarget.Entity.GetComponent<UnityRigidbody>().Rigidbody;
            ref var radiusComp = ref item.GetComponent<InRadius>();
            if (Vector3.Distance(selfRigidbody.position, followRigidbody.position) <= radiusComp.Radius)
            {
                moveDir.Direction = Vector2.zero;
                radiusComp.IsInRadius = true;
                continue;
            }
            
            var worldDirection = followRigidbody.position - selfRigidbody.position;
            var direction = new Vector2(worldDirection.x, worldDirection.z).normalized;
            moveDir.Direction = direction;
            radiusComp.IsInRadius = false;
        }
    }
}