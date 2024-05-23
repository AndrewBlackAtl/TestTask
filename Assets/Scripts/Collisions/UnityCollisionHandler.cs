using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class UnityCollisionHandler : MonoBehaviour
{
    [SerializeReference] public CollisionHandler Handler;
    public InteractionLayerEnum InteractionLayerMask;
    public GameObject DestroyObjectAfterInteraction;
    
    private void OnTriggerEnter(Collider other)
    {
        var entityProvider = other.GetComponent<EntityProvider>();
        if (entityProvider == null)
        {
            return;
        }

        var entity = entityProvider.Entity;
        if (!entity.Has<InteractionLayer>())
        {
            return;
        }

        var layer = entity.GetComponent<InteractionLayer>().Layer;
        if (!InteractionLayerMask.HasFlag(layer))
        {
            return;
        }
        
        Handler?.OnTriggerEnter(entity);

        if (DestroyObjectAfterInteraction != null)
        {
            Destroy(DestroyObjectAfterInteraction);
        }
    }
}