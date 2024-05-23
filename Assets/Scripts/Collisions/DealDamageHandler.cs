using Scellecs.Morpeh;

public class DealDamageHandler : CollisionHandler
{
    private readonly World _world;
    private readonly float _damage;
    
    public DealDamageHandler(World world, float damage)
    {
        _world = world;
        _damage = damage;
    }
    
    public override void OnTriggerEnter(Entity entity)
    {
        _world.CreateDamageEvent(entity, _damage);
    }
}