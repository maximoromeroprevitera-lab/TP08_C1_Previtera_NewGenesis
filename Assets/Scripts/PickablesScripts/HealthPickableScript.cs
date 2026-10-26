using UnityEngine;
public class HealthPickable : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLife player = collision.GetComponent<PlayerLife>();

        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}