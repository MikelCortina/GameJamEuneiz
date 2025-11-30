using UnityEngine;
using UnityEngine.InputSystem;

public class Pausa : MonoBehaviour
{
    private bool pausado = false;

    [SerializeField] public Canvas canvas;

    public InputActionAsset inputActions;

    private InputAction pausaAction;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogWarning("InputActionAsset no asignado en el inspector.");
            return;
        }

        // Buscar la acción por ruta (ajusta el nombre del action map / acción si es distinto)
        pausaAction = inputActions.FindAction("Player Controls/Pause");

        // Enable the action
        pausaAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the action
        pausaAction.Disable();
    }

    private void Start()
    {
        // Asegurar que el menú de pausa esté oculto al inicio
        canvas.enabled = false;
    }

    public void Update()
    {
        if (pausaAction.triggered)
        {
            if (!pausado)
            {
                Pausar();
            }
            else if(pausado)
            {
                Reanudar();
            }
        }
    }
    
    public void Pausar()
    {
        // Muestra el menú de pausa
        canvas.enabled = true;
        pausado = true;
    }

    public void Reanudar()
    {

        // Oculta el menú de pausa
        canvas.enabled = false;
        pausado = false;
    }
}
