using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct CoinsUI : IComponent
{
    public TextMeshProUGUI CoinsText;

    private int _coinsValue;

    public void SetCoins(int value)
    {
        if (_coinsValue == value)
        {
            return;
        }
        _coinsValue = value;
        CoinsText.SetText(_coinsValue.ToString());
    }
}