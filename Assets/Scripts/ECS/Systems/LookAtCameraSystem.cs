using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(LookAtCameraSystem))]
public sealed class LookAtCameraSystem : UpdateSystem
{
    private Filter _cameraFilter;
    private Filter _lookAtCameraFilter; 
        
    public override void OnAwake()
    {
        _cameraFilter = World.Filter.With<MainCamera>().Build();
        _lookAtCameraFilter = World.Filter.With<LookAtCamera>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        var camera = _cameraFilter.FirstOrDefault();
        if (!camera.IsNullOrDisposed())
        {
            foreach (var item in _lookAtCameraFilter)
            {
                item.GetComponent<LookAtCamera>().Transform.forward = camera.GetComponent<MainCamera>().Camera.transform.forward;
            }
        }
    }
}