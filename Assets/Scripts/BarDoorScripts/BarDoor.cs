using UnityEngine;
public class BarDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLife player = collision.GetComponentInParent<PlayerLife>();

        if (player != null)
        {
            FindFirstObjectByType<VictoryManager>().Win();
        }
    }
}