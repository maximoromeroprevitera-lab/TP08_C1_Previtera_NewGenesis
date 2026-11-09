using System.Collections.Generic;
using UnityEngine;
public class PickablePool : MonoBehaviour
{
    [SerializeField] private GameObject[] pickablePrefabs;
    [SerializeField] private int initialAmount = 2;
    private List<GameObject>[] pools;
    private void Awake()
    {
        pools = new List<GameObject>[pickablePrefabs.Length];

        for (int i = 0; i < pickablePrefabs.Length; i++)
        {
            pools[i] = new List<GameObject>();

            for (int j = 0; j < initialAmount; j++)
            {
                GameObject pickable = Instantiate(pickablePrefabs[i], transform);
                pickable.SetActive(false);
                pools[i].Add(pickable);
            }
        }
    }
    public GameObject GetPickable(int type, Vector3 position)
    {
        foreach (GameObject pickable in pools[type])
        {
            if (!pickable.activeInHierarchy)
            {
                pickable.transform.position = position;
                pickable.SetActive(true);
                return pickable;
            }
        }

        GameObject newPickable = Instantiate(
            pickablePrefabs[type],
            position,
            Quaternion.identity,
            transform
        );

        pools[type].Add(newPickable);

        return newPickable;
    }
    public void ReturnPickable(GameObject pickable)
    {
        pickable.SetActive(false);
    }
}
