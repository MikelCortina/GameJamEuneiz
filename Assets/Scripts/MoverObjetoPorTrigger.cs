using UnityEngine;

public class MoverObjetoPorDistancia : MonoBehaviour
{
    [Header("Objeto que se va a mover")]
    public Transform objetoAMover;

    [Header("Tres posiciones posibles para el teletransporte")]
    public Transform[] posicionesDestino = new Transform[3];

    [Header("Distancia base")]
    public float distanciaBase = 5f;

    [Header("Opciones adicionales")]
    public bool moverSoloUnaVez = true;

    private Transform objetoDetector;
    private bool yaMovio = false;

    private float nivelActual = 0;

    public EntidadData entidadData;

    public Animator animator;

    public AnimationClip vibra;
    public AnimationClip idle;

    private void OnEnable()
    {
        if (NivelManager.Instancia != null)
        {
            nivelActual = NivelManager.Instancia.NivelActual;
            NivelManager.Instancia.OnNivelCambiado += ActualizarNivel;
        }
    }

    private void OnDisable()
    {
        if (NivelManager.Instancia != null)
            NivelManager.Instancia.OnNivelCambiado -= ActualizarNivel;
    }

    private void ActualizarNivel(float nuevoNivel)
    {
        nivelActual = nuevoNivel;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            objetoDetector = playerObj.transform;
    }

    private void Update()
    {
        if (objetoAMover == null || objetoDetector == null)
            return;

        if (moverSoloUnaVez && yaMovio)
            return;

        float distanciaRequerida = distanciaBase / (1f + nivelActual);
        float distanciaActual = Vector3.Distance(objetoDetector.position, transform.position);

        if (distanciaActual <= distanciaRequerida)
            SetVibra();
    }

    private void MoverObjeto()
    {
        entidadData.Morir();

        if (posicionesDestino.Length != 3)
        {
            Debug.LogError("Debes asignar exactamente 3 posiciones destino.");
            return;
        }

        float[] distancias = new float[3];
        for (int i = 0; i < 3; i++)
            distancias[i] = Vector3.Distance(objetoAMover.position, posicionesDestino[i].position);

        int indiceMasCercano = 0;
        for (int i = 1; i < 3; i++)
            if (distancias[i] < distancias[indiceMasCercano])
                indiceMasCercano = i;

        var indicesValidos = new System.Collections.Generic.List<int> { 0, 1, 2 };
        indicesValidos.Remove(indiceMasCercano);

        int indiceElegido = indicesValidos[Random.Range(0, indicesValidos.Count)];

        objetoAMover.position = posicionesDestino[indiceElegido].position;

        yaMovio = true;
    }

    public void SetIdle()
    {
        animator.Play(idle.name);
    }
    public void SetVibra()
    {
        animator.Play(vibra.name);
    }
}
