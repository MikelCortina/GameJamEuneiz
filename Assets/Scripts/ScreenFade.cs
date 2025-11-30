using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] private Image blackScreen; // Imagen negra UI que cubrirá toda la pantalla
    [SerializeField] private float duration = 1.5f; // Tiempo del fade

    private void Awake()
    {
        if (blackScreen == null)
        {
            Debug.LogError("ScreenFade: La referencia a la imagen negra no está asignada");
            return;
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        Color c = blackScreen.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / duration);
            blackScreen.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        blackScreen.color = new Color(c.r, c.g, c.b, 0f); // Asegura transparencia total al final
    }
}
