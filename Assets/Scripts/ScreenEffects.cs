using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    public Image flashImage;            // La imagen blanca del Canvas
    public float fadeInDuration = 0.3f; // Duración del fade in (blanco)
    public float holdDuration = 1.5f;   // Tiempo que se mantiene blanco
    public float fadeToBlackDuration = 1f; // Duración del fade out a negro

    public void PlayDeathEffects()
    {
        StartCoroutine(FlashWhiteToBlack());
    }

    private IEnumerator FlashWhiteToBlack()
    {
        // 1️⃣ Fade IN blanco
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
            flashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        flashImage.color = new Color(1f, 1f, 1f, 1f); // asegurar alpha 1

        // 2️⃣ Mantener blanco
        yield return new WaitForSeconds(holdDuration);

        // 3️⃣ Fade OUT a negro
        t = 0f;
        Color startColor = flashImage.color; // blanco
        Color endColor = Color.black;        // negro

        while (t < fadeToBlackDuration)
        {
            t += Time.deltaTime;
            flashImage.color = Color.Lerp(startColor, endColor, t / fadeToBlackDuration);
            yield return null;
        }

        flashImage.color = Color.black; // asegurar negro al final
    }
}
