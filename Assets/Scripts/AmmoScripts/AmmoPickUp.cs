using UnityEngine;
public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int amount = 4;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAmmo playerAmmo = collision.GetComponentInParent<PlayerAmmo>();

        if (playerAmmo != null)
        {
            playerAmmo.AddAmmo(amount);
            Destroy(gameObject);
        }
    }
}