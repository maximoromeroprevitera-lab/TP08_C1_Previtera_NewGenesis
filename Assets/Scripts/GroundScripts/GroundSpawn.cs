using UnityEngine;
public class GroundSpawn : MonoBehaviour
{
    public Transform player;
    public Transform[] groundPieces;
    public float groundWidth = 30f;
    private float previousPlayerX;
    void Start()
    {
        previousPlayerX = player.position.x;
    }
    void Update()
    {
        if (player == null || groundPieces.Length == 0)
        {
            return;
        }
        float playerMovement = player.position.x - previousPlayerX;

        if (playerMovement > 0f)
        {
            foreach (Transform piece in groundPieces)
            {
                if (player.position.x > piece.position.x + groundWidth)
                {
                    float rightmostX = GetRightmostX();

                    piece.position = new Vector3(
                        rightmostX + groundWidth,
                        piece.position.y,
                        piece.position.z
                    );
                }
            }
        }
        else if (playerMovement < 0f)
        {
            foreach (Transform piece in groundPieces)
            {
                if (player.position.x < piece.position.x - groundWidth)
                {
                    float leftmostX = GetLeftmostX();

                    piece.position = new Vector3(
                        leftmostX - groundWidth,
                        piece.position.y,
                        piece.position.z
                    );
                }
            }
        }
        previousPlayerX = player.position.x;
    }
    float GetRightmostX()
    {
        float rightmostX = groundPieces[0].position.x;

        foreach (Transform piece in groundPieces)
        {
            if (piece.position.x > rightmostX)
            {
                rightmostX = piece.position.x;
            }
        }

        return rightmostX;
    }
    float GetLeftmostX()
    {
        float leftmostX = groundPieces[0].position.x;

        foreach (Transform piece in groundPieces)
        {
            if (piece.position.x < leftmostX)
            {
                leftmostX = piece.position.x;
            }
        }
        return leftmostX;
    }
}
