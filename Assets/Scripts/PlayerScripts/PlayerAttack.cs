using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float comboResetTime = 1f;
    private PlayerBackDash playerBackDash;
    private int comboIndex;
    private float lastAttackTime;
    private bool isAttacking;
    private bool canChainAttack;
    private bool nextAttackQueued;
    public bool IsAttacking => isAttacking;
    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        playerBackDash = GetComponent<PlayerBackDash>();
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
            else if (canChainAttack && comboIndex < 3)
            {
                lastAttackTime = Time.time;
                nextAttackQueued = true;
            }
        }
    }
    private void StartAttack()
    {
        isAttacking = true;
        canChainAttack = false;
        nextAttackQueued = false;

        animator.Play("Attack" + comboIndex, 0, 0f);
    }
    public void OpenComboWindow()
    {
        canChainAttack = true;
    }
    public void CloseComboWindow()
    {
        canChainAttack = false;
    }
    public void FinishAttack()
    {
        isAttacking = false;
        canChainAttack = false;

        if (nextAttackQueued && comboIndex < 3)
        {
            comboIndex++;
            StartAttack();
        }
        else
        {
            nextAttackQueued = false;
        }

        if (comboIndex >= 3 && !isAttacking)
        {
            comboIndex = 0;
        }
    }
}