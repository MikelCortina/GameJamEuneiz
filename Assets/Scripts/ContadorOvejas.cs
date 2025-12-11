using UnityEngine;
using TMPro;

public class ContadorOvejas : MonoBehaviour
{
    [SerializeField] private TMP_Text textoUI; // Asigna el TMP_Text del Canvas
    public int valor = 0;

    void Start()
    {
        ActualizarTexto();
    }

    public void ActualizarTexto()
    {
        if (textoUI != null)
            textoUI.text = "x"+ valor.ToString();
    }
}
