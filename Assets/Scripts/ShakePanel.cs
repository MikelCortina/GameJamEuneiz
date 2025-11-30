using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakePanel : MonoBehaviour
{
    public RectTransform panel;
    public float intensidad = 10f;
    public float duracion = 0.3f;
    public Transform rayoTransform;
    public Vector3 vectorRayo;
    private void Start()
    {
        vectorRayo = rayoTransform.localPosition;
    }
    public void Shake()
    {
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        Vector3 origen = panel.anchoredPosition;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;
            panel.anchoredPosition = origen + new Vector3(x, y, 0);

            tiempo += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = origen;
    }
    public void ResetPosition()
    { 
        panel.anchoredPosition = vectorRayo;
    }
}
