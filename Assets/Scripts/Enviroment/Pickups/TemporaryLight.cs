using UnityEngine;

public class TemporaryLight : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private Color darkColor = Color.black;
    [SerializeField] private float lightDuration = 1f;

    [Header("Pickup Settings")]
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    [Header("Spotlight Settings")]
    public UnityEngine.Rendering.Universal.Light2D spotLight;
    public float minFalloff = 0.3f;
    public float maxFalloff = 0.8f;
    public float speed = 2f;

    void Update()
    {
        if (spotLight != null)
        {
            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            spotLight.falloffIntensity = Mathf.Lerp(minFalloff, maxFalloff, t);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(pickupSound, 0.7f);
            LightManager.Instance.ResetToStartColor(0);
            LightManager.Instance.StartColorChange(darkColor, lightDuration);
            Destroy(gameObject);
        }
    }
}
