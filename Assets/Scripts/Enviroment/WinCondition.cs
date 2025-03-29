using System.Collections;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private RectTransform panelAnimation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.HasNextLevel())
            {
                int currentIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                string currentLevel = "00" + currentIndex;
                string nextLevel = "00" + (currentIndex + 1);

                if (panelAnimation != null)
                {
                    panelAnimation.gameObject.SetActive(true);
                    PanelAnimation.Instance.StartTransition(currentLevel, nextLevel);
                    StartCoroutine(LoadNextLevelAfterDelay(5f));
                }
            }
            else
            {
                GameManager.Instance.ReturnToMainMenu();
            }
        }
    }

    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.NextLevel();
    }
}
