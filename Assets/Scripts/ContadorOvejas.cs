using UnityEngine;
using TMPro;

public class ContadorOvejas : MonoBehaviour
{
    [SerializeField] private TMP_Text textoUI; // Asigna el TMP_Text del Canvas
    [SerializeField] private GameObject panelMejoresPuntuaciones; // Panel que muestra las puntuaciones
    [SerializeField] private TMP_Text textoMejoresPuntuaciones; // TMP_Text dentro del panel

    public int valor = 0;
  

    void Start()
    {
        ActualizarTexto();
        Time.timeScale=Time.unscaledTime;
    }

    public void ActualizarTexto()
    {
        if (textoUI != null)
            textoUI.text = "x" + valor.ToString();
    }

    // Llamar al final de la run
    public void TerminarRun()
    {
        GuardarPuntuacion();
        valor = 0;
        ActualizarTexto();

    }

    private void GuardarPuntuacion()
    {
        // Recupera la mejor puntuación anterior
        int mejorPuntuacion = PlayerPrefs.GetInt("MejorPuntuacion", 0);

        if (valor > mejorPuntuacion)
        {
            PlayerPrefs.SetInt("MejorPuntuacion", valor);
            PlayerPrefs.Save();
        }
    }

    public void MostrarPanelMejores()
    {
        if (panelMejoresPuntuaciones != null)
            panelMejoresPuntuaciones.SetActive(true);

        if (textoMejoresPuntuaciones != null)
        {
            int mejor = PlayerPrefs.GetInt("MejorPuntuacion", 0);
            textoMejoresPuntuaciones.text = "Mejor puntuación: " + mejor.ToString();
        }
    }
}
