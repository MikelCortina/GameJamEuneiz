using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraFollow))]
public class IntroLetterbox : MonoBehaviour
{
    [Header("≡ REFERENCIAS UI")]
    public Image topBar;
    public Image bottomBar;

    [Header("≡ COMPORTAMIENTO")]
    public bool startWithLetterbox = false;   // true = empieza con bandas | false = empieza sin bandas (pantalla completa)
    public bool endWithLetterbox = true;    // true = termina con bandas | false = las quita al final

    [Header("≡ TAMAÑO DE LAS BANDAS")]
    [Range(50f, 600f)] public float closedHeight = 280f;   // Altura final cuando están "cerradas" o "abiertas" (según dirección)

    [Header("≡ ANIMACIÓN")]
    [Range(0.5f, 10f)] public float durationOverride = 0f; // 0 = usa la misma duración que CameraFollow
    public AnimationCurve animationCurve = new AnimationCurve(
        new Keyframe(0f, 0f, 0f, 0f),
        new Keyframe(0.3f, 0.1f, 0.3f, 0.3f),
        new Keyframe(0.7f, 0.9f, 2f, 2f),
        new Keyframe(1f, 1f, 0f, 0f)
    );

    private CameraFollow camFollow;
    private RectTransform topRect, bottomRect;
    private float timer = 0f;
    private bool animating = false;

    void Awake()
    {
        camFollow = GetComponent<CameraFollow>();

        if (topBar == null || bottomBar == null)
        {
            Debug.LogError("IntroLetterbox: Asigna topBar y bottomBar");
            enabled = false;
            return;
        }

        topRect = topBar.rectTransform;
        bottomRect = bottomBar.rectTransform;
        topBar.color = bottomBar.color = Color.black;

        // Configuración inicial según lo que quieras
        float startH = startWithLetterbox ? closedHeight : 0f;
        topRect.sizeDelta = new Vector2(topRect.sizeDelta.x, startH);
        bottomRect.sizeDelta = new Vector2(bottomRect.sizeDelta.x, startH);

        topBar.gameObject.SetActive(true);
        bottomBar.gameObject.SetActive(true);

        // Reiniciamos el timer
        timer = 0f;
        animating = true;
    }

    void Update()
    {
        if (!animating || camFollow == null) return;

        // Duración: usar la del CameraFollow o la personalizada
        float totalDuration = durationOverride > 0.1f ? durationOverride : camFollow.introDuration;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / totalDuration);
        float curveValue = animationCurve.Evaluate(t);

        float targetHeight;

        if (startWithLetterbox)
        {
            // Empieza con bandas → se van quitando
            targetHeight = Mathf.Lerp(closedHeight, 0f, curveValue);
        }
        else
        {
            // Empieza sin bandas → van entrando
            targetHeight = Mathf.Lerp(0f, closedHeight, curveValue);
        }

        topRect.sizeDelta = new Vector2(topRect.sizeDelta.x, targetHeight);
        bottomRect.sizeDelta = new Vector2(bottomRect.sizeDelta.x, targetHeight);

        // Final de animación
        if (t >= 1f)
        {
            animating = false;

            if (!endWithLetterbox)
            {
                topBar.enabled = false;
                bottomBar.enabled = false;
            }
        }
    }

    // === BOTONES RÁPIDOS EN EL INSPECTOR ===
    [ContextMenu("Probar: Bandas entrando")]
    void TestEnter() { StartAnimation(false, true); }

    [ContextMenu("Probar: Bandas saliendo")]
    void TestExit() { StartAnimation(true, false); }

    public void StartAnimation(bool startClosed, bool endClosed)
    {
        startWithLetterbox = startClosed;
        endWithLetterbox = endClosed;
        Awake(); // Reinicia la animación
    }
}