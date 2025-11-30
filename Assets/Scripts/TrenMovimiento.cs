using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class TrenMovimiento : MonoBehaviour
{
    [SerializeField] public float velocidad = 5f;
    public GameManager gameManager;
    public SpawnManager spawnManager;
    public GameObject resetPoint;
    public GameObject actionPoint;
    public float distanciaEntrePuntos = 40f;
    public float alturaCambio = 3f;

    public float velocidadVertical = 5f; // Velocidad del Lerp vertical

    private Vector3 targetPosition;

    private bool blockingInputs = false;

    [SerializeField] private float smoothTimeVertical = 0.25f; // Tiempo de suavizado vertical
    private float velocityY = 0f; // Necesario para SmoothDamp
    public ShakePanel shakePanel;  // Asigna tu panel con efecto shake desde el inspector

    public Animator animator; // Asigna el Animator desde el inspector



    void Start()
    {
        targetPosition = transform.position; // iniciar posición objetivo
    }



    void Update()
    {
        // Movimiento horizontal constante (puedes seguir reduciéndolo si quieres)
        float horizontalSpeed = velocidad;

        float verticalDiff = Mathf.Abs(transform.position.y - targetPosition.y);
        if (verticalDiff > 0.01f)
            horizontalSpeed *= 0.85f; // opcional: más lento mientras sube/baja

        transform.position += Vector3.right * horizontalSpeed * Time.deltaTime;

        // <<< AQUÍ ESTÁ LA MAGIA >>>
        // Suavizado natural con curva de aceleración/desaceleración
        float newY = Mathf.SmoothDamp(
            transform.position.y,
            targetPosition.y,
            ref velocityY,
            smoothTimeVertical
        );

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void IrPorArriba()
    {
        targetPosition = new Vector3(transform.position.x, transform.position.y + alturaCambio, transform.position.z);

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
        // Detecta si el objeto que entra tiene el script Atropello o derivado
        Atropello atropello = collision.GetComponent<Atropello>();
        if (atropello != null)
        {
            shakePanel.Shake();
            animator.Play("SustoClip");
        }


        if (collision.CompareTag("ResetPoint"))
        {
            StartCoroutine(BlockInputs());
            Debug.Log("RESET");
            gameManager.canChangeTrack = true;

            gameManager.nuevaDecision = 0;
            if (gameManager.decisionParaCuestas == 1)
            {
                gameManager.animator.Play("PalancaArribaMedio");
                gameManager.audioSource.PlayOneShot(gameManager.palancaArribaMedioSound);
            }
            else if (gameManager.decisionParaCuestas == 2)
            {
                gameManager.animator.Play("PalancaAbajoMedio");
                gameManager.audioSource.PlayOneShot(gameManager.palancaAbajoMedioSound);
            }

            IrPorElMedio();
        }

        if (collision.CompareTag("ActionPoint"))
        {
            Debug.Log("ACCION");
            spawnManager.Randomizar();

            if (gameManager.decision == 1)
            {
                IrPorArriba();
                gameManager.canChangeTrack = false;
            }
            else if (gameManager.decision == 2)
            {
                IrPorAbajo();
                gameManager.canChangeTrack = false;
            }
            else if (gameManager.decision == 0)
            {
                Debug.Log("Fin del juego)");
            }
        }
    }

    public IEnumerator BlockInputs()
    {
        if (blockingInputs) yield break; // Evita apilar bloqueos

        blockingInputs = true;
        gameManager.blocked = true;

        yield return new WaitForSeconds(0.2f);

        gameManager.blocked = false;
        blockingInputs = false;
    }
}
