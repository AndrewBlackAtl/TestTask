using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Events;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class RaiseEventOnDeathProvider : MonoProvider<RaiseEventOnDeath>
{
    public UnityEvent SerializedEvent;

    protected override void OnValidate()
    {
        base.OnValidate();

        GetData().Event = SerializedEvent;
    }
}