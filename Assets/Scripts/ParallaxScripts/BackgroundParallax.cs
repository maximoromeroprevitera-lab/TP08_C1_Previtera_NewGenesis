using UnityEngine;
public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float parallaxSpeed = 0.35f;
    private Transform background1;
    private Transform background2;
    private float backgroundWidth;
    private float previousPlayerX;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;

        background1 = transform.GetChild(0);
        background2 = transform.GetChild(1);

        SpriteRenderer spriteRenderer =
            background1.GetComponent<SpriteRenderer>();

        backgroundWidth = spriteRenderer.bounds.size.x;

        previousPlayerX = player.position.x;
    }
    void Update()
    {
        if (player == null || cam == null)
        {
            return;
        }

        float playerMovement = player.position.x - previousPlayerX;
        float movement = -playerMovement * parallaxSpeed;

        Vector3 movementVector = Vector3.right * movement;

        background1.position += movementVector;
        background2.position += movementVector;

        float cameraLeft =
            cam.transform.position.x - cam.orthographicSize * cam.aspect;

        float cameraRight =
            cam.transform.position.x + cam.orthographicSize * cam.aspect;

        if (playerMovement > 0f)
        {
            RepeatRight(background1, background2, cameraLeft);
            RepeatRight(background2, background1, cameraLeft);
        }
        else if (playerMovement < 0f)
        {
            RepeatLeft(background1, background2, cameraRight);
            RepeatLeft(background2, background1, cameraRight);
        }

        previousPlayerX = player.position.x;
    }
    void RepeatRight(Transform background, Transform otherBackground, float cameraLeft)
    {
        if (background.position.x + backgroundWidth / 2f < cameraLeft)
        {
            background.position = new Vector3(
                otherBackground.position.x + backgroundWidth,
                background.position.y,
                background.position.z
            );
        }
    }
    void RepeatLeft(Transform background, Transform otherBackground, float cameraRight)
    {
        if (background.position.x - backgroundWidth / 2f > cameraRight)
        {
            background.position = new Vector3(
                otherBackground.position.x - backgroundWidth,
                background.position.y,
                background.position.z
            );
        }
    }
}
