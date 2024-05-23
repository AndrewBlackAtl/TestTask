using Scellecs.Morpeh;

public static class DamageUtils 
{
    public static void CreateDamageEvent(this World world, Entity target, float damageAmount)
    {
        if (target.IsNullOrDisposed() || target.Has<DeathMarker>() || !target.Has<Health>())
        {
            return;
        }
        ref var damage = ref world.CreateEntity().AddComponent<DamageEvent>();
        damage.Target = target;
        damage.DamageAmount = damageAmount;
    }
}
