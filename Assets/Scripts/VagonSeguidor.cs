using UnityEngine;

public class VagonSeguidor : MonoBehaviour
{
    [SerializeField] private CambioSpritePorTag locomotora; // arrastrar aquí el objeto locomotora
    [SerializeField] private SpriteRenderer miRenderer;
    [SerializeField] private float distanciaX = 2f;

    [Header("Delay y Suavizado")]
    [SerializeField] private float delayY = 0.1f;
    [SerializeField] private float smoothY = 8f;

    private float delayedY;
    private float timer;
    private int indiceSpriteActual = 0;

    public Sprite vagonArriba1;
    public Sprite vagonArriba2;
    public Sprite vagonArriba3;


    public Sprite vagonAbajo1;
    public Sprite vagonAbajo2;
    public Sprite vagonAbajo3;

    public Sprite spriteParaIdle;



    private SpriteRenderer spriteRenderer;

    void Start()
    {
        delayedY = locomotora.transform.position.y;
    }

    void LateUpdate()
    {
        SeguirPosicion();

        // Si la locomotora avanzó a un nuevo evento de sprite, el vagón lo copia
        if (indiceSpriteActual < locomotora.historialTriggers.Count)
        {
            string nuevoTrigger = locomotora.historialTriggers[indiceSpriteActual];
            AplicarSpriteComoLocomotora(nuevoTrigger);
            indiceSpriteActual++;
        }
    }

    private void SeguirPosicion()
    {
        timer += Time.deltaTime;
        if (timer >= delayY)
        {
            delayedY = locomotora.transform.position.y;
            timer = 0f;
        }

        float yLerp = Mathf.Lerp(transform.position.y, delayedY, Time.deltaTime * smoothY);
        float xPos = locomotora.transform.position.x - distanciaX;
        transform.position = new Vector3(xPos, yLerp, transform.position.z);
    }

    private void AplicarSpriteComoLocomotora(string tag)
    {
        if (tag == "Arriba1") { miRenderer.sprite = vagonArriba1; return; }
        if (tag == "Arriba2") { miRenderer.sprite = vagonArriba2; return; }
        if (tag == "Arriba3") { miRenderer.sprite = vagonArriba3; return; }


        if (tag == "ArribaAbajo1") { miRenderer.sprite = vagonAbajo1; return; }
        if (tag == "ArribaAbajo2") { miRenderer.sprite = vagonAbajo2; return; }
        if (tag == "ArribaAbajo3") { miRenderer.sprite = vagonAbajo3; return; }

        if (tag == "Abajo1") { miRenderer.sprite = vagonAbajo1; return; }
        if (tag == "Abajo2") { miRenderer.sprite = vagonAbajo2; return; }
        if (tag == "Abajo3") { miRenderer.sprite = vagonAbajo3; return; }

        if (tag == "AbajoArriba1") { miRenderer.sprite = vagonArriba1; return; }
        if (tag == "AbajoArriba2") { miRenderer.sprite = vagonArriba2; return; }
        if (tag == "AbajoArriba3") { miRenderer.sprite = vagonArriba3; return; }



    }
}
