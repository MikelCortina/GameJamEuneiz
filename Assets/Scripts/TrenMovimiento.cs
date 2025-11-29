using System.Collections;
using UnityEngine;

public class TrenMovimiento : MonoBehaviour
{
    [SerializeField] public float velocidad = 5f;
    public GameManager gameManager;
    public GameObject resetPoint;
    public GameObject actionPoint;
    public float distanciaEntrePuntos = 40f;
    public float alturaCambio = 3f;
    public Animator animator;

    public float velocidadVertical = 5f; // Velocidad del Lerp vertical

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position; // iniciar posición objetivo
    }

    void Update()
    {
        // Movimiento horizontal constante
        transform.position += new Vector3(velocidad, 0, 0) * Time.deltaTime;

        // Movimiento vertical suave hacia la posición objetivo
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, targetPosition.y, transform.position.z),
            Time.deltaTime * velocidadVertical
        );

    }

    void IrPorArriba()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + alturaCambio, transform.position.z);
        animator.Play("ArribaTren", -1, 0f);
    }

    void IrPorAbajo()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y - alturaCambio, transform.position.z);
    }

    void IrPorElMedio()
    {
        targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetPoint"))
        {
            Debug.Log("RESET");
            gameManager.canChangeTrack = true;
            gameManager.decision = 0;
            //resetPoint.transform.position += new Vector3(distanciaEntrePuntos, 0, 0);
            IrPorElMedio();
        }

        if (collision.CompareTag("ActionPoint"))
        {
            Debug.Log("ACCION");
            //actionPoint.transform.position += new Vector3(distanciaEntrePuntos, 0, 0);

            if (gameManager.decision == 1)
            {
                IrPorArriba();
                gameManager.canChangeTrack = false;
            }

            if (gameManager.decision == 2)
            {
                IrPorAbajo();
                gameManager.canChangeTrack = false;
            }
               
        }
    }
}
