using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    public Image flashImage;
    public float fadeInDuration = 0.3f;
    public float holdDuration = 1.5f;
    public float fadeToBlackDuration = 1f;

    public void PlayDeathEffects()
    {
        StartCoroutine(FlashWhiteToBlack());
    }

    private IEnumerator FlashWhiteToBlack()
    {
        Time.timeScale = 0f; // El juego se pausa, pero el efecto seguirá

        // 1️⃣ Fade IN blanco (UNSCALED)
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
            flashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        flashImage.color = new Color(1f, 1f, 1f, 1f);

        // 2️⃣ Mantener blanco usando WaitForSecondsRealtime
        yield return new WaitForSecondsRealtime(holdDuration);

        // 3️⃣ Fade OUT a negro (UNSCALED)
        t = 0f;
        Color startColor = flashImage.color;
        Color endColor = Color.black;

        while (t < fadeToBlackDuration)
        {
            t += Time.unscaledDeltaTime;
            flashImage.color = Color.Lerp(startColor, endColor, t / fadeToBlackDuration);
            yield return null;
        }

        flashImage.color = Color.black;
    }
}
