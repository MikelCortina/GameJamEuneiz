using UnityEngine;

public class EndGame : MonoBehaviour
{
    public ScreenEffects screenEffects;

    // En Start(), Awake() o donde lo necesites
    void Awake()
    {
        screenEffects = FindObjectOfType<ScreenEffects>();

        if (screenEffects == null)
        {
            Debug.LogError("No se encontró ningún objeto ScreenEffects en la escena!");
        }
        else
        {
            Debug.Log("ScreenEffects encontrado: " + screenEffects.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            screenEffects.PlayDeathEffects();
        }
    }
}
