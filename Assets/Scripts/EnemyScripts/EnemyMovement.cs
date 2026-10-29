using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyFollow follow;
    private EnemyStateMachine stateMachine;
    private EnemyLife enemyLife;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GetComponent<EnemyFollow>();
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyLife = GetComponent<EnemyLife>();
    }
    private void FixedUpdate()
    {
        if (enemyLife != null && enemyLife.IsDead)
        {
            return;
        }
    }
}