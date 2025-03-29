using System.Collections;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager Instance { get; private set; }
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    public float colorChangeDuration = 2.5f;

    [Header("Color Settings")]
    public Color startColor = Color.white;
    public Color targetColor = Color.black;

    private Coroutine colorChangeCoroutine;

    private void Awake()
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

    void Start()
    {
        globalLight.color = startColor;

        StartColorChange(targetColor, colorChangeDuration);
    }

    public void StartColorChange(Color newColor, float duration)
    {
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        colorChangeCoroutine = StartCoroutine(ChangeColor(newColor, duration));
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        Color initialColor = globalLight.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            globalLight.color = Color.Lerp(initialColor, targetColor, progress);
            yield return null;
        }

        globalLight.color = targetColor;
    }

    public void ResetToStartColor(float duration)
    {
        StartColorChange(startColor, duration);
    }
}