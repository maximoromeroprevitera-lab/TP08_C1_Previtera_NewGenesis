using System.Collections;
using UnityEngine;
public class PlayerAttackHitbox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private Transform shootPoint;
    [Header("Attack Damage")]
    [SerializeField] private int attack1Damage = 1;
    [SerializeField] private int attack2Damage = 1;
    [SerializeField] private int attack3Damage = 2;
    [Header("Attack Timing")]
    [SerializeField] private float attack1Delay = 0.2f;
    [SerializeField] private float attack2Delay = 0.2f;
    [SerializeField] private float attack3Delay = 0.25f;
    private Coroutine attackCoroutine;
    public void StartHitbox(int attackIndex)
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        attackCoroutine = StartCoroutine(SpawnHitbox(attackIndex));
    }
    private IEnumerator SpawnHitbox(int attackIndex)
    {
        float delay = 0f;
        int damage = 0;

        switch (attackIndex)
        {
            case 1:
                delay = attack1Delay;
                damage = attack1Damage;
                break;

            case 2:
                delay = attack2Delay;
                damage = attack2Damage;
                break;

            case 3:
                delay = attack3Delay;
                damage = attack3Damage;
                break;

            default:
                yield break;
        }

        yield return new WaitForSeconds(delay);

        if (hitboxPrefab != null && shootPoint != null)
        {
            GameObject hitbox = Instantiate(
                hitboxPrefab,
                shootPoint.position,
                shootPoint.rotation
            );

            PlayerHitbox hitboxScript = hitbox.GetComponent<PlayerHitbox>();

            if (hitboxScript != null)
                hitboxScript.Setup(damage);
        }

        attackCoroutine = null;
    }
}