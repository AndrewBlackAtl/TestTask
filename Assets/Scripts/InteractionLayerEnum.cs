using System;

[Flags]
public enum InteractionLayerEnum
{
    Player = 1 << 0,
    Enemy = 1 << 1
}