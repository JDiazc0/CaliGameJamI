using System.Collections;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    [SerializeField] private RectTransform panelAnimation;
    [SerializeField] private AudioClip damageSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(damageSound, 0.5f);

            int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            string currentLevel = "00" + (currentIndex);

            if (panelAnimation != null)
            {
                panelAnimation.gameObject.SetActive(true);
                PanelAnimation.Instance.StartTransition("", currentLevel, 0f, 0f);
                StartCoroutine(RestartLevelAfterDelay(3f));
            }
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.RestartGame();
    }
}
