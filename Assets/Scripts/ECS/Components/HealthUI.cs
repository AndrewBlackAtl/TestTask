using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct HealthUI : IComponent 
{
    public TextMeshProUGUI HPText;

    private float _hpValue;

    public void SetHP(float value)
    {
        if (_hpValue == value)
        {
            return;
        }
        _hpValue = value;
        HPText.SetText(_hpValue.ToString());
    }
}