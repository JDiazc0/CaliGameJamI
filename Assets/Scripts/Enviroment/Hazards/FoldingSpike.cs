using UnityEngine;

public class FoldingSpike : MonoBehaviour
{
    [Header("Settings")]
    public float hiddenDuration = 2f;
    public float exposedDuration = 3f;
    public float safeTimeAfterHide = 1f;
    //public AudioClip spikeSound;

    private Animator animator;
    private Collider2D hazardCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        hazardCollider = GetComponent<Collider2D>();
        hazardCollider.enabled = false;
        StartCoroutine(SpikeCycle());
    }

    System.Collections.IEnumerator SpikeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(hiddenDuration);

            animator.SetTrigger("ShowSpike");
            yield return new WaitForSeconds(GetAnimationDuration("ShowSpike"));

            hazardCollider.enabled = true;
            //if (spikeSound != null) AudioManager.Instance.PlaySFX(spikeSound);

            yield return new WaitForSeconds(exposedDuration);

            animator.SetTrigger("HideSpike");
            hazardCollider.enabled = false;
            yield return new WaitForSeconds(GetAnimationDuration("HideSpike"));

            yield return new WaitForSeconds(safeTimeAfterHide);
        }
    }

    private float GetAnimationDuration(string animationName)
    {
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f; // Si no se encuentra, retorna 0
    }
}