using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float comboResetTime = 1f;
    private PlayerBackDash playerBackDash;
    private PlayerStateMachine stateMachine;
    private int comboIndex;
    private float lastAttackTime;
    private bool isAttacking;
    private bool nextAttackQueued;
    public bool IsAttacking => isAttacking;
    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        playerBackDash = GetComponent<PlayerBackDash>();
        stateMachine = GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        if (playerBackDash.IsDashing)
        {
            return;
        }

        if (comboIndex > 0 &&
            !isAttacking &&
            Time.time - lastAttackTime > comboResetTime)
        {
            comboIndex = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                comboIndex = 1;
                lastAttackTime = Time.time;
                StartAttack();
            }
            else if (comboIndex < 3)
            {
                nextAttackQueued = true;
                lastAttackTime = Time.time;
            }
        }
    }
    private void StartAttack()
    {
        isAttacking = true;
        nextAttackQueued = false;

        stateMachine.ChangeState(PlayerState.Attack);

        animator.Play("Attack" + comboIndex, 0, 0f);
    }
    public void FinishAttack()
    {
        if (nextAttackQueued && comboIndex < 3)
        {
            comboIndex++;
            lastAttackTime = Time.time;
            StartAttack();
        }
        else
        {
            isAttacking = false;
            nextAttackQueued = false;

            stateMachine.ChangeState(PlayerState.Idle);
        }

        if (comboIndex >= 3 && !isAttacking)
        {
            comboIndex = 0;
        }
    }
}