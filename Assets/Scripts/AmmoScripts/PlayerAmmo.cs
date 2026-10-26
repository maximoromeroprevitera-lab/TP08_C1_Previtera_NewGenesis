using UnityEngine;
public class PlayerAmmo : MonoBehaviour
{
    [SerializeField] private int magazineSize = 4;
    [SerializeField] private int reserveBullets = 12;
    [SerializeField] private float reloadTime = 1f;
    private int currentBullets;
    private bool reloading;
    private float reloadTimer;
    private Animator animator;
    public int CurrentBullets => currentBullets;
    public int ReserveBullets => reserveBullets;
    public bool CanShoot => currentBullets > 0 && !reloading;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentBullets = magazineSize;
    }
    private void Update()
    {
        if (reloading)
        {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0)
            {
                reloading = false;
                currentBullets++;
                reserveBullets--;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.R) &&
            currentBullets < magazineSize &&
            reserveBullets > 0)
        {
            reloading = true;
            reloadTimer = reloadTime;

            animator.SetTrigger("Recharge");
        }
    }
    public void UseBullet()
    {
        currentBullets--;
    }
    public void AddAmmo(int amount)
    {
        reserveBullets += amount;
    }
}