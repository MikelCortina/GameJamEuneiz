using UnityEngine;

public class MergeTrigger : MonoBehaviour
{
    public GameManager gameManager;

    private void Awake()
    {
        // Busca un GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("No se encontr� un GameManager en la escena.");
        }
         gameManager.canChangeTrack = true;
         gameManager.blocked = false;
         gameManager.pausado=false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.canChangeTrack = true;
        // Fuerza la palanca al medio (con animaci�n y sonido)
        gameManager.ForzarPalancaAlMedio();
        if (other.TryGetComponent<FollowSpline2D>(out var f))
            f.OnMergeTriggerEntered(this);
    }
}