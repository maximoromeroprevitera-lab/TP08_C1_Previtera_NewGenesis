using UnityEngine;
public class DoorLock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TileDoor tileDoor;
    private PlayerInventory playerInventory;
    private bool playerInRange;
    private bool isUnlocked;
    private void Update()
    {
        if (!playerInRange || isUnlocked)
            return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (playerInventory == null)
                return;

            if (!playerInventory.HasKey)
            {
                Debug.Log("You need a key!");
                return;
            }

            tileDoor.OpenDoor();
            playerInventory.UseKey();

            isUnlocked = true;

            Debug.Log("Door unlocked!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory inventory =
            collision.GetComponentInParent<PlayerInventory>();

        if (inventory != null)
        {
            playerInventory = inventory;
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInventory inventory =
            collision.GetComponentInParent<PlayerInventory>();

        if (inventory != null)
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}