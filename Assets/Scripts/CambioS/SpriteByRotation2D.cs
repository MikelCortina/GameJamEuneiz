using UnityEngine;

public class SpriteByRotationInterpolated : MonoBehaviour
{
    public Sprite arribaDerecha;
    public Sprite arribaDerechaIntermedio;
    public Sprite medioArribaDerecha;
    public Sprite medioArribaDerechaIntermedio;
    public Sprite adelante;
    public Sprite medioAbajoDerechaIntermedio;
    public Sprite medioAbajoDerecha;
    public Sprite abajoDerechaIntermedio;
    public Sprite abajoDerecha;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float z = transform.eulerAngles.z;

        // Convertir a -180 a 180
        if (z > 180f)
            z -= 360f;

        // Selección de sprite según rangos intermedios
        if (z > 33.75f && z <= 45f)
            sr.sprite = arribaDerecha;
        else if (z > 22.5f && z <= 33.75f)
            sr.sprite = arribaDerechaIntermedio;
        else if (z > 11.25f && z <= 22.5f)
            sr.sprite = medioArribaDerecha;
        else if (z > 3f && z <= 11.25f)
            sr.sprite = medioArribaDerechaIntermedio;
        else if (z > -11.25f && z <= 3f)
            sr.sprite = adelante;
        else if (z > -22.5f && z <= -11.25f)
            sr.sprite = medioAbajoDerechaIntermedio;
        else if (z > -33.75f && z <= -22.5f)
            sr.sprite = medioAbajoDerecha;
        else if (z > -45f && z <= -33.75f)
            sr.sprite = abajoDerechaIntermedio;
        else
            sr.sprite = abajoDerecha;
    }
}
