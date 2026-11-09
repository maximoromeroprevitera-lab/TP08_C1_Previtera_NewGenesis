using UnityEngine;
public class InvincibilityPickable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPickableEffects effects =
            collision.GetComponentInParent<PlayerPickableEffects>();

        if (effects == null)
            return;

        if (effects.TryCollectInvincibility())
            Destroy(gameObject);
    }
}