using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Necesario para TMP_Text

public class ScreenEffects : MonoBehaviour
{
    public Image flashImage;
    public float fadeInDuration = 0.3f;
    public float holdDuration = 2.5f;        // Aumenté un poco para que dé tiempo a leer
    public float fadeToBlackDuration = 1f;

    public GameObject panel;                  // El panel normal del juego (lo ocultamos)

    public AudioSource audioSource;

    // === NUEVAS REFERENCIAS PARA MOSTRAR LA PUNTUACIÓN ===
    public ContadorOvejas contadorOvejas;
    public CanvasGroup deathScoreCanvasGroup; // Crea un CanvasGroup con el texto de puntuación final
    public TMP_Text deathScoreText;           // Texto que dirá "Has contado X ovejas"
    public TMP_Text bestScoreText;            // Opcional: texto de la mejor puntuación

    public void PlayDeathEffects()
    {
        StartCoroutine(FlashWhiteToBlack());
    }
    private void Start()
    {
        // Asegurarse de que el texto esté oculto al inicio
        if (deathScoreCanvasGroup != null)
        {
            deathScoreCanvasGroup.alpha = 0f;
            deathScoreCanvasGroup.gameObject.SetActive(false);
        }
    }

    private IEnumerator FlashWhiteToBlack()
    {
        panel.SetActive(false);
        Time.timeScale = 0f;

        // === PREPARAR EL TEXTO DE PUNTUACIÓN FINAL ===
        if (deathScoreCanvasGroup != null)
        {
            deathScoreCanvasGroup.alpha = 1f;           // Aparece al instante
            deathScoreCanvasGroup.gameObject.SetActive(true);
        }

        if (deathScoreText != null)
        {
            deathScoreText.text = $"Has reventado {contadorOvejas.valor} oveja{(contadorOvejas.valor != 1 ? "s" : "")} alienígena{(contadorOvejas.valor != 1 ? "s" : "")}!";
            deathScoreText.color = Color.black; // Mantener negro
        }

        if (bestScoreText != null)
        {
            int mejor = PlayerPrefs.GetInt("MejorPuntuacion", 0);
            if (contadorOvejas.valor > mejor)
            {
                bestScoreText.text = "¡NUEVO RÉCORD!";
                bestScoreText.color = new Color(1f, 0.8f, 0f); // Dorado bonito
            }
            else
            {
                bestScoreText.text = $"Mejor puntuación: {mejor}";
                bestScoreText.color = Color.black;       // Negro si no es récord
            }
        }

        // 1️⃣ Fade IN blanco
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
            flashImage.color = new Color(1f, 1f, 1f, alpha);

            // No tocar el alpha del texto para que permanezca visible
            yield return null;
        }

        flashImage.color = Color.white;
        audioSource.Play();

        // 2️⃣ Mantener blanco + puntuación visible
        yield return new WaitForSecondsRealtime(holdDuration);

        // 3️⃣ Fade OUT a negro del fondo únicamente (texto se mantiene)
        t = 0f;
        Color startColor = flashImage.color;
        while (t < fadeToBlackDuration)
        {
            t += Time.unscaledDeltaTime;
            float normalized = t / fadeToBlackDuration;

            flashImage.color = Color.Lerp(startColor, Color.black, normalized);

            yield return null;
        }

        flashImage.color = Color.black;

        // Guardar la puntuación y volver al menú
        contadorOvejas.TerminarRun();

        SceneManager.LoadScene("MenuInicial");
        Time.timeScale = 1f;
    }
}