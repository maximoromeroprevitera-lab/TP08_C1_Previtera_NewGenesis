using UnityEngine;
using TMPro;
public class DoorLock : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TileDoor tileDoor;
    [SerializeField] private TMP_Text interactionText;
    private PlayerInventory playerInventory;
    private bool playerInRange;
    private bool isUnlocked;
    private void Awake()
    {
        interactionText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        if (!playerInRange || isUnlocked)
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        interactionText.gameObject.SetActive(true);

        if (playerInventory != null && playerInventory.HasKey)
        {
            interactionText.text = "Presioná [F] para insertar la llave";

            if (Input.GetKeyDown(KeyCode.F))
            {
                tileDoor.OpenDoor();
                playerInventory.UseKey();

                isUnlocked = true;
                interactionText.gameObject.SetActive(false);

                Debug.Log("Door unlocked!");
            }
        }
        else
        {
            interactionText.text = "Necesitás una llave";
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

            interactionText.gameObject.SetActive(false);
        }
    }
}