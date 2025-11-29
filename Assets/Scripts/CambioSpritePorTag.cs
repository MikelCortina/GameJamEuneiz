using UnityEngine;

public class CambioSpritePorTag : MonoBehaviour
{

    public GameManager gameManager;
    // Asigna aquí tus sprites en el Inspector
    [Header("Sprites según Tag")]
    public Sprite spriteParaGiroArriba1;
    public Sprite spriteParaGiroArriba2;
    public Sprite spriteParaGiroArriba3;
    public Sprite spriteParaGiroArriba4;
    public Sprite spriteParaIdle;

    public Sprite spriteParaGiroAbajo1;
    public Sprite spriteParaGiroAbajo2;
    public Sprite spriteParaGiroAbajo3;

    // Puedes añadir más si necesitas

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("Este objeto necesita un SpriteRenderer!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManager.decision == 1)
        {
            // Cambia según el tag del activador
            if (other.CompareTag("Arriba1") && spriteParaGiroArriba1 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba1;
            }
            else if (other.CompareTag("Arriba2") && spriteParaGiroArriba2 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba2;
            }
            else if (other.CompareTag("Arriba3") && spriteParaGiroArriba3 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba3;
            }
            else if (other.CompareTag("Arriba4") && spriteParaGiroArriba4 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba4;
            }
            else if (other.CompareTag("Idle") && spriteParaIdle != null)
            {
                spriteRenderer.sprite = spriteParaIdle;
            }
        }


        if (gameManager.decisionParaCuestas == 1)
            if (other.CompareTag("ArribaAbajo1") && spriteParaGiroAbajo1 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo1;
            }
            else if (other.CompareTag("ArribaAbajo2") && spriteParaGiroAbajo2 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo2;
            }
            else if (other.CompareTag("ArribaAbajo3") && spriteParaGiroAbajo3 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo3;
            }
            else if (other.CompareTag("Idle") && spriteParaIdle != null)
            {
                spriteRenderer.sprite = spriteParaIdle;
            }


        if (gameManager.decision == 2)
        {
            // Cambia según el tag del activador
            if (other.CompareTag("Abajo1") && spriteParaGiroAbajo1 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo1;
            }
            else if (other.CompareTag("Abajo2") && spriteParaGiroAbajo2 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo2;
            }
            else if (other.CompareTag("Abajo3") && spriteParaGiroAbajo3 != null)
            {
                spriteRenderer.sprite = spriteParaGiroAbajo3;
            }
            else if (other.CompareTag("Idle") && spriteParaIdle != null)
            {
                spriteRenderer.sprite = spriteParaIdle;
            }
        }


        if (gameManager.decisionParaCuestas == 2)
        {


            if (other.CompareTag("AbajoArriba1") && spriteParaGiroArriba1 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba1;
            }
            else if (other.CompareTag("AbajoArriba2") && spriteParaGiroArriba2 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba2;
            }
            else if (other.CompareTag("AbajoArriba3") && spriteParaGiroArriba3 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba3;
            }
            else if (other.CompareTag("AbajoArriba4") && spriteParaGiroArriba4 != null)
            {
                spriteRenderer.sprite = spriteParaGiroArriba4;
            }
            else if (other.CompareTag("Idle") && spriteParaIdle != null)
            {
                spriteRenderer.sprite = spriteParaIdle;
            }
        }
    


        // Añade más "else if" aquí si tienes más tags

        // Opcional: destruir o desactivar el activador después de usarlo
        // Destroy(other.gameObject);
        // other.gameObject.SetActive(false);
    }
}