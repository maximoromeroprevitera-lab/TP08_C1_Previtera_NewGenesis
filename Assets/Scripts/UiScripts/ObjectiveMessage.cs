using UnityEngine;
using TMPro;
using System.Collections;
public class ObjectiveMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private float moveSpeed = 500f;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = objectiveText.GetComponent<RectTransform>();

        objectiveText.text = "¡SOBREVIVE!\nSobrevive 100 segundos y llega a la puerta del bar.";
        objectiveText.gameObject.SetActive(true);

        StartCoroutine(ShowObjective());
    }
    private IEnumerator ShowObjective()
    {
        yield return new WaitForSeconds(displayTime);

        float screenRight = Screen.width + rectTransform.rect.width;

        while (rectTransform.anchoredPosition.x < screenRight)
        {
            rectTransform.anchoredPosition +=
                Vector2.right * moveSpeed * Time.deltaTime;

            yield return null;
        }
        Destroy(gameObject);
    }

}
