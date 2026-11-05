using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    private bool hasKey;
    public bool HasKey => hasKey;
    public void GetKey()
    {
        hasKey = true;
        Debug.Log("Key obtained!");
    }
    public void UseKey()
    {
        hasKey = false;
    }
}