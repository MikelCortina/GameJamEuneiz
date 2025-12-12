using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public int leverState = 0; // 0: medio, 1: abajo, 2: arriba
    public GameManager gameManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Ejemplo de control con teclas (puedes cambiarlo a UI o lo que quieras)
        if (gameManager.nuevaDecision==1)
        {
            leverState = 2; // Arriba

        }
        else if (gameManager.nuevaDecision == -1)
        {
            leverState = 1; // Abajo
      
        }
        else if (gameManager.nuevaDecision == 0)
        {
            leverState = 0; // Medio
            
        }
    }

    public void ResetLever()
    {
        leverState = 0;
        Debug.Log("Palanca reiniciada a medio (0)");
    }
}