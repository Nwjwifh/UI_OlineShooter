using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeIn : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Start()
    {
        // Set the initial alpha value to 0 to make the canvas invisible.
        canvasGroup.alpha = 0f;

        // Start the fade-in coroutine.
        StartCoroutine(FadeInCanvas());
    }

    private IEnumerator FadeInCanvas()
    {
        // Gradually increase the alpha value to 1 over the fade duration.
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is set to 1 at the end of the coroutine.
        canvasGroup.alpha = 1f;
    }
}
