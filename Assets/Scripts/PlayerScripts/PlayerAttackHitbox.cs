using System.Collections;
using UnityEngine;
public class PlayerAttackHitbox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Coroutine attackCoroutine;
    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
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
                delay = playerData.attack1Delay;
                damage = playerData.attack1Damage;
                break;

            case 2:
                delay = playerData.attack2Delay;
                damage = playerData.attack2Damage;
                break;

            case 3:
                delay = playerData.attack3Delay;
                damage = playerData.attack3Damage;
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

            BoxCollider2D hitboxCollider =
                hitbox.GetComponent<BoxCollider2D>();

            if (hitboxCollider != null && spriteRenderer != null)
            {
                Vector2 offset = hitboxCollider.offset;

                offset.x = Mathf.Abs(offset.x) *
                    (spriteRenderer.flipX ? -1f : 1f);

                hitboxCollider.offset = offset;
            }

            PlayerHitbox hitboxScript =
                hitbox.GetComponent<PlayerHitbox>();

            if (hitboxScript != null)
                hitboxScript.Setup(damage);
        }

        attackCoroutine = null;
    }
}