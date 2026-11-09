using UnityEngine;
public class InvincibilityPickable : MonoBehaviour
{
    private PickablePool pool;
    private void Awake()
    {
        pool = FindFirstObjectByType<PickablePool>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPickableEffects effects =
            collision.GetComponentInParent<PlayerPickableEffects>();

        if (effects == null)
            return;

        if (effects.TryCollectInvincibility())
            pool.ReturnPickable(gameObject);
    }
}