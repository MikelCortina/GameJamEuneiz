using UnityEngine;
using UnityEngine.Splines;

public class BranchTrigger : MonoBehaviour
{
    public SplineContainer upBranch;
    public SplineContainer downBranch;
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
        gameManager.canChangeTrack = false;



        Debug.Log("Entrado en BranchTrigger, palanca reseteada a 0.");

        if (other.TryGetComponent<FollowSpline2D>(out var f))
            f.OnBranchTriggerEntered(this);
    }
}