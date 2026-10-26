using TMPro;
using UnityEngine;
public class AmmoCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private GameObject victoryPanel;
    private PlayerAmmo playerAmmo;
    private PlayerLife playerLife;
    private void Start()
    {
        playerAmmo = FindFirstObjectByType<PlayerAmmo>();
        playerLife = FindFirstObjectByType<PlayerLife>();
    }
    private void Update()
    {
        if (playerAmmo == null || playerLife == null) return;

        if (playerLife.IsDead ||
            (victoryPanel != null && victoryPanel.activeInHierarchy))
        {
            ammoText.gameObject.SetActive(false);
            return;
        }

        ammoText.text = playerAmmo.CurrentBullets + " / " + playerAmmo.ReserveBullets;
    }
}
