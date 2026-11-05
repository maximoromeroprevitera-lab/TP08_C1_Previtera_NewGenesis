using UnityEngine;
public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory inventory = collision.GetComponentInParent<PlayerInventory>();

        if (inventory == null)
            return;

        if (inventory.HasKey)
            return;

        inventory.GetKey();

        Destroy(gameObject);
    }
}