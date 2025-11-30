using UnityEngine;
using UnityEngine.Playables;

public class ActivarCinematicaPorCamara : MonoBehaviour
{
    public PlayableDirector cinematica;
    public bool soloUnaVez = true;
    public IntroLetterbox letterbox; // referencia necesaria

    public MovimientoDerecha vagones;

    private bool yaActivado = false;
    

    private void Start()
    {
        cinematica.stopped += AlTerminarCinematica;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("TriggerAnimacion"))
        {
            vagones.mover=false;
            if (!yaActivado || !soloUnaVez)
            {
                cinematica.Play();
                yaActivado = true;
            }
        }
    }

    private void AlTerminarCinematica(PlayableDirector director)
    {
       // panelCine.SetActive(false);
     
    }
}
