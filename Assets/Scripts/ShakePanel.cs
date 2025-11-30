using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakePanel : MonoBehaviour
{
    public RectTransform panel;
    public float intensidad = 10f;
    public float duracion = 0.3f;

    private Vector2 posicionInicial;

    private void Start()
    {
        posicionInicial = panel.anchoredPosition;   // Guardamos su posición original real
    }

    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;
            panel.anchoredPosition = posicionInicial + new Vector2(x, y);

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Al final vuelve exactamente donde estaba
        panel.anchoredPosition = posicionInicial;
    }
}
