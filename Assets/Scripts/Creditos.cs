using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class Creditos : MonoBehaviour
{
    public GameObject panelCreditos;
    public float speed = 20f;

    public InputActionAsset inputActions; // Referencia al asset input actions 

    private InputAction Exit;
    
     private void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogWarning("InputActionAsset no asignado en el inspector.");
            return;
        }

        // Buscar las acciones por ruta (ajusta el nombre del action map / acción si es distinto)
        Exit = inputActions.FindAction("Player Controls/Exit");

        // Enable the actions
        Exit.Enable();      
    }

    private void OnDisable()
    {
        // Disable the actions
        Exit.Disable();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        panelCreditos.transform.position += Vector3.up * Time.deltaTime * speed;
        if (Exit.triggered) // solo al pulsar una vez
        {
            SceneManager.LoadScene("MenuInicial");
        }
    }
}
