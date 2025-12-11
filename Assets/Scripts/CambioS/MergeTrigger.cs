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
            Debug.LogError("No se encontró un GameManager en la escena.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.canChangeTrack = true;
        // Fuerza la palanca al medio (con animación y sonido)
        gameManager.ForzarPalancaAlMedio();
        if (other.TryGetComponent<FollowSpline2D>(out var f))
            f.OnMergeTriggerEntered(this);
    }
}