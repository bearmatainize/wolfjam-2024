using UnityEngine;
using System.Collections;

public class SquashedBug : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of fade-out effect
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  // Get the SpriteRenderer of the squashed bug

        // Start fading out the squashed bug immediately after it appears
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsedTime = 0f;                // Track how much time has passed
        Color initialColor = spriteRenderer.color;  // Initial color of the squashed bug (with full opacity)

        while (elapsedTime < fadeDuration)
        {
            // Calculate the alpha value based on the elapsed time
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the bug is fully transparent before destroying
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        // Destroy the squashed bug object after fading out
        Destroy(gameObject);
    }
}
