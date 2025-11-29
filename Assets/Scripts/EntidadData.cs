using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "NuevoConfigJugador",
    menuName = "Configuraciones/Jugador/ConfigJugador")]
public class EntidadData : ScriptableObject
{
    public GameObject gameObject;

    public bool estaVivo = true;

    //public Sprite imagen;

    //public AnimationClip animacion;

    //public AudioClip sonido;

    //[TextArea]
    //public string descripcion; // opcional

    public void Morir()
    { 
        estaVivo = false;
        Destroy(gameObject,5f);
    }
}
