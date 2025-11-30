using UnityEngine;
using System.Collections.Generic;

public class CambioSpritePorTag : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Sprites según Tag")]
    public Sprite spriteParaGiroArriba1;
    public Sprite spriteParaGiroArriba2;
    public Sprite spriteParaGiroArriba3;
    public Sprite spriteParaGiroArriba4;
    public Sprite spriteParaIdle;

    public Sprite spriteParaGiroAbajo1;
    public Sprite spriteParaGiroAbajo2;
    public Sprite spriteParaGiroAbajo3;

    private SpriteRenderer spriteRenderer;

    // Histórico de triggers activados
    public List<string> historialTriggers = new List<string>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("Falta SpriteRenderer en la locomotora");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;

        // Si el tag produjo un cambio visual, lo registramos
        if (AplicarSpritePorTag(tag))
        {
            historialTriggers.Add(tag);
        }
    }

    private bool AplicarSpritePorTag(string tag)
    {
        // Retorna true solo si cambió sprite (evita guardar basura)

        if (gameManager.decision == 1)
        {
            if (tag == "Arriba1") { spriteRenderer.sprite = spriteParaGiroArriba1; return true; }
            if (tag == "Arriba2") { spriteRenderer.sprite = spriteParaGiroArriba2; return true; }
            if (tag == "Arriba3") { spriteRenderer.sprite = spriteParaGiroArriba3; return true; }
            if (tag == "Arriba4") { spriteRenderer.sprite = spriteParaGiroArriba4; return true; }
            if (tag == "Idle")    { spriteRenderer.sprite = spriteParaIdle;        return true; }
        }

        if (gameManager.decisionParaCuestas == 1)
        {
            if (tag == "ArribaAbajo1") { spriteRenderer.sprite = spriteParaGiroAbajo1; return true; }
            if (tag == "ArribaAbajo2") { spriteRenderer.sprite = spriteParaGiroAbajo2; return true; }
            if (tag == "ArribaAbajo3") { spriteRenderer.sprite = spriteParaGiroAbajo3; return true; }
            if (tag == "Idle")         { spriteRenderer.sprite = spriteParaIdle;       return true; }
        }

        if (gameManager.decision == 2)
        {
            if (tag == "Abajo1") { spriteRenderer.sprite = spriteParaGiroAbajo1; return true; }
            if (tag == "Abajo2") { spriteRenderer.sprite = spriteParaGiroAbajo2; return true; }
            if (tag == "Abajo3") { spriteRenderer.sprite = spriteParaGiroAbajo3; return true; }
            if (tag == "Idle")   { spriteRenderer.sprite = spriteParaIdle;       return true; }
        }

        if (gameManager.decisionParaCuestas == 2)
        {
            if (tag == "AbajoArriba1") { spriteRenderer.sprite = spriteParaGiroArriba1; return true; }
            if (tag == "AbajoArriba2") { spriteRenderer.sprite = spriteParaGiroArriba2; return true; }
            if (tag == "AbajoArriba3") { spriteRenderer.sprite = spriteParaGiroArriba3; return true; }
            if (tag == "AbajoArriba4") { spriteRenderer.sprite = spriteParaGiroArriba4; return true; }
            if (tag == "Idle")         { spriteRenderer.sprite = spriteParaIdle;       return true; }
        }

        return false;
    }
}
