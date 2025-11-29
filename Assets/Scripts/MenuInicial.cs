using UnityEngine;
using UnityEngine.SceneManagement;
using SystemCollections;
using SystemCollections.Generic;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main");
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
