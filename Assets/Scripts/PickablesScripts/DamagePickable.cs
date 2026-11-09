using UnityEngine;
public class DamagePickable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPickableEffects effects =
            collision.GetComponentInParent<PlayerPickableEffects>();

        if (effects == null)
            return;

        if (effects.TryCollectDamageBoost())
            Destroy(gameObject);
    }
}