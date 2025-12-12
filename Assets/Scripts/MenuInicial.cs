using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main 1");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
