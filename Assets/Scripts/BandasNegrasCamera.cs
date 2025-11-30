using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraFollow))]
public class IntroLetterbox : MonoBehaviour
{
    [Header("≡ REFERENCIAS UI")]
    public Image topBar;
    public Image bottomBar;

    [Header("≡ TAMAÑO DE LAS BANDAS")]
    [Range(50f, 600f)] public float closedHeight = 280f;

    [Header("≡ ANIMACIÓN")]
    [Range(0.5f, 10f)] public float durationOverride = 0f;
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
    private bool closing = false;

    void Awake()
    {
        camFollow = GetComponent<CameraFollow>();

        topRect = topBar.rectTransform;
        bottomRect = bottomBar.rectTransform;
        topBar.color = bottomBar.color = Color.black;

        // arranca SIN animación
        topRect.sizeDelta = new Vector2(topRect.sizeDelta.x, 0f);
        bottomRect.sizeDelta = new Vector2(bottomRect.sizeDelta.x, 0f);

        topBar.gameObject.SetActive(true);
        bottomBar.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!animating) return;

        float totalDuration = durationOverride > 0.1f ? durationOverride : camFollow.introDuration;
        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / totalDuration);
        float curveValue = animationCurve.Evaluate(t);

        float targetHeight = closing
            ? Mathf.Lerp(0f, closedHeight, curveValue)      // cerrando
            : Mathf.Lerp(closedHeight, 0f, curveValue);     // abriendo

        topRect.sizeDelta = new Vector2(topRect.sizeDelta.x, targetHeight);
        bottomRect.sizeDelta = new Vector2(bottomRect.sizeDelta.x, targetHeight);

        if (t >= 1f) animating = false;
    }

    // LLAMAR SOLO DESDE TU SCRIPT
    public void AbrirBandas()   // quita bandas → empieza arriba y baja
    {
        closing = false;
        timer = 0f;
        animating = true;
    }

    public void CerrarBandas() // cierra bandas solo cuando mando la orden
    {
        closing = true;
        timer = 0f;
        animating = true;
    }
}
