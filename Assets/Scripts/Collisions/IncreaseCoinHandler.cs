using System;
using Scellecs.Morpeh;

[Serializable]
public class IncreaseCoinHandler : CollisionHandler
{
    public int Amount;
    public override void OnTriggerEnter(Entity entity)
    {
        if (!entity.Has<CoinsStorage>())
        {
            return;
        }

        entity.GetComponent<CoinsStorage>().Coins += Amount;
    }
}
