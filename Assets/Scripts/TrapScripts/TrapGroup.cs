using UnityEngine;
public class TrapGroup : MonoBehaviour
{
    [SerializeField] private TrapLauncher[] traps;

    public void DisableTraps()
    {
        foreach (TrapLauncher trap in traps)
        {
            trap.SetTrapActive(false);
        }
    }
}