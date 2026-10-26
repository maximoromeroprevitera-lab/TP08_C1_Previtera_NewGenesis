using UnityEngine;
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform fill;
    private Vector3 originalScale;
    private void Start()
    {
        originalScale = fill.localScale;
    }
    public void SetHealth(float currentHealth, float maxHealth)
    {
        float healthPercent = currentHealth / maxHealth;

        fill.localScale = new Vector3(
            originalScale.x * healthPercent,
            originalScale.y,
            originalScale.z
        );
    }
}