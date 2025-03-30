using TMPro;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TMP_Text currentLevelText;
    [SerializeField] private TMP_Text nextLevelText;

    public static PanelAnimation Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTransition(string currentText, string nextText, float delayAlpha = 2.5f, float delayMove = 3f)
    {
        currentLevelText.text = currentText;
        nextLevelText.text = nextText;

        Color nextTextColor = nextLevelText.color;
        nextTextColor.a = 0;
        nextLevelText.color = nextTextColor;

        LeanTween.moveY(currentLevelText.rectTransform, currentLevelText.rectTransform.anchoredPosition.y + 250, delayMove)
            .setEase(LeanTweenType.easeInOutQuad);

        LeanTween.value(currentLevelText.gameObject, 1, 0, delayAlpha)
            .setOnUpdate((float val) =>
            {
                Color color = currentLevelText.color;
                color.a = val;
                currentLevelText.color = color;
            })
            .setOnComplete(() =>
            {
                LeanTween.moveY(nextLevelText.rectTransform, 0, 2.5f)
                    .setEase(LeanTweenType.easeInOutQuad);

                LeanTween.value(nextLevelText.gameObject, 0, 1, 3f)
                    .setOnUpdate((float val) =>
                    {
                        Color color = nextLevelText.color;
                        color.a = val;
                        nextLevelText.color = color;
                    });
            });
    }
}
