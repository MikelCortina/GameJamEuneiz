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
            DontDestroyOnLoad(gameObject);
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
            Debug.Log("Palanca arriba (2)");
        }
        else if (gameManager.nuevaDecision == -1)
        {
            leverState = 1; // Abajo
            Debug.Log("Palanca abajo (1)");
        }
        else if (gameManager.nuevaDecision == 0)
        {
            leverState = 0; // Medio
            Debug.Log("Palanca medio (0)");
        }
    }

    public void ResetLever()
    {
        leverState = 0;
        Debug.Log("Palanca reiniciada a medio (0)");
    }
}