using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "NuevoConfigJugador",
    menuName = "Configuraciones/Jugador/ConfigJugador")]
public class EntidadData : MonoBehaviour
{
    public bool estaVivo = true;

    public Sprite imagen;

    public AnimationClip animacion;

    public AudioClip sonido;

    public AudioSource audioSource;

    [TextArea]
    public string descripcion; // opcional

    public void Morir()
    {
        Debug.Log("Atropellado");
        estaVivo = false;
        //animacion

        //sonido
        audioSource.clip = sonido;
        audioSource.Play();

        Destroy(gameObject,5f);
    }
}
