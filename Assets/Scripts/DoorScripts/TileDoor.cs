using UnityEngine;
using UnityEngine.Tilemaps;
public class TileDoor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap groundTilemap;
    [Header("Tiles to Remove")]
    [SerializeField] private Vector3Int startPosition = new Vector3Int(-7, -5, 0);
    [SerializeField] private int tilesToRemove = 4;
    private bool isOpen;

    public void OpenDoor()
    {
        if (isOpen)
            return;

        for (int i = 0; i < tilesToRemove; i++)
        {
            Vector3Int position = new Vector3Int(
                startPosition.x + i,
                startPosition.y,
                startPosition.z
            );

            groundTilemap.SetTile(position, null);
        }

        isOpen = true;
    }
}