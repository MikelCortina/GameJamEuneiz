using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    fileName = "NuevoConfigJugador",
    menuName = "Configuraciones/Jugador/ConfigJugador")]
public class EntidadData : MonoBehaviour
{
    public bool estaVivo = true;

    public AnimationClip animacion;

    public AudioClip sonido1;
    public AudioClip sonido2;
    private SpriteRenderer spriteRenderer;
    public Sprite sangre;


    private AudioSource audioSource;

    [TextArea]
    public string descripcion; // opcional
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   // Se obtiene al nacer el objeto
        audioSource = GetComponentInParent<AudioSource>(); // Obtiene el componente en el padre
    }
    public void Morir()
    {
        spriteRenderer.sprite = sangre;
        Debug.Log("Atropellado");
        estaVivo = false;
        //animacion

        //sonido
        audioSource.clip = sonido1;
        audioSource.Play();
        audioSource.clip = sonido2;
        audioSource.Play();

        Destroy(gameObject,5f);
    }
}
