using UnityEngine;
public class TrapSwitch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TrapGroup trapGroup;
    private bool activated;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        PlayerLife player = collision.GetComponentInParent<PlayerLife>();

        if (player == null)
            return;

        if (player.IsDead)
            return;

        trapGroup.DisableTraps();

        activated = true;

        Debug.Log("Traps disabled!");
    }
}